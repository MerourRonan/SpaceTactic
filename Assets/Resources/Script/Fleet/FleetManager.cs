using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FleetManager : MonoBehaviour {

	public static FleetManager Instance;

	public List<SpaceObject> m_PlayerShips;
	public List<SpaceObject> m_PlayerSmallShips;
	public List<SpaceObject> m_PlayerLargeShips;
	public List<SpaceObject> m_PlayerInterceptors;
	public List<SpaceObject> m_PlayerHeavyAssault;
	public List<SpaceObject> m_PlayerLancer;
	public List<SpaceObject> m_PlayerBomber;
	public List<SpaceObject> m_PlayerSupport;
	public List<SpaceObject> m_PlayerAssaultFregate;
	public List<SpaceObject> m_PlayerArtilleryFregate;
	public List<SpaceObject> m_PlayerSupportFregate;
	public List<SpaceObject> m_PlayerAntiFighterFregate;

	public List<SpaceObject> m_EnemyShips;
	public List<SpaceObject> m_EnemySmallShips;
	public List<SpaceObject> m_EnemyLargeShips;
	public List<SpaceObject> m_EnemyInterceptors;
	public List<SpaceObject> m_EnemyHeavyAssault;
	public List<SpaceObject> m_EnemyLancer;
	public List<SpaceObject> m_EnemyBomber;
	public List<SpaceObject> m_EnemySupport;
	public List<SpaceObject> m_EnemyAssaultFregate;
	public List<SpaceObject> m_EnemyArtilleryFregate;
	public List<SpaceObject> m_EnemySupportFregate;
	public List<SpaceObject> m_EnemyAntiFighterFregate;


	public void Awake()
	{
		Instance = this;

		m_PlayerShips=new List<SpaceObject>();
		m_PlayerSmallShips=new List<SpaceObject>();
		m_PlayerLargeShips=new List<SpaceObject>();
		m_PlayerInterceptors=new List<SpaceObject>();
		m_PlayerHeavyAssault=new List<SpaceObject>();
		m_PlayerLancer=new List<SpaceObject>();
		m_PlayerBomber=new List<SpaceObject>();
		m_PlayerSupport=new List<SpaceObject>();
		m_PlayerAssaultFregate=new List<SpaceObject>();
		m_PlayerArtilleryFregate=new List<SpaceObject>();
		m_PlayerSupportFregate=new List<SpaceObject>();
		m_PlayerAntiFighterFregate=new List<SpaceObject>();
		
		m_EnemyShips=new List<SpaceObject>();
		m_EnemySmallShips=new List<SpaceObject>();
		m_EnemyLargeShips=new List<SpaceObject>();
		m_EnemyInterceptors=new List<SpaceObject>();
		m_EnemyHeavyAssault=new List<SpaceObject>();
		m_EnemyLancer=new List<SpaceObject>();
		m_EnemyBomber=new List<SpaceObject>();
		m_EnemySupport=new List<SpaceObject>();
		m_EnemyAssaultFregate=new List<SpaceObject>();
		m_EnemyArtilleryFregate=new List<SpaceObject>();
		m_EnemySupportFregate=new List<SpaceObject>();
		m_EnemyAntiFighterFregate=new List<SpaceObject>();
	}

	public void SpawnFleets()
	{
		SpawnPlayerFleet ();
		SpawnEnemyFleet ();
		GridPathFinder.Instance.ComputeAllPathfindingClearance ();
	}


	public void SpawnPlayerFleet()
	{
		Fleet playerFleet = MissionInfo2.Instance.GetPlayerFleet ();
		List<Cluster> spawnCluster = Grid2.Instance.GetPlayerSpawnClusters ();

		foreach(Cluster cluster in spawnCluster)
		{
			foreach(Squadron squadron in playerFleet.GetSquadron())
			{
				if(squadron != null && squadron.GetLargeShip() != null)
				{
					Tile shipSpawnTile = cluster.GetClusterTiles()[2,1];
					PlayerShipSpawner(squadron.GetLargeShip(),shipSpawnTile);
				}

				for(int smallShipNumber = 0; smallShipNumber<3;smallShipNumber++)
				{
					if(squadron != null && squadron.GetSmallShip(smallShipNumber) != null)
					{
						Tile shipSpawnTile = cluster.GetComponent<Cluster>().GetClusterTiles()[2+smallShipNumber,4];
						PlayerShipSpawner(squadron.GetSmallShip(smallShipNumber),shipSpawnTile);
					}	
				}
			}
		}
	}

	public void PlayerShipSpawner(string shipName, Tile tileOfSpawn)
	{
		GameObject ship = Resources.Load("Prefab/Ship/"+shipName) as GameObject;
		GameObject shipInstance = Instantiate (ship, tileOfSpawn.transform.position, Quaternion.LookRotation(-Vector3.right)) as GameObject;
		SpaceObject shipScript = shipInstance.GetComponent<SpaceObject> ();

		shipInstance.transform.Find ("Body").rotation = Quaternion.LookRotation (Vector3.forward);
		shipScript.SetFaction ("Player");
		if(shipScript.GetSize() == "Small")
			shipInstance.transform.SetParent(transform.Find("PlayerFleet/SmallShips"));
		else
			shipInstance.transform.SetParent(transform.Find("PlayerFleet/LargeShips"));

		Grid2.Instance.LockTile (tileOfSpawn, shipScript);
	}

	public void SpawnEnemyFleet()
	{
		string factionName = MissionInfo2.Instance.GetEnemyFaction ();
		int difficulty = MissionInfo2.Instance.GetDifficulty ();
		List<Squadron> enemySquadrons = FactionManager.Instance.GetFactionSquadrons (factionName, difficulty);
		int numberOfSquadrons = enemySquadrons.Count;
		List<Cluster> spawnCluster = Grid2.Instance.GetEnemySpawnClusters ();
		
		foreach(Cluster cluster in spawnCluster)
		{
			Squadron randomSquadron = enemySquadrons[Random.Range(0,numberOfSquadrons)];
			if(randomSquadron != null && randomSquadron.GetLargeShip() != null)
			{
				Tile shipSpawnTile = cluster.GetClusterTiles()[2,1];
				EnemyShipSpawner(randomSquadron.GetLargeShip(),shipSpawnTile);
			}
			
			for(int smallShipNumber = 0; smallShipNumber<3;smallShipNumber++)
			{
				if(randomSquadron != null && randomSquadron.GetSmallShip(smallShipNumber) != null)
				{
					Tile shipSpawnTile = cluster.GetClusterTiles()[2+smallShipNumber,4];
					EnemyShipSpawner(randomSquadron.GetSmallShip(smallShipNumber),shipSpawnTile);
				}	
			}
		}
	}

	public void EnemyShipSpawner(string shipName, Tile tileOfSpawn)
	{
		GameObject ship = Resources.Load("Prefab/Ship/"+shipName) as GameObject;
		GameObject shipInstance = Instantiate (ship, tileOfSpawn.transform.position, Quaternion.LookRotation(-Vector3.right)) as GameObject;
		SpaceObject shipScript = shipInstance.GetComponent<SpaceObject> ();
		
		shipInstance.transform.Find ("Body").rotation = Quaternion.LookRotation (Vector3.forward);
		shipScript.SetFaction ("Enemy");
		if(shipScript.GetSize() == "Small")
			shipInstance.transform.SetParent(transform.Find("EnemyFleet/SmallShips"));
		else
			shipInstance.transform.SetParent(transform.Find("EnemyFleet/LargeShips"));
		
		Grid2.Instance.LockTile (tileOfSpawn, shipScript);
	}

	public void AddSpaceObject(SpaceObject spaceObject)
	{
		if(spaceObject.GetFaction() == "Player")
		{
			m_PlayerShips.Add(spaceObject);
			if(spaceObject.GetSize() == "Small")
			{
				m_PlayerSmallShips.Add(spaceObject);
				if(spaceObject.GetClass() == "Interceptor")
				{
					m_PlayerInterceptors.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "HeavyAssault")
				{
					m_PlayerHeavyAssault.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "Lancer")
				{
					m_PlayerLancer.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "Support")
				{
					m_PlayerSupport.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "Bomber")
				{
					m_PlayerBomber.Add(spaceObject);
				}
				else
					Debug.LogError("invalid class : "+spaceObject.GetClass()+" for space object : " +spaceObject.gameObject.name);
			}
			else if(spaceObject.GetSize() == "Large")
			{
				m_PlayerLargeShips.Add(spaceObject);
				if(spaceObject.GetClass() == "AssaultFregate")
				{
					m_PlayerAssaultFregate.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "ArtilleryFregate")
				{
					m_PlayerArtilleryFregate.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "SupportFregate")
				{
					m_PlayerSupportFregate.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "AntiFighterFregate")
				{
					m_PlayerAntiFighterFregate.Add(spaceObject);
				}
				else
					Debug.LogError("invalid class : "+spaceObject.GetClass()+" for space object : " +spaceObject.gameObject.name);
			}
			else
				Debug.LogError("invalid size : "+spaceObject.GetSize()+" for space object : " +spaceObject.gameObject.name);

		

		}
		else if(spaceObject.GetFaction() == "Enemy")
		{
			m_EnemyShips.Add(spaceObject);
			if(spaceObject.GetSize() == "Small")
			{
				m_EnemySmallShips.Add(spaceObject);
				if(spaceObject.GetClass() == "Interceptor")
				{
					m_EnemyInterceptors.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "HeavyAssault")
				{
					m_EnemyHeavyAssault.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "Lancer")
				{
					m_EnemyLancer.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "Support")
				{
					m_EnemySupport.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "Bomber")
				{
					m_EnemyBomber.Add(spaceObject);
				}
				else
					Debug.LogError("invalid class : "+spaceObject.GetClass()+" for space object : " +spaceObject.gameObject.name);
			}
			else if(spaceObject.GetSize() == "Large")
			{
				m_EnemyLargeShips.Add(spaceObject);
				m_EnemyLargeShips.Add(spaceObject);
				if(spaceObject.GetClass() == "AssaultFregate")
				{
					m_EnemyAssaultFregate.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "ArtilleryFregate")
				{
					m_EnemyArtilleryFregate.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "SupportFregate")
				{
					m_EnemySupportFregate.Add(spaceObject);
				}
				else if(spaceObject.GetClass() == "AntiFighterFregate")
				{
					m_EnemyAntiFighterFregate.Add(spaceObject);
				}
				else
					Debug.LogError("invalid class : "+spaceObject.GetClass()+" for space object : " +spaceObject.gameObject.name);
			}
			else
				Debug.LogError("invalid size : "+spaceObject.GetSize()+" for space object : " +spaceObject.gameObject.name);
		}
		else
			Debug.LogError("invalid faction : "+spaceObject.GetFaction()+" for space object : " +spaceObject.gameObject.name);
	}

	public List<SpaceObject> GetShipList(string faction,string shipClass)
	{
		if (faction == "Player") 
		{
			if(shipClass == "Interceptor")
			{
				return m_PlayerInterceptors;
			}
			else if(shipClass == "HeavyAssault")
			{
				return m_PlayerHeavyAssault;
			}
			else if(shipClass == "Lancer")
			{
				return m_PlayerLancer;
			}
			else if(shipClass == "Support")
			{
				return m_PlayerSupport;
			}
			else if(shipClass == "Bomber")
			{
				return m_PlayerBomber;
			}
			else if(shipClass == "AssaultFregate")
			{
				return m_PlayerAssaultFregate;
			}
			else if(shipClass == "ArtilleryFregate")
			{
				return m_PlayerArtilleryFregate;
			}
			else if(shipClass == "SupportFregate")
			{
				return m_PlayerSupportFregate;
			}
			else if(shipClass == "AntiFighterFregate")
			{
				return m_PlayerAntiFighterFregate;
			}
			else
			{
				Debug.LogError("invalid class : "+shipClass);
				return null;
			}
		}
		else if(faction == "Enemy")
		{
			if(shipClass == "Interceptor")
			{
				return m_EnemyInterceptors;
			}
			else if(shipClass == "HeavyAssault")
			{
				return m_EnemyHeavyAssault;
			}
			else if(shipClass == "Lancer")
			{
				return m_EnemyLancer;
			}
			else if(shipClass == "Support")
			{
				return m_EnemySupport;
			}
			else if(shipClass == "Bomber")
			{
				return m_EnemyBomber;
			}
			else if(shipClass == "AssaultFregate")
			{
				return m_EnemyAssaultFregate;
			}
			else if(shipClass == "ArtilleryFregate")
			{
				return m_EnemyArtilleryFregate;
			}
			else if(shipClass == "SupportFregate")
			{
				return m_EnemySupportFregate;
			}
			else if(shipClass == "AntiFighterFregate")
			{
				return m_EnemyAntiFighterFregate;
			}
			else
			{
				Debug.LogError("invalid class : "+shipClass);
				return null;
			}
		}
		return null;

	}
}
