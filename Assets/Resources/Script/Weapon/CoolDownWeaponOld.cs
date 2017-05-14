using UnityEngine;
using System.Collections;

public abstract class CoolDownWeaponOld : Weapon {

	/*protected int m_CoolDownDuration;
	protected int m_CurrentCoolDown;

	public void Awake()
	{
		base.Awake ();
	}
	
	public void Start()
	{
		base.Start ();
	}

	override protected void TurnReset()
	{
		base.TurnReset ();
		CoolDownRefresh ();
	}

	protected override void InitializeWeapon(string name, string size,string mount, Sprite HUDWeaponSprite, Sprite WeaponSprite, string info)
	{
		base.InitializeWeapon (name, size,mount, HUDWeaponSprite, WeaponSprite, info);
		//m_Type = "CoolDown";
	}

	protected void InitializeStats(int damage, int rangeMin, int rangeMax, float accuracy, float rateOfFire, int numberOfProj, string targetSize, string targetFaction, int energyDrain, int coolDownDuration, string projectileName=null)
	{
        base.InitializeStats(damage, rangeMin, rangeMax, accuracy, rateOfFire, numberOfProj, targetSize, targetFaction, energyDrain, projectileName);
		m_CoolDownDuration = coolDownDuration;
		m_CurrentCoolDown = 0;
		m_ProjectilePrefab = Resources.Load("Prefab/Projectiles/"+projectileName) as GameObject;
	}

	/*override protected bool CheckAmmoCoolDown()
	{
		if (m_CurrentCoolDown <= 0)
			return true;
		else
			return false;
	}

	protected void ActiveCoolDown()
	{
		m_CurrentCoolDown = m_CoolDownDuration;
	}

	protected void CoolDownRefresh()
	{
		m_CurrentCoolDown -= 1;
		if(m_CurrentCoolDown <=0)
		{
			m_CurrentCoolDown = 0;
		}
	}

	protected void CoolDownRefresh(int arg)
	{
		m_CurrentCoolDown -= arg;
		if(m_CurrentCoolDown <=0)
		{
			m_CurrentCoolDown = 0;
		}
		if(m_CurrentCoolDown > m_CoolDownDuration)
		{
			m_CurrentCoolDown = m_CoolDownDuration;
		}
	}*/
}
