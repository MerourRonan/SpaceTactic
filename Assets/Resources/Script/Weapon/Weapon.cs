using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Weapon : MonoBehaviour{

	protected SpaceObject m_SpaceObject;
	//protected GameObject m_WeaponBody;

	/**** Weapon Properties ****/
	protected string m_Name;
	protected string m_Size; // Small, Medium, Large
	protected string m_Info;
	protected Sprite m_WeaponSprite;
	protected Sprite m_HUDWeaponSprite;
	protected int m_EnergyDrain;
	public bool HasBeenUsed;
	
	/**** Targeting Properties ****/
	protected int m_RangeMax;
	protected int m_RangeMin;
	protected float m_Accuracy;
	protected string m_TargetSize; // Small, Medium, Large
	protected string m_TargetFaction; // Same/Other

	/* Ammo Weapon Features */
	protected bool m_IsAmmoWeapon;
	protected Sprite m_AmmoSprite;
	protected int m_AmmoMax;
	protected int m_AmmoCurrent;
	protected int m_AmmoConso;

	/* CoolDown Weapon Features */
	protected bool m_IsCooldownWeapon;
	protected int m_CoolDownDuration;
	protected int m_CurrentCoolDown;

	/**** Effect Properties ****/
	protected string m_EffectType;	//Damage, Repair, Buff, etc...
	protected string m_EffectName; //Missile, Laser, Stun, etc...

	/**** Armor Damage properties ****/
	protected float m_Damage;
	protected float m_SystemDamage;
	protected float m_ArmorDamageMultiplier;
	protected float m_ShieldDamageMultiplier;
	protected float m_SystemDamageMultiplier;

	/**** Special Properties ****/
	protected int m_ArmorPiercing;
	protected bool m_IgnoreShield;

	/**** Can Damage Properties ****/
	protected bool m_CanDamageArmor;
	protected bool m_CanDamageShield;
	protected bool m_CanDamageSystem;

	/**** Repare Properties ****/
	protected float m_ArmorRepare;
	protected float m_ShieldRepare;
	protected float m_SystemRepare;
	protected float m_ArmorRepareMultiplier;
	protected float m_ShieldRepareMultiplier;
	protected float m_SystemRepareMultiplier;

	/**** Buff Properties ****/
	protected Buff m_BuffEffect;

	/**** Displayer ****/
	protected WeaponAttackDisplayer m_WeaponAttackDisplayer;
	protected WeaponAnimation m_WeaponAnimation;


	/**** Unity Function ****/
	public void Awake()
	{
		m_SpaceObject = transform.parent.parent.parent.GetComponent<SpaceObject>();
		//m_WeaponBody = m_SpaceObject.transform.Find ("Body").gameObject;

		m_AmmoSprite = Resources.Load<Sprite>("Sprite/HUDIcone/AmmoSprite");
		m_WeaponAttackDisplayer = transform.GetComponent<WeaponAttackDisplayer> ();
	}

	public void Start()
	{
	}


	protected virtual void TurnReset()
	{
		HasBeenUsed = false;
	}

	public void InitialiseWeaponProperties(string name, string size, int energyDrain, Sprite HUDWeaponSprite, Sprite WeaponSprite, string info)
	{
		m_Name = name;
		m_Size = size;
		m_HUDWeaponSprite = HUDWeaponSprite;
		m_WeaponSprite = WeaponSprite;
		m_Info = info;
		m_EnergyDrain = energyDrain;
		HasBeenUsed = false;
	}
	public void InitialiseTargetingProperties(int rangeMin, int rangeMax, float accuracy, string targetSize, string targetFaction)
	{
		m_RangeMin = rangeMin;
		m_RangeMax = rangeMax;
		m_Accuracy = accuracy;
		m_TargetSize = targetSize;
		m_TargetFaction = targetFaction;
	}
	protected void InitializeAmmoProperties(bool isAmmoWeapon, int ammoMax=0, int ammoConso=0)
	{
		m_IsAmmoWeapon = isAmmoWeapon;
		m_AmmoMax = ammoMax;
		m_AmmoCurrent = m_AmmoMax;
		m_AmmoConso = ammoConso;
	}
	protected void InitializeCoolDownProperties(bool isCoolDownWeapon,int coolDownDuration=0)
	{
		m_IsCooldownWeapon = true;
		m_CoolDownDuration = coolDownDuration;
		m_CurrentCoolDown = 0;
	}
	protected void InitializeEffectProperties(string effectType, string effectName, Buff buffEffect = null)
	{
		m_EffectType = effectType;
		m_EffectName = effectName;
		m_BuffEffect = buffEffect;
	}
	protected void InitializeCanDamageProperties(bool canDamageShield, bool canDamageArmor, bool canDamageSystem)
	{
		m_CanDamageArmor = canDamageArmor;
		m_CanDamageShield = canDamageShield;
		m_CanDamageSystem = canDamageSystem;
	}
	protected void InitializeDamageProperties(float damage, float systemDamage, float shieldMultiplier, float armorMultiplier, float systemMultiplier)
	{
		m_Damage = damage;
		m_SystemDamage = systemDamage;
		m_ShieldDamageMultiplier = shieldMultiplier;
		m_ArmorDamageMultiplier = armorMultiplier;
		m_SystemDamageMultiplier = systemMultiplier;
	}
	protected void InitializeSpecialProperties(int armorPiercing, bool ignoreShield)
	{
		m_ArmorPiercing = armorPiercing;
		m_IgnoreShield = ignoreShield;
	}

	protected void InitializeRepareProperties(float shieldRepare, float armorRepare, float systemRepare, float shieldMultiplier, float armorMultiplier, float systemMultiplier)
	{
		m_ShieldRepare = shieldRepare;
		m_ArmorRepare = armorRepare;
		m_SystemRepare = systemRepare;
		m_ShieldRepareMultiplier = shieldMultiplier;
		m_ArmorRepareMultiplier = armorMultiplier;
		m_SystemRepareMultiplier = systemMultiplier;
	}
	protected virtual void InitializeAnimationProperties(string mount, float rateOfFire, int numberOfProj, string projectileName =null)
	{
		m_WeaponAnimation = transform.GetComponent<WeaponAnimation> ();
		m_WeaponAnimation.SetWeapon (this);
		m_WeaponAnimation.SetMount (mount);
		m_WeaponAnimation.SetRateOfFire (rateOfFire);
		m_WeaponAnimation.SetNumberOfProj (numberOfProj);
		m_WeaponAnimation.SetProjectilePrefab(Resources.Load("Prefab/Projectiles/"+projectileName) as GameObject);
	}
	
	public bool CheckAmmo()
	{
		if(m_IsAmmoWeapon == true)
		{
			if (m_AmmoConso <= m_AmmoCurrent) 
				return true;
			else
				return false;
		}
		else
			return true;
	}
	public bool CheckCoolDown()
	{
		if (m_IsCooldownWeapon == true) 
		{
			if (m_CurrentCoolDown <= 0)
				return true;
			else
				return false;
		}
		else
			return true;
	}
	public virtual void ComputeEquipmentScore ()
	{

	}
	public void UseAmmo()
	{
		if(m_IsAmmoWeapon == true)
		{
			m_AmmoCurrent-=m_AmmoConso;
		}
	}
	public void UseCoolDown()
	{
		if(m_IsCooldownWeapon == true)
		{
			m_CurrentCoolDown = m_CoolDownDuration;
		}
	}
	protected bool EnoughEnergy()
	{
		if (m_SpaceObject.GetEnergyPoints ("Current") >= m_EnergyDrain) {
			return true;
		} else {
			return false;
		}
	}
	protected bool ValidTargetSize(SpaceObject target)
	{
		//Debug.Log (target.name);
		string objectType = target.GetObjectType ();
		if (objectType == "Decor")
		{
			return false;
		}
		else if(objectType == "Ship")
		{
			if(m_TargetSize == "Small")
			{
				return true;
			}
			else if(m_TargetSize == "Medium" && target.GetComponent<Ship>().GetSize() != "Small")
			{
				return true;
			}
			else if(m_TargetSize == "Large" && target.GetComponent<Ship>().GetSize() == "Large")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
	public bool ValidTargetFaction(SpaceObject target)
	{
		if (m_TargetFaction == "Other" && target.GetComponent<SpaceObject> ().GetObjectType () == "Decor")
			return true;
		if ((m_TargetFaction == "Same" && target.GetComponent<SpaceObject> ().GetFaction () == m_SpaceObject.GetFaction ())
		    || (m_TargetFaction == "Other" && target.GetComponent<SpaceObject> ().GetFaction () != m_SpaceObject.GetFaction ())
			|| (m_TargetFaction == "Self" && target.gameObject == m_SpaceObject)) {
			return true;
		}
		else
		{
			return false;
		}
	}
	public bool IsValidTarget(SpaceObject target)
	{
		if (HasBeenUsed == false
			&& EnoughEnergy () == true
			&& ValidTargetSize (target) == true
			&& ValidTargetFaction (target) == true
			&& CheckAmmo () == true
		    && CheckCoolDown () == true) 
		{
			return true;
		} 
		else
			return false;
	}
	public virtual void PrepareEffect(SpaceObject target, LargeWeapon systemTargeted=null)
	{
		float overDamage = target.PrepareShieldDamage (GetShieldDamage ());
		target.PrepareArmorDamage (GetArmorDamage (overDamage), GetArmorPiercing ());
		target.PrepareAccuracy (GetAccuracy ());
		if (systemTargeted != null)
			target.PrepareSystemDamage (GetSystemDamage ());
	}
	public virtual void ApplyEffect(SpaceObject target, LargeWeapon systemTargeted=null)
	{
		target.ApplyShieldDamage (AttackManager.Instance.GetShieldDamage ());
		target.ApplyArmorDamage (AttackManager.Instance.GetArmorDamage ());
		if (systemTargeted != null)
			systemTargeted.SetIntegrity ("Current", AttackManager.Instance.GetSystemDamage ());

		HUD.Instance.ActiveDamagePanel (target, AttackManager.Instance.GetArmorDamage ());
	}
	public virtual HashSet<Tile> ComputeAttackRange(SpaceObject objectSelected)
	{
		List<Tile> startTiles = objectSelected.GetTiles();
		return Utilities.ComputeTileInDistance(startTiles,objectSelected.GetBody().transform.position ,m_RangeMin,m_RangeMax);
	}
	public virtual HashSet<Tile> ComputeAttackRange(Tile tile)
	{
		List<Tile> startTiles = new List<Tile> ();
		startTiles.Add (tile);
		//return Utilities.ComputeTileInDistance(startTiles,m_SpaceObject.GetBody().transform.position ,m_RangeMin,m_RangeMax);
		return Utilities.ComputeTileInDistance(startTiles,tile.transform.position ,m_RangeMin,m_RangeMax);
	}
	public virtual List<SpaceObject> ComputeTargetInRange(HashSet<Tile> attackRangeTiles)
	{
		//Debug.Log ("finding targets");
		List<SpaceObject> targetsInRange = new List<SpaceObject> ();
		foreach(Tile tile in attackRangeTiles)
		{
			/*if(tile.m_IsFree == false)
				Debug.Log("tile = "+tile.gameObject.name);*/
			SpaceObject spaceObjectOnTile = tile.GetSpaceObjectOnTile();
			/*if(spaceObjectOnTile != null && targetsInRange.Contains(spaceObjectOnTile) == false)
				Debug.Log("target = " + spaceObjectOnTile.gameObject.name + " / valide target = " + IsValidTarget(spaceObjectOnTile));*/

			if(spaceObjectOnTile != null && targetsInRange.Contains(spaceObjectOnTile) == false && IsValidTarget(spaceObjectOnTile) == true)
				targetsInRange.Add(spaceObjectOnTile);
		}
		return targetsInRange;
	}
	public virtual float ComputeScore(SpaceObject target=null)
	{
		PrepareEffect (target);
		float shieldArmorPoints = target.GetShieldPoints () + target.GetArmorPoints ("Current");
		float damagePoints = AttackManager.Instance.GetShieldDamage () + AttackManager.Instance.GetArmorDamage ();
		float score = Mathf.Ceil (((Mathf.Clamp(5 * damagePoints / shieldArmorPoints,0,5)) + (Mathf.Clamp(AttackManager.Instance.GetAccuracy () * 5 / 100,0,5)))/2);
		return score;
	}


	/****************** Ammo Weapon Function ******************/
	public bool IsAmmoWeapon()
	{
		return m_IsAmmoWeapon;
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
	public int GetAmmo(string arg)
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
	}
	public Sprite GetAmmoSprite()
	{
		return m_AmmoSprite;
	}

	/****************** CoolDown Weapon Function ******************/
	public bool IsCoolDownWeapon()
	{
		return m_IsCooldownWeapon;
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
	}

	/**** Set ****/
	public void SetDamage(string arg, float value)
	{
		if(arg == "Initialize")
		{
			if(value >=0)
				m_Damage=Mathf.RoundToInt(value);
		}
		else if(arg == "*")
			m_Damage = Mathf.RoundToInt(m_Damage*value);
		else if(arg == "+")
			m_Damage += Mathf.RoundToInt(value);
		else if(arg == "-")
			m_Damage -= Mathf.RoundToInt(value);
		else if(arg == "/")
			m_Damage = Mathf.RoundToInt(m_Damage/value);
		else
			Debug.LogError ("invalid argument");
		
		if(m_Damage < 0)
			m_Damage = 0;
	}

	/**** Get ****/
	public int GetCoolDown(string arg)
	{
		if(arg == "Max")
		{
			return m_CoolDownDuration;
		}
		else if(arg == "Current")
		{
			return m_CurrentCoolDown;
		}
		else
		{
			Debug.LogError("Error : wrong argument");
			return 0;
		}
	}
	public string GetEffectType()
	{
		return m_EffectType;
	}
	public string GetEffectName()
	{
		return m_EffectName;
	}
	public SpaceObject GetSpaceObject()
	{
		return m_SpaceObject;
	}
	public string GetName()
	{
		return m_Name;
	}
	public string GetInfo()
	{
		return m_Info;
	}
	public int GetRange(string arg)
	{
		if(arg == "Max")
		{
			return m_RangeMax;
		}
		else if(arg == "Min")
		{
			return m_RangeMin;
		}
		else
		{
			Debug.Log("invalid argument");
			return 0;
		}
	}
	public float GetAccuracy()
	{
		return m_Accuracy;
	}
	public int GetEnergyDrain()
	{
		return m_EnergyDrain;
	}
	public Sprite GetSprite(string arg)
	{
		if (arg == "HUD")
			return m_HUDWeaponSprite;
		else if (arg == "Weapon")
			return m_WeaponSprite;
		else
		{
			Debug.LogError("invalid argument");
			return null;
		}
	}
	public string GetTargetFaction()
	{
		return m_TargetFaction;
	}
	public string GetSize()
	{
		return m_Size;
	}
	public float GetDamage()
	{
		return m_Damage;
	}
	public float GetArmorDamage()
	{
		if (m_CanDamageArmor)
			return m_Damage * m_ArmorDamageMultiplier;
		else
			return 0;
	}
	public float GetArmorDamage(float overDamage)
	{
		if (m_CanDamageArmor)
			return overDamage * m_ArmorDamageMultiplier/m_ShieldDamageMultiplier;
		else
			return 0;
	}
	public float GetShieldDamage()
	{
		if (m_CanDamageShield)
			return m_Damage * m_ShieldDamageMultiplier;
		else
			return 0;
	}
	public float GetSystemDamage()
	{
		return m_SystemDamage*m_SystemDamageMultiplier;
	}
	public float GetArmorDamageMultiplier()
	{
		return m_ArmorDamageMultiplier;
	}
	public float GetShieldDamageMultiplier()
	{
		return m_ShieldDamageMultiplier;
	}
	public int GetArmorPiercing()
	{
		return m_ArmorPiercing;
	}
	public float GetArmorRepare()
	{
		return m_ArmorRepare;
	}
	public float GetShieldRepare()
	{
		return m_ShieldRepare;
	}
	public float GetSystemRepare()
	{
		return m_SystemRepare;
	}
	public float GetArmorRepareMultiplier()
	{
		return m_ArmorRepareMultiplier;
	}
	public float GetShieldRepareMultiplier()
	{
		return m_ShieldRepareMultiplier;
	}
	public Buff GetBuffEffect()
	{
		return m_BuffEffect;
	}
	public WeaponAttackDisplayer GetWeaponAttackDisplayer()
	{
		return m_WeaponAttackDisplayer;
	}
	public WeaponAnimation GetWeaponAnimation()
	{
		return m_WeaponAnimation;
	}
	public bool CanDamageSystem()
	{
		return m_CanDamageSystem;
	}
	/*public GameObject GetWeaponBody()
	{
		return m_WeaponBody;
	}*/


}
