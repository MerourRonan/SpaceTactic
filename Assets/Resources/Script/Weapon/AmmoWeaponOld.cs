using UnityEngine;
using System.Collections;

public class AmmoWeaponOld : Weapon {

	/*protected int m_AmmoMax;
	protected int m_AmmoCurrent;
	protected int m_AmmoConso;

	public void Awake()
	{
		base.Awake ();
	}
	
	public void Start()
	{
		base.Start ();
	}

	protected override void InitializeWeapon(string name,string size,string mount, Sprite HUDWeaponSprite, Sprite WeaponSprite, string info)
	{
		base.InitializeWeapon (name, size,mount, HUDWeaponSprite, WeaponSprite, info);
		//m_Type = "Ammo";
	}

	protected void InitializeStats(int damage, int rangeMin, int rangeMax, float accuracy, float rateOfFire, int numberOfProj, string targetSize, string targetFaction, int energyDrain, int ammoMax, int ammoConso, string projectileName=null)
	{
        base.InitializeStats(damage, rangeMin, rangeMax, accuracy, rateOfFire, numberOfProj, targetSize, targetFaction, energyDrain,projectileName);
		m_AmmoMax = ammoMax;
		m_AmmoCurrent = m_AmmoMax;
		m_AmmoConso = ammoConso;
		m_ProjectilePrefab = Resources.Load("Prefab/Projectiles/"+projectileName) as GameObject;
	}

/*	protected override bool CheckAmmoCoolDown()
	{
		if (m_AmmoConso <= m_AmmoCurrent) {
			return true;
		} else
			return false;
	}

	public void SetAmmo(string arg, int value=0)
	{
		if(arg == "Max")
		{
			m_AmmoMax += value;
		}
		else if(arg == "Current")
		{
			m_AmmoCurrent += value;
			if(m_AmmoCurrent > m_AmmoMax)
			{
				m_AmmoCurrent = m_AmmoMax;
			}
		}
		else if(arg == "Reset")
		{
			m_AmmoCurrent = m_AmmoMax;
		}
	}

/*	public void GetAmmo(string arg, int value=0)
	{
		if(arg == "Max")
		{
			return m_AmmoMax;
		}
		else if(arg == "Current")
		{
			return m_AmmoCurrent;
		}
		else
		{
			Debug.LogError("Error : wrong argument");
			return 0;
		}
	}*/
}
