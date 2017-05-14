using UnityEngine;
using System.Collections;

public class Engines : ShipSystem {

	public Engines(GameObject myShip, int integrityMax) : base("Engines",myShip,integrityMax)
	{
	}
	
	public override void SystemDestroyed()
	{
		GetShipScript().SetCanMove(false);
		m_SystemOnline = false;
	}
	
	public override void SystemRepared()
	{
		GetShipScript().SetCanMove (true);
		m_SystemOnline = true;
	}
}
