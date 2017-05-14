using UnityEngine;
using System.Collections;

public class MissionInfo2 : MonoBehaviour {

	public static MissionInfo2 Instance;

	private string m_MissionType;
	private string m_MissionDecor;
	private int  m_Difficulty;
	private string m_EnemyFaction;

	private Fleet m_PlayerFleet;
	
	void Awake()
	{
		Instance = this;
		m_MissionType = "Skirmish";
		m_MissionDecor = "RockAsteroid";

		SetDifficulty (0);
		SetEnemyFaction ("FactionTest");

		Fleet fleet0 = new Fleet ();
		Squadron squadron1 = new Squadron ();
		squadron1.SetLargeShip ("LargePlayerShipTest2");
		squadron1.SetSmallShip (0, "SmallPlayerShipTest");
		squadron1.SetSmallShip (1, "SmallPlayerShipTest");
		squadron1.SetSmallShip (2, "SmallPlayerShipTest");
		fleet0.SetSquadron (0, squadron1);
		m_PlayerFleet = fleet0;
	}

	public void SetDifficulty(int difficulty)
	{
		m_Difficulty = difficulty;
	}
	
	public void SetEnemyFaction(string enemyFaction)
	{
		m_EnemyFaction = enemyFaction;
	}
	
	public int GetDifficulty()
	{
		return m_Difficulty;
	}
	
	public string GetEnemyFaction()
	{
		return m_EnemyFaction;
	}
	
	public void SetMissionType(string missionType)
	{
		m_MissionType = missionType;
	}

	public void SetMissionDecor(string missionDecor)
	{
		m_MissionDecor = missionDecor;
	}

	public string GetMissionType()
	{
		return m_MissionType;
	}
	
	public string GetMissionDecor()
	{
		return m_MissionDecor;
	}

	public void SetPlayerFleet(Fleet playerFleet)
	{
		m_PlayerFleet = playerFleet;
	}

	public Fleet GetPlayerFleet()
	{
		return m_PlayerFleet;
	}
}
