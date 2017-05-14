using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid2 : MonoBehaviour {

	public static Grid2 Instance;

	public static Tile[,] m_TilesArray;
	protected static Cluster[,] m_ClustersArray;
	protected static List<Cluster> m_PlayerSpawnClusters;
	public static List<Cluster> m_EnemySpawnClusters;
	//protected static List<GameObject> m_ThreatZone1;
	//protected static List<GameObject> m_ThreatZone2;
	protected static HashSet<Tile> m_FreeTiles;
	protected static HashSet<Tile> m_OccupiedTiles;
	protected static int m_FreeTilesNumber;
	
	protected static int[] MapSize;

	protected static LineRenderer LinePath;

	protected void Awake()
	{
		Instance = this;
		m_PlayerSpawnClusters = new List<Cluster>();
		m_EnemySpawnClusters = new List<Cluster>();
	//	m_ThreatZone1 = new List<GameObject>();
		//m_ThreatZone2 = new List<GameObject>();
		m_FreeTilesNumber = 1296;
		LinePath = transform.GetComponent<LineRenderer>();
		LinePath.enabled = false;
	}
	
	public void AddFreeTile(Tile tile)
	{
		if (m_FreeTiles == null)
			m_FreeTiles = new HashSet<Tile> ();
		if (m_OccupiedTiles == null)
			m_OccupiedTiles = new HashSet<Tile> ();

		if(m_FreeTiles.Add (tile)==true && m_FreeTilesNumber < 1296)
			//m_FreeTilesNumber++;
		
		m_OccupiedTiles.Remove (tile);
	}

	public void AddOccupiedTile(Tile tile)
	{
		if (m_FreeTiles == null)
			m_FreeTiles = new HashSet<Tile> ();
		if (m_OccupiedTiles == null)
			m_OccupiedTiles = new HashSet<Tile> ();

		if(m_OccupiedTiles.Add (tile)==true && m_FreeTilesNumber >0)
			//m_FreeTilesNumber--;

		m_FreeTiles.Remove (tile);
	}

	public void LockTile(Tile referenceTile, SpaceObject spaceObject)
	{
		List<Tile> lockTileList = Utilities.RecoverClearanceBox(referenceTile,spaceObject.GetClearance());
		spaceObject.SetReferenceTile (referenceTile);
		spaceObject.GetTiles ().Clear ();
		foreach(Tile lockTile in lockTileList)
		{
			lockTile.SetFree(false,spaceObject.gameObject);
			spaceObject.SetTiles(lockTile);
			GridPathFinder.Instance.ComputeNeighboorsClearance(lockTile);
		}
	}

	public void ReleaseTile(Tile referenceTile, SpaceObject spaceObject)
	{
		Debug.Log ("releasing tile = " + referenceTile.gameObject.name);
		List<Tile> releaseTiles = Utilities.RecoverClearanceBox(referenceTile,spaceObject.GetClearance());
		spaceObject.GetTiles ().Clear ();
		foreach(Tile releaseTile in releaseTiles)
		{
			releaseTile.SetFree(true);
			GridPathFinder.Instance.ComputeTileClearance(releaseTile);
			GridPathFinder.Instance.ComputeNeighboorsClearance(releaseTile);
		}
	}
	public void ActiveTile(bool active)
	{
		foreach (Tile tile in GetTilesArray())
			tile.gameObject.SetActive (active);
	}

	public Tile[,] GetTilesArray()
	{
		return m_TilesArray;
	}

	public int[] GetMapSize()
	{

		return MapSize;
	}

	public List<Cluster> GetPlayerSpawnClusters()
	{
		return m_PlayerSpawnClusters;
	}

	public List<Cluster> GetEnemySpawnClusters()
	{
		return m_EnemySpawnClusters;
	}

	/*public List<GameObject> GetThreatZone(int number)
	{
		switch(number)
		{
		case(1):
			return m_ThreatZone1;
			break;
		case(2):
			return m_ThreatZone2;
			break;
		default:
			Debug.LogError("Invalid threat zone number = " + number);
			return null;
			break;
		}
	}*/




}
