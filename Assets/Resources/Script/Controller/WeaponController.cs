using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	protected Weapon[] m_Equipments;

	void Awake()
	{
		if(transform.Find ("Body/Equipments") != null)
		m_Equipments =  transform.Find ("Body/Equipments").GetComponentsInChildren<Weapon> ();
	}

	public Weapon GetEquipment(int number)
	{
		return m_Equipments[number];
	}
	
	public Weapon[] GetEquipments()
	{
		return m_Equipments;
	}
}
