using UnityEngine;
using System.Collections;

public class LargeShip : Ship {


	ShipSystem[] m_ShipSystems;
	bool CanDock;

	protected override void Awake()
	{
		base.Awake ();
	}

	public void SetCanDock(bool arg)
	{
		CanDock = arg;
	}

	public ShipSystem GetSystem(string systemName)
	{

		if(systemName == "Engines")
			return m_ShipSystems[0];
		else if(systemName == "ControlTower")
			return m_ShipSystems[1];
		else if(systemName == "DockingBay")
			return m_ShipSystems[2];
		else if(systemName == "PowerCore")
			return m_ShipSystems[3];
		else
		{
			Debug.LogError("invalid argument : systemName");
			return null;
		}
	}

	public bool GetCanDock()
	{
		return CanDock;
	}
}

