using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipFitting {
	
	public string m_ShipName;
	public string[] m_Equipments;
	public string[] m_Passives;

	public ShipFitting(ShipStats shipStats, string[] equipments = null, string[] passives = null)
	{
		m_ShipName = shipStats.GetName();
		if (equipments != null && equipments.Length <= shipStats.GetNumberOf ("Slots"))
			m_Equipments = equipments;
		if (passives != null && passives.Length <= shipStats.GetNumberOf ("Passives"))
			m_Passives = passives;
	}

	public void SetShipName(string name)
	{
		m_ShipName = name;
	}

	public string GetShipName()
	{
		return m_ShipName;
	}

	public Weapon[] GetEquipments()
	{
		return Utilities.FactoryEquipments(m_Equipments);
	}

	public Passives[] GetPassives()
	{
		return Utilities.FactoryPassives(m_Passives);
	}

	public GameObject GetShipToSpawn()
	{
		GameObject shipToSpawn =  Resources.Load ("Prefab/Ship/"+GetShipName()) as GameObject;
		return shipToSpawn;
	}


}
