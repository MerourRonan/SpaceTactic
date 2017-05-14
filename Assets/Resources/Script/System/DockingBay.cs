using UnityEngine;
using System.Collections;

public class DockingBay : ShipSystem {

	public DockingBay(GameObject myShip, int integrityMax) : base("DockingBay",myShip,integrityMax)
	{
	}
	
	public override void SystemDestroyed()
	{
		GetMyShip ().GetComponent<LargeShip> ().SetCanDock (false);
		m_SystemOnline = false;
	}
	
	public override void SystemRepared()
	{
		GetMyShip ().GetComponent<LargeShip> ().SetCanDock (true);
		m_SystemOnline = true;
	}
}
