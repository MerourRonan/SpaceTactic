using UnityEngine;
using System.Collections;

public class BattleInfo {

	private GameObject m_ShipAttacking;
	private GameObject m_ShipAttacked;
	private Weapon m_WeaponUsed;
	private string m_SystemTargeted;

	public BattleInfo()
	{
		m_ShipAttacking = null;
		m_ShipAttacked = null;
		m_WeaponUsed = null;
		m_SystemTargeted = "";
	}


	public void SetShipAttacking(GameObject shipAttacking)
	{
		m_ShipAttacking = shipAttacking;
	}

	public void SetShipAttacked(GameObject shipAttacked)
	{
		m_ShipAttacked = shipAttacked;
	}

	public void SetWeaponUsed(Weapon weaponUsed)
	{
		m_WeaponUsed = weaponUsed;
	}

	public void SetSystemTargeted(string system)
	{
		m_SystemTargeted = system;
	}

	public GameObject GetShipAttacking()
	{
		return m_ShipAttacking;
	}
	
	public GameObject GetShipAttacked()
	{
		return m_ShipAttacked ;
	}
	
	public Weapon GetWeaponUsed()
	{
		return m_WeaponUsed;
	}
	public string GetSystemTargeted()
	{
		return m_SystemTargeted;
	}

	public void Reset()
	{
		m_ShipAttacking = null;
		m_ShipAttacked = null;
		m_WeaponUsed = null;
		m_SystemTargeted = "";
	}
}
