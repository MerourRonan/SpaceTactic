using UnityEngine;
using System.Collections;

public abstract class ShipSystem {

	private GameObject m_MyShip;
	private Ship m_ShipScript;

	private string m_SystemName;
	private Sprite m_SystemSprite;
	private int m_IntegrityMax;
	private int m_IntegrityCurrent;
	protected bool m_SystemOnline;

	public ShipSystem(string systemName,GameObject myShip, int integrityMax = 0)
	{
		SetShip (myShip);
		m_SystemName = systemName;
		m_SystemOnline = true;
		SetIntegrity ("Initialize", integrityMax);

	}

	public abstract void SystemDestroyed();
	public abstract void SystemRepared();

	public void SetIntegrity(string arg, int value=0, string operation = "")
	{
		if(arg == "Initialize")
		{
			m_IntegrityMax = value;
			m_IntegrityCurrent = value;
		}
		else if(arg == "Max")
		{
			if(operation == "+")
				m_IntegrityMax += value;
			else if(operation == "-")
				m_IntegrityMax -= value;
		}
		else if(arg == "Current")
		{
			if(operation == "+")
				m_IntegrityCurrent += value;
			else if(operation == "-")
				m_IntegrityCurrent -= value;
		}
		else if(arg == "Reset")
			m_IntegrityCurrent = m_IntegrityMax;
		else
			Debug.LogError ("invalid argument");
		
		if(m_IntegrityCurrent > m_IntegrityMax)
			m_IntegrityCurrent = m_IntegrityMax;
		if(m_IntegrityMax < 0)
			m_IntegrityMax = 0;
		if (m_IntegrityCurrent <= 0) 
		{
			m_IntegrityCurrent = 0;
			if(m_SystemOnline == true)
			{
				SystemDestroyed ();
			}
		}
		else if (m_IntegrityCurrent <= m_IntegrityMax) 
		{
			m_IntegrityCurrent = m_IntegrityMax;
			if(m_SystemOnline == false)
			{
				SystemRepared ();
			}
		}
	}

	public void SetSystemName(string name)
	{
		if (name == "Engines"
			|| name == "ControlTower"
			|| name == "DockingBay"
			|| name == "PowerCore") 
			m_SystemName = name;
		else
			Debug.LogError ("invalid argument");

	}

	public void SetShip(GameObject ship)
	{
		m_MyShip = ship;
		m_ShipScript = ship.GetComponent<Ship> ();
	}
	
	public int GetIntegrity(string arg)
	{
		if (arg == "Max")
			return m_IntegrityMax;
		else if (arg == "Current")
			return m_IntegrityCurrent;
		else
		{
			Debug.LogError("invalid argument");
			return 0;
		}
	}

	public string GetName()
	{
		return m_SystemName;
	}

	public Ship GetShipScript()
	{
		return m_ShipScript;
	}

	public GameObject GetMyShip()
	{
		return m_MyShip;
	}


}
