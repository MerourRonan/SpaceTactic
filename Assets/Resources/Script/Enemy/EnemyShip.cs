using UnityEngine;
using System.Collections;

public class EnemyShip {

	string m_ShipName;
	float[,] m_ChanceOfSpawn;
	int m_Clearance;
	
	public EnemyShip(string shipName,int clearance, float[,] chanceOfSpawn)
	{
		m_ShipName = shipName;
		m_Clearance = clearance;
		m_ChanceOfSpawn = chanceOfSpawn;
	}
	
	public string GetShipName()
	{
		return m_ShipName;
	}

	public int GetShipClearance()
	{
		return m_Clearance;
	}
	
	public float GetChanceOfSpawn(int difficultyValue, int threatZoneValue)
	{
		return m_ChanceOfSpawn[difficultyValue,threatZoneValue];
	}
}
