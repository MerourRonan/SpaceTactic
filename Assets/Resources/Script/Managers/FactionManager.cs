using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactionManager : MonoBehaviour {

	public static FactionManager Instance;

	protected List<Vector2> m_SpawnCoordinatesZone1;
	protected List<Vector2> m_SpawnCoordinatesZone2;

	void Awake()
	{
		Instance = this;

		m_SpawnCoordinatesZone1 = new List<Vector2> ();
		m_SpawnCoordinatesZone2 = new List<Vector2> ();
	}

	public List<Squadron> GetFactionSquadrons(string factionName, int difficultyValue)
	{
		Faction missionFaction = FactoryFaction (factionName);
		return missionFaction.GetSquadronsList (difficultyValue);
	}

	public Faction FactoryFaction(string factionName)
	{
		switch(factionName)
		{
		case("FactionTest"):
			return new FactionTest();
				break;
		default:
			Debug.LogError("Invalid faction name = " + factionName);
			return null;
			break;
		}
	}

	
	public void SetSpawnCoordinate(string difficulty)
	{
		for(int numberOfSpawn1 =0 ; numberOfSpawn1<NumberOfSpawnCoordinatesByZone(1,difficulty);numberOfSpawn1++)
		{
			m_SpawnCoordinatesZone1.Add(GetRandomClusterPosition(1));
		}
		for(int numberOfSpawn2 =0 ; numberOfSpawn2<NumberOfSpawnCoordinatesByZone(2,difficulty);numberOfSpawn2++)
		{
			m_SpawnCoordinatesZone2.Add(GetRandomClusterPosition(2));
		}

	}
	
	public int NumberOfSpawnCoordinatesByZone(int zoneNumber, string difficulty)
	{
		if(zoneNumber == 1)
			return 2;
		if (zoneNumber == 2)
			return 3;
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
