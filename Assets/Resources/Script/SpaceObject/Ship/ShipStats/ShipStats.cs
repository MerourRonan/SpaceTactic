using UnityEngine;
using System.Collections;

public class ShipStats {

	private string m_Name;
	private string m_Size;
	private string m_Class;
	private string m_HUDSpritePath;

	private int m_NumberOfSlots;
	private int m_NumberOfPassives;

	private Weapon[] m_ShipWeapons;
	private Passives[] m_ShipPassives;
	
	private int m_EnergyMax;
	private int m_ArmorMax;
	
	private int m_Evade;
	private int m_Defence;
	private int m_CritChance;
	private int m_MoveRange;

	/******************* Object Function *******************/
	protected void IntializeShip(string name, string size, string objectClass, string HUDSpritePath)
	{
		SetName (name);
		SetSize (size);
		SetClass (objectClass);
		SetSpritePath (HUDSpritePath);
	}

	protected void InitializeSlot(int nbSlots, Weapon[] shipWeapons )
	{
		SetNumberOf ("Slots", nbSlots);
		if (shipWeapons.Length <= nbSlots)
			m_ShipWeapons = shipWeapons;
		else
			Debug.LogError ("Error : shipWeapons.Length > nbSlots");
	}
	
	protected void IntializePassive(int nbPassives,Passives[] shipPassives)
	{
		SetNumberOf ("Passives", nbPassives);
		if (shipPassives.Length <= nbPassives)
			m_ShipPassives = shipPassives;
		else
			Debug.LogError ("Error : shipPassives.Length > nbPassives");
	}
	
	protected void IntializeEnergyArmor(int energy, int armor)
	{
		SetEnergyPoints (energy);
		SetArmorPoints (armor);
	}
	
	protected void InitializeStats(int evade, int defence, int critChance, int moveRange)
	{
		SetEvade (evade);
		SetDefence (defence);
		SetCriticalHitChance (critChance);
		SetMoveRange (moveRange);
	}
	
	/******************* Set Function *******************/
	public void SetName(string name)
	{
		m_Name = name;
	}
	
	public void SetSize(string size)
	{
		if(size == "Small"
		   || size == "Large")
			m_Size = size;
		else 
			Debug.LogError ("invalid argument");
	}
	
	public void SetClass(string objectClass)
	{
		m_Class = objectClass;
	}

	public void SetSpritePath(string spritePath)
	{
		m_HUDSpritePath = spritePath;
	}

	public void SetNumberOf( string arg, int value)
	{
		if (arg == "Slots")
		{
			if (value >= 0 && value <= 6)
				m_NumberOfSlots = value;
		}
		else if (arg == "Passives")
		{
			if (value >= 0 && value <= 5)
				m_NumberOfPassives = value;
		}
		else 
			Debug.LogError("invalid argument");
	}
	
	public void SetEnergyPoints(int value)
	{
		if(value >0)
		{
			m_EnergyMax = value;
		}
		else
			Debug.LogError ("invalid argument");
	}
	
	public void SetArmorPoints(int value)
	{
		if(value >0)
		{
			m_ArmorMax = value;
		}
		else
			Debug.LogError ("invalid argument");
	}
	
	public void SetEvade(int value)
	{
		if (value >= 0 && value <= 100)
			m_Evade = value;
		else
			Debug.LogError ("invalid argument");
	}
	
	public void SetDefence(int value)
	{
			if(value >=0 && value <=5)
				m_Defence=value;
		else
			Debug.LogError ("invalid argument");
	}

	public void SetCriticalHitChance(int value)
	{
		if(value >=0 && value <=100)
			m_CritChance=value;
		else
			Debug.LogError ("invalid argument");
	}

	public void SetMoveRange(int value)
	{
		if(value >=0)
			m_MoveRange = value;
		else
			Debug.LogError ("invalid argument");
	}
	
	/******************* Get Function *******************/
	public string GetName()
	{
		return m_Name;
	}
	
	public string GetSize()
	{
		return m_Size;
	}
	
	public string GetClass()
	{
		return m_Class;
	}
	
	public string GetHUDSpritePath()
	{
		return m_HUDSpritePath;
	}
	
	public int GetNumberOf(string arg)
	{
		if (arg == "Slots")
			return m_NumberOfSlots;
		else if (arg == "Passives")
			return m_NumberOfPassives;
		else {
			Debug.LogError("invalid argument");
			return 0;
		}
	}
	
	public int GetEnergyPoints()
	{
		return m_EnergyMax;
	}
	
	public int GetArmorPoints()
	{
		return m_ArmorMax;
	}
	
	public int GetEvade()
	{
		return m_Evade;
	}
	
	public int GetDefence()
	{
		return m_Defence;
	}
	
	public int GetCritChance()
	{
		return m_CritChance;
	}
	
	public int GetMoveRange()
	{
		return m_MoveRange;
	}

	public Weapon[] GetShipWeapons()
	{
		return m_ShipWeapons;
	}

	public Passives[] GetShipPassives()
	{
		return m_ShipPassives;
	}

}
