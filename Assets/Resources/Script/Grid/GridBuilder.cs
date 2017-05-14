using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridBuilder : Grid2 {

	public static GridBuilder Instance;

	protected int m_NumberOfClusterRow;
	protected int m_NumberOfClusterColumn;

	protected List<Vector2> m_PlayerSpawnClustersPosition;
	public List<Vector2> m_EnemySpawnClustersPosition;
	protected List<Vector2> m_SpecialSpawnClustersPosition;
	protected HashSet<Vector2> m_AllSpawnClustersPosition;
	

	protected void Awake()
	{
		base.Awake ();
		Instance = this;

		m_NumberOfClusterRow = 8;
		m_NumberOfClusterColumn = 8;
		MapSize = new int[2]{6*m_NumberOfClusterRow,6*m_NumberOfClusterColumn};

		m_PlayerSpawnClustersPosition = new List<Vector2> ();
		m_EnemySpawnClustersPosition = new List<Vector2> ();
		m_SpecialSpawnClustersPosition = new List<Vector2> ();
		m_AllSpawnClustersPosition = new HashSet<Vector2> ();
	}

	public void CreateClusterGrid()
	{
		Debug.Log ("mapsize[0] = " + MapSize [0]);
		Debug.Log ("mapsize[1] = " + MapSize [1]);

		SetPlayerSpawnCluster (MissionInfo2.Instance.GetMissionType());
		SetEnemySpawnCluster (MissionInfo2.Instance.GetMissionType(),MissionInfo2.Instance.GetDifficulty());
		SetSpecialSpawnCluster (MissionInfo2.Instance.GetMissionType());

		InstantiatePlayerCluster ();
		InstantiateEnemyCluster ();
		InstantiateOrdinaryCluster ();

		SetNeighboors ();
		GridPathFinder.Instance.ComputeAllPathfindingClearance ();
		DebugTileArray ();

	}

	public List<GameObject> LoadDecorObjects(string missionDecor)
	{
		GameObject emptyCluster = Resources.Load ("Prefab/Clusters/EmptyCluster") as GameObject;
		List<GameObject> ClustersToSpawn = new List<GameObject>();
		ClustersToSpawn.Add(emptyCluster);
		
		switch(missionDecor)
		{
		case "RockAsteroid":
			for(int iter = 0 ; iter<5 ; iter++)
			{
				ClustersToSpawn.Add(Resources.Load("Prefab/Clusters/RockAsteroidClusters/AsteroidCluster"+iter) as GameObject);
			}
			return ClustersToSpawn;
		case "Empty":
				ClustersToSpawn.Add(Resources.Load ("Prefab/Clusters/EmptyCluster") as GameObject);
			return ClustersToSpawn;
		default :
			Debug.LogError("invalid missionDecor : " + missionDecor);
			return null;
		}
	}

	public void InstantiatePlayerCluster()
	{
		GameObject emptyCluster = Resources.Load ("Prefab/Clusters/EmptyCluster") as GameObject;
		foreach(Vector2 clusterPosition in m_PlayerSpawnClustersPosition)
		{
			GameObject clusterInstance = Instantiate(emptyCluster,new Vector3((int)clusterPosition.x*6,0,(int)clusterPosition.y*6),Quaternion.identity) as GameObject;
			Cluster clusterScript = clusterInstance.GetComponent<Cluster>();
			clusterInstance.transform.SetParent(this.gameObject.transform.FindChild("Clusters"));
			clusterInstance.name = "Cluster("+clusterPosition.x+";"+clusterPosition.y+")";
			AddClusterToArray ((int)clusterPosition.x, (int)clusterPosition.y, clusterScript);
			AddTileToArray ((int)clusterPosition.x, (int)clusterPosition.y, clusterScript.GetClusterTiles ());
			m_PlayerSpawnClusters.Add(clusterScript);
		}
	}

	public void InstantiateEnemyCluster()
	{
		GameObject emptyCluster = Resources.Load ("Prefab/Clusters/EmptyCluster") as GameObject;
		foreach(Vector2 clusterPosition in m_EnemySpawnClustersPosition)
		{
			GameObject clusterInstance = Instantiate(emptyCluster,new Vector3((int)clusterPosition.x*6,0,(int)clusterPosition.y*6),Quaternion.identity) as GameObject;
			Cluster clusterScript = clusterInstance.GetComponent<Cluster>();
			clusterInstance.transform.SetParent(this.gameObject.transform.FindChild("Clusters"));
			clusterInstance.name = "Cluster("+clusterPosition.x+";"+clusterPosition.y+")";
			AddClusterToArray ((int)clusterPosition.x, (int)clusterPosition.y, clusterScript);
			AddTileToArray ((int)clusterPosition.x, (int)clusterPosition.y, clusterScript.GetClusterTiles ());
			m_EnemySpawnClusters.Add(clusterScript);
		}
	}

	public void InstantiateSpecialCluster()
	{

	}

	public void InstantiateOrdinaryCluster()
	{
		List<GameObject> ordinaryCluster = LoadDecorObjects (MissionInfo2.Instance.GetMissionDecor ());

		Debug.Log (m_NumberOfClusterRow + " " + m_NumberOfClusterColumn);
		for(int row =0; row<m_NumberOfClusterRow; row++)
		{
			for(int column =0; column<m_NumberOfClusterColumn; column++)
			{
				if(m_AllSpawnClustersPosition.Contains(new Vector2(row,column))==false)
				{
					Quaternion randomRotation = Quaternion.AngleAxis(Mathf.FloorToInt(Random.Range(0,4))*90,Vector3.up);
					GameObject clusterToSpawn = ChooseRandomCluster (ordinaryCluster);
					GameObject spawnedCluster = Instantiate(clusterToSpawn,new Vector3(row*6,0,column*6),randomRotation) as GameObject;
					Cluster clusterScript = spawnedCluster.GetComponent<Cluster>();
					
					spawnedCluster.transform.SetParent(this.gameObject.transform.FindChild("Clusters"));
					spawnedCluster.name = "Cluster("+row+";"+column+")";
					
					AddClusterToArray (row, column, clusterScript);
					AddTileToArray (row, column, clusterScript.GetClusterTiles ());
				}
			}
		}
	}

	void SetNeighboors()
	{
		for(int indexRow = 0; indexRow < MapSize[0] ; indexRow++)
		{
			for(int indexColumn = 0; indexColumn < MapSize[1] ; indexColumn++)
			{
				if(indexRow+1 < MapSize[0])
				{
					m_TilesArray[indexRow,indexColumn].AddNeighboors(m_TilesArray[indexRow+1,indexColumn]);
				}
				if(indexRow-1 >=0)
				{
					m_TilesArray[indexRow,indexColumn].AddNeighboors(m_TilesArray[indexRow-1,indexColumn]);
				}
				if(indexColumn+1 < MapSize[1])
				{
					m_TilesArray[indexRow,indexColumn].AddNeighboors(m_TilesArray[indexRow,indexColumn+1]);
				}
				if(indexColumn-1 >=0)
				{
					m_TilesArray[indexRow,indexColumn].AddNeighboors(m_TilesArray[indexRow,indexColumn-1]);
				}
			}
		}
	}
	
	private GameObject ChooseRandomCluster (List<GameObject> clusters)
	{
		float randomRange = 0;
		float top = 0;
		
		foreach(GameObject cluster in clusters)
		{
			randomRange+=(cluster.GetComponent<Cluster>().GetSpawnChance());
		}
		float random = UnityEngine.Random.Range (0, randomRange);
		foreach(GameObject cluster in clusters)
		{
			top+=(cluster.GetComponent<Cluster>().GetSpawnChance());
			if(random < top)
				return cluster;
		}
		Debug.LogError ("error in choose random cluster");
		return null;
	}
	
	public void AddTileToArray(int row, int column, Tile[,] clusterTiles)
	{
		if(m_TilesArray == null)
		{
			Debug.Log ("initialize tile array");
			m_TilesArray = new Tile[m_NumberOfClusterRow*6,m_NumberOfClusterColumn*6];
		}
		
		foreach(Tile tile in clusterTiles)
		{

			int globalRow = Mathf.RoundToInt(tile.transform.position.x-tile.transform.parent.localPosition.x);
			int globalColumn = Mathf.RoundToInt(tile.transform.position.z-tile.transform.parent.localPosition.z);

			tile.gameObject.name = "Tile("+globalRow+";"+globalColumn+")";
			tile.SetGridPosition(globalRow,globalColumn);
			m_TilesArray[globalRow,globalColumn] = tile;
		}
	}
	
	public void AddClusterToArray(int row, int column, Cluster cluster)
	{
		if(m_ClustersArray == null)
			m_ClustersArray = new Cluster[m_NumberOfClusterRow,m_NumberOfClusterColumn];
		Debug.Log ("row = " + row + " / column = " + column);
		m_ClustersArray [row, column] = cluster;

		cluster.SetGridRow (row);
		cluster.SetGridColumn (column);
		cluster.AddChildTiles();
	}

	public void DebugTileArray()
	{
		/*Debug.Log ("Checking null tiles");
		for(int iterRow = 0 ; iterRow < 8*6;iterRow++)
		{
			for(int iterColumn = 0 ; iterColumn < 8*6;iterColumn++)
			{
				if(m_TilesArray[iterRow,iterColumn].name == "")
				{
					Debug.Log("tile is null for row = "+iterRow+" / column = " +iterColumn);
				}
			}
		}*/
	}

	private void SetPlayerSpawnCluster(string missionType)
	{
		switch(missionType)
		{
		case "Skirmish":
			for(int row = 1; row<=2;row++)
				for(int column = 1; column<=2;column++)
				{
					Vector2 clusterPosition = new Vector2(row,column);
					m_PlayerSpawnClustersPosition.Add(clusterPosition);
					Debug.Log("adding m_AllSpawnClustersPosition");
					m_AllSpawnClustersPosition.Add(clusterPosition);
				}
			break;
		default :
			Debug.LogError("invalid missionType : " + missionType);
			break;
		}
	}

	private void SetEnemySpawnCluster(string missionType, int difficulty)
	{
		switch(missionType)
		{
		case "Skirmish":
			for(int number =0 ; number<NumberOfSpawnCoordinatesByZone(1,difficulty);number++)
			{
				Vector2 clusterPosition = GetRandomClusterPosition(1);
				m_EnemySpawnClustersPosition.Add(clusterPosition);
				m_AllSpawnClustersPosition.Add(clusterPosition);
			}
			for(int number =0 ; number<NumberOfSpawnCoordinatesByZone(2,difficulty);number++)
			{
				Vector2 clusterPosition = GetRandomClusterPosition(2);
				m_EnemySpawnClustersPosition.Add(clusterPosition);
				m_AllSpawnClustersPosition.Add(clusterPosition);
			}	
			Debug.Log("number of enemy spawn tile = " + m_EnemySpawnClustersPosition.Count);
			break;
		default :
			Debug.LogError("invalid missionType : " + missionType);
			break;
		}
	}

	private void SetSpecialSpawnCluster(string missionType)
	{
		
	}

	public int NumberOfSpawnCoordinatesByZone(int zoneNumber, int difficulty)
	{
		if(zoneNumber == 1)
			return 4;
		if (zoneNumber == 2)
			return 6;
		else
			return 0;
	}
	
	public Vector2 GetRandomClusterPosition(int zoneNumber)
	{
		int x = 0;
		int y = 0;
		if(zoneNumber == 1)
		{
			x = Random.Range(0,6);
			if(x <4)
				y = Random.Range(4,6);
			else
				y = Random.Range(0,6);
		}
		else if( zoneNumber == 2)
		{
			x = Random.Range(0,8);
			if(x <6)
				y = Random.Range(6,8);
			else
				y = Random.Range(0,8);
		}
		return new Vector2(x,y);
	}
}
