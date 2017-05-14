using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPathFinder : Grid2 {

	public static GridPathFinder Instance;

	public HashSet<Tile> m_MoveRangeTiles;
	public List<Tile> m_PathFindingTiles;
	public List<Tile> m_DestinationTiles;

	protected void Awake()
	{
		base.Awake ();
		Instance = this;
		m_PathFindingTiles = new List<Tile> ();
		m_DestinationTiles = new List<Tile> ();
		m_MoveRangeTiles = new HashSet<Tile> ();
	}

	public void ComputeAllPathfindingClearance()
	{
		foreach(Tile currentTile in m_TilesArray)
		{
			ComputeTileClearance(currentTile);
		}
	}

	public void ComputeTileClearance(Tile tile)
	{
		for (int clearanceLevel = 1; clearanceLevel <=3; clearanceLevel++) 
		{
			List<Tile> clearanceBox = Utilities.RecoverClearanceBox(tile,clearanceLevel);
			tile.SetClearance(clearanceLevel);
			
			if(clearanceBox == null)
			{
				tile.SetClearance(tile.GetClearance()-1);
				return;
			}
			
			foreach (Tile tileInBox in clearanceBox) 
			{
				if (tileInBox.GetComponent<Tile> ().GetFree () == false) 
				{
					tile.SetClearance(tile.GetClearance()-1);
					return;
				}
			}
		}
	}

	public void ComputeNeighboorsClearance(Tile tile)
	{
		List<Tile> clearanceNeighboors = Utilities.RecoverClearanceNeighboors(tile,3);
		foreach(Tile currentTile in clearanceNeighboors)
		{
			ComputeTileClearance(currentTile);
		}
	}

	public void ComputeMoveRange(SpaceObject objectSelected)
	{
		m_MoveRangeTiles.Clear ();
		m_MoveRangeTiles = Utilities.ComputeTileInRange(objectSelected,true);
	}

	public void ComputePathFinding(SpaceObject objectSelected, Tile destinationTile, HashSet<Tile> moveRangeTiles)
	{
		m_PathFindingTiles.Clear ();
		if(moveRangeTiles.Contains(destinationTile) == true)
		{
			m_PathFindingTiles = PathFinding.FindDirectPath(objectSelected,destinationTile);
		}
	}

	public void ComputeDestinationTiles(Tile tilePointed, int clearance, HashSet<Tile> moveRangeTiles)
	{
		if(moveRangeTiles.Contains(tilePointed)==true)
		{
			Tile destinationTile = RecomputingDestination(tilePointed,clearance);
			m_DestinationTiles = Utilities.RecoverBox(destinationTile,clearance);
			foreach (Tile tile in m_DestinationTiles)
			{
				if (tile.GetFree () == false || moveRangeTiles.Contains (tile) == false) 
				{
					m_DestinationTiles.Clear();
					return;
				}
			}
		}
		else
			m_DestinationTiles.Clear();
	}

	public Tile RecomputingDestination(Tile tileBelowMouse, int clearance)
	{
		int row = 0;
		int column = 0;
		if(clearance%2 ==0)
		{
			row = tileBelowMouse.GetGridRow()+clearance/2-1;
			column = tileBelowMouse.GetGridColumn()-clearance/2-1;
		}
		else if(clearance%2 ==1)
		{
			row = tileBelowMouse.GetGridRow()-Mathf.FloorToInt(clearance/2);
			column = tileBelowMouse.GetGridColumn()-Mathf.FloorToInt(clearance/2);
		}
		
		if (row >= 0 && row < Grid2.Instance.GetMapSize () [0] && column >= 0 && column < Grid2.Instance.GetMapSize () [1])
			return Grid2.Instance.GetTilesArray () [row, column];
		else
		{
			Debug.Log("destination till out of grid");
			return null; 
		}
	}

	public HashSet<Tile> GetMoveRangeTiles()
	{
		return m_MoveRangeTiles;
	}

	public List<Tile> GetDestinationTiles()
	{
		return m_DestinationTiles;
	}

	public List<Tile> GetPathFindingTiles()
	{
		return m_PathFindingTiles;
	}


}
