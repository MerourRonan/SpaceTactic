using UnityEngine;
using System.Collections;

public class ControlTower : ShipSystem {

	public ControlTower(GameObject myShip, int integrityMax) : base("ControlTower",myShip, integrityMax)
	{
	}

	public override void SystemDestroyed()
	{
		foreach(Weapon weapon in GetShipScript().GetWeaponController().GetEquipments())
		{
			weapon.SetDamage("/",2);
		}
		m_SystemOnline = false;
	}

	public override void SystemRepared()
	{
		foreach(Weapon weapon in GetShipScript().GetWeaponController().GetEquipments())
		{
			weapon.SetDamage("*",2);
		}
		m_SystemOnline = true;
	}
}
