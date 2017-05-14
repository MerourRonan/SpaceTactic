using UnityEngine;
using System.Collections;

public class PowerCore : ShipSystem {

	public PowerCore(GameObject myShip, int integrityMax) : base("PowerCore",myShip,integrityMax)
	{
	}
	
	public override void SystemDestroyed()
	{
		GetShipScript ().SetEnergyPoints ("Max", 2, "/");
		m_SystemOnline = false;
	}
	
	public override void SystemRepared()
	{
		GetShipScript ().SetEnergyPoints ("Max", 2, "*");
		m_SystemOnline = true;
	}
}
