using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squadron {

	//int m_SquadronNumber;
	public string m_LargeShip;
	public string[] m_SmallShips;

	public Squadron()
	{
		m_LargeShip = "";
		m_SmallShips = new string[3]{null,null,null};
	}

	public Squadron(string largeShipName, string smallShipName1, string smallShipName2, string smallShipName3)
	{
		m_LargeShip = largeShipName;
		m_SmallShips = new string[3]{smallShipName1,smallShipName2,smallShipName3};
	}

	public void SetSmallShip(int number, string shipName)
	{
		if(number >= 0 && number < 4)
		{
			m_SmallShips[number] = shipName;
		}
		else
		{
			Debug.Log("ERROR : invalid argument");
		}
		
	}

	public void SetLargeShip(string shipName)
	{
		m_LargeShip = shipName;
		
	}

	public string GetSmallShip(int number)
	{
		if(number >= 0 && number < 4)
		{
			return m_SmallShips[number];
		}
		else
		{
			Debug.Log("ERROR : invalid argument");
			return null;
		}
	}
	public string GetLargeShip()
	{
		return m_LargeShip;
		
	}

	public string[] GetSmallShips()
	{
		return m_SmallShips;
	}


	// fonction bouchon

	public string GetShips(int number)
	{
		return null;
	}

	public string[] GetShips()
	{
		return null;
	}
}
