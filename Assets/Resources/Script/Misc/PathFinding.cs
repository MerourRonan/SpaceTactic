using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PathFinding {

	public class Node
	{
		private Tile m_Tile;
		private Node m_PreviousNode;
		private float m_Fscore;
		private float m_Gscore;


		public Node(Tile Tile, Node PreviousNode, float Fscore, float Gscore)
		{
			m_Tile = Tile;
			m_PreviousNode = PreviousNode;
			m_Fscore = Fscore;
			m_Gscore = Gscore;
		}

		public Tile GetTile()
		{
			return m_Tile;
		}

		public Node GetPreviousNode()
		{
			return m_PreviousNode;
		}

		public float GetFscore()
		{
			return m_Fscore;
		}

		public float GetGscore()
		{
			return m_Gscore;
		}
	}

	public static List<Tile> FindDirectPath(SpaceObject ship, Tile EndTile)
	{
		//Debug.Log ("Starting pathfinding for destination : " + EndTile.name);
		bool noObstacle = false;
		int clearance = ship.GetClearance ();
		Tile StartTile = ship.GetReferenceTile ();
		List<Tile> TotalPath = new List<Tile>();
		List<Node> OpenNodes = new List<Node>();
		List<Node> CloseNodes = new List<Node>();
		HashSet<Tile> OpenTileHashSet = new HashSet<Tile>();
		HashSet<Tile> CloseTileHashSet = new HashSet<Tile>();
		
		float Gscore = 0;
		float Fscore = Gscore + Vector3.Distance(StartTile.transform.position, EndTile.transform.position);
		Node CurrentNode = new Node(StartTile,null,Fscore,Gscore);

		OpenNodes.Add (CurrentNode);
		
		while(OpenNodes.Count != 0)
		{
			CurrentNode = NodeWithLowestFscore(OpenNodes);
			if(CheckNoObstacle(CurrentNode.GetTile(),EndTile,clearance) == true)
			{
				noObstacle = true;
				CurrentNode = new Node(EndTile,CurrentNode,0,0);
			}
			
			if(CurrentNode.GetTile() == EndTile || noObstacle == true)
			{
				TotalPath = ReconstructDirectPath(clearance,CurrentNode,StartTile);
				return TotalPath;
			}
			
			OpenNodes.Remove(CurrentNode);
			OpenTileHashSet.Remove (CurrentNode.GetTile ());
			CloseNodes.Add(CurrentNode);
			CloseTileHashSet.Add(CurrentNode.GetTile ());

			foreach(Tile tile in CurrentNode.GetTile().GetNeighboors(clearance))
			{
				if(TileInListNodes(tile, CloseNodes))
				if(CloseTileHashSet.Contains(tile))
				{
					continue;
				}
				
				float tentative_Gscore = CurrentNode.GetGscore() + Vector3.Distance(tile.transform.position, CurrentNode.GetTile().transform.position);
				
				if(!OpenTileHashSet.Contains(tile) || OpenNodes.Find(x=>x.GetTile() == tile).GetGscore() > tentative_Gscore)
				{
					if(!OpenTileHashSet.Contains(tile) 
					   && tile.GetFree()
					   && tile.GetMoveThrough())
					{
						Fscore = tentative_Gscore + Vector3.Distance(tile.transform.position,EndTile.transform.position);
						OpenNodes.Add(new Node(tile,CurrentNode,Fscore,tentative_Gscore));
						OpenTileHashSet.Add(tile);
					}
				}
			}
		}
		return TotalPath;
	}

	public static List<Tile> ReconstructDirectPath(int clearance, Node EndNode , Tile StartTile)
	{
		List<Tile> TotalDirectPath = new List<Tile>();
		Node CurrentNode = EndNode;
		Node SavedNode = CurrentNode.GetPreviousNode();
		Node NextNode = CurrentNode.GetPreviousNode();

		TotalDirectPath.Add (CurrentNode.GetTile ());
		while(CurrentNode.GetTile() != StartTile)
		{
			RaycastHit[] hits;
			Transform CurrentNavTrigger = CurrentNode.GetTile().transform.Find("NavTrigger");
			Transform NextNavTrigger = NextNode.GetTile().transform.Find("NavTrigger");
			Vector3 dir = NextNavTrigger.position-CurrentNavTrigger.position;
			hits = Physics.RaycastAll(CurrentNavTrigger.position, dir, dir.magnitude,(1<<LayerMask.NameToLayer("Nav")));

			foreach(RaycastHit hit in hits)
			{
				if(hit.transform.GetComponentInParent<Tile>().GetFree() == false || hit.transform.GetComponentInParent<Tile>().GetClearance() < clearance)
				{
					CurrentNode = SavedNode;
					TotalDirectPath.Add(CurrentNode.GetTile());
					break;
				}
			}
			SavedNode = NextNode;
			NextNode = NextNode.GetPreviousNode();
			if(NextNode == null)
			{
				TotalDirectPath.Add(StartTile);
				break;
			}
		}
		TotalDirectPath.Reverse ();
		return TotalDirectPath;
	}


	static Node NodeWithLowestFscore(List<Node> OpenNodes)
	{
		float fscore = Mathf.Infinity;
		Node closestNode = null;
		foreach(Node node in OpenNodes)
		{
			if(node.GetFscore()<fscore)
			{
				fscore = node.GetFscore();
				closestNode = node;
			}
		}
		return closestNode;
	}

	static bool TileInListNodes(Tile Tile, List<Node> Nodes)
	{
		foreach(Node Node in Nodes)
		{
			if(Node.GetTile() == Tile)
			{
				return true;
			}
		}
		return false;
	}

	public static bool CheckNoObstacle(Tile currentTile, Tile endTile,int clearance)
	{
		RaycastHit[] hits;
		Vector3 dir = endTile.transform.position-currentTile.transform.position;
		hits = Physics.RaycastAll(currentTile.transform.position, dir, dir.magnitude,(1<<LayerMask.NameToLayer("Nav")));
		foreach(RaycastHit hit in hits)
		{
			if(hit.transform.GetComponentInParent<Tile>().GetFree() == false || hit.transform.GetComponentInParent<Tile>().GetClearance() < clearance)
			{
				return false;
			}
		}
		return true;
	}






}
