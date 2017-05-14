using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThreatZone {

	protected List<GameObject> m_TilesClearanceLevelOne;
	protected List<GameObject> m_TilesClearanceLevelTwo;
	protected List<GameObject> m_TilesClearanceLevelThree;

	public ThreatZone()
	{
		m_TilesClearanceLevelOne = new List<GameObject> ();
		m_TilesClearanceLevelTwo = new List<GameObject> ();
		m_TilesClearanceLevelThree = new List<GameObject> ();
	}

	public List<GameObject> GetTilesClearance(int clearanceLevel)
	{
		switch(clearanceLevel)
		{
			case 1:
				return m_TilesClearanceLevelOne;
			case 2:
				return m_TilesClearanceLevelTwo;
			case 3:
				return m_TilesClearanceLevelThree;
			default :
				return null;
		}
	}

	public void SetTilesClearance(GameObject tile , int clearanceLevel)
	{
		switch(clearanceLevel)
		{
		case 1:
			m_TilesClearanceLevelOne.Add(tile);
			return;
		case 2:
			m_TilesClearanceLevelTwo.Add(tile);
			return;
		case 3:
			m_TilesClearanceLevelThree.Add(tile);
			return;
		default :
			Debug.LogError("invalid clearance level");
			return;
		}
	}
}
