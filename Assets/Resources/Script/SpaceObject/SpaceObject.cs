using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceObject : MonoBehaviour {

	/**** Object Definition ****/
	protected string m_Name;
	protected string m_Size; 				// Small, Large
	protected string m_Class; 			// Fregate, Interceptor, Bomber , etc...
	public string m_Faction; 			// Player/Friend/Enemy/Neutral/Decor
	protected string m_ObjectType; 		// Decor, Ship
	
	/**** Controller ****/
	protected MoveController m_MoveController;
	protected BuffController m_BuffController;
	protected WeaponController m_WeaponController;
	protected PassiveController m_PassiveController;
	protected HUDController m_HUDController;

	/**** Body and Collider ****/
	protected GameObject m_Body;
	protected GameObject m_Shield;
	protected Collider m_ArmorCollider;
	protected Collider m_ShieldCollider;

	/**** Variables ****/
	protected float m_ArmorMax;
	public float m_ArmorCurrent;
	protected int m_EnergyMax;
	protected int m_EnergyCurrent;
	public float m_ShieldCurrent;
	protected float m_ArmorReductionMultiplier;
	protected float m_ShieldReductionMultiplier;
	
	protected int m_Evade;
	protected int m_Defence;
	protected int m_CritChance;
	public int m_MoveRange;
	
	/**** Control boolean ****/
	protected bool m_HasPlayed;
	protected bool m_CanPlay;

	/**** Marker ****/
	protected Marker m_TargetMarker;

	/******************* Unity Function *******************/
	protected virtual void Awake()
	{
		m_HasPlayed = false;
		m_CanPlay = true;

		m_Body = transform.Find ("Body").gameObject;
		m_ArmorCollider = m_Body.GetComponent<Collider> ();

		m_MoveController = transform.GetComponent<MoveController> ();
		m_BuffController = transform.GetComponent<BuffController> ();
		m_WeaponController = transform.GetComponent<WeaponController> ();
		m_PassiveController = transform.GetComponent<PassiveController> ();
		m_HUDController = transform.GetComponent<HUDController> ();

		m_ArmorReductionMultiplier = 1;
		m_ShieldReductionMultiplier = 1;
	}

	protected virtual void Start()
	{
		AddToGameManagerList ();
	}

	public void TurnReset()
	{
		m_HasPlayed = false;
		m_MoveController.SetHasMoved(false);
		SetEnergyPoints ("Reset");
		m_BuffController.TurnApplyBuffs ();
	}

	/******************* Object Function *******************/
	public void InitializeSpaceObject(string objectType, string name, string size, string objectClass)
	{
		SetObjectType (objectType);
		SetName (name);
		SetSize (size);
		if(GetObjectType() != "Decor")
		{
			SetClass (objectClass);
		}
	}

	public void InitializeArmor(int armorMax)
	{
		SetArmorPoints ("Initialize", armorMax);
	}

	public void InitializeHUDSprite(string spritePath)
	{
		SetHUDSprite (spritePath);
	}

	protected void AddToGameManagerList()
	{
		GameManager.Instance.AddToList (this);
	}
	
	protected void RemoveOfGameManagerList()
	{
		GameManager.Instance.RemoveToList (this);
	}

	public void PrepareAccuracy(float weaponAccuracy)
	{
		float accuracy = Mathf.Clamp (weaponAccuracy - GetEvade (), 0, 100);
		AttackManager.Instance.SetAccuracy (accuracy);
	}
	public float PrepareShieldDamage(float shieldDamage)
	{
		float overDamage = shieldDamage;
		if(GetShieldPoints () >0)
		{
			float shieldDamageTaken = shieldDamage / m_ShieldReductionMultiplier;
			overDamage = Mathf.Clamp (shieldDamageTaken - GetShieldPoints (), 0f, Mathf.Infinity);
			AttackManager.Instance.SetShieldDamage(Mathf.Round (shieldDamageTaken));
		}
		return overDamage;
	}
	public void PrepareArmorDamage(float armorDamage, int armorPiercing)
	{
		float defenceReduction =Mathf.Clamp((GetDefence () - armorPiercing),0,Mathf.Infinity)/10;
		float weaponArmorDamage = armorDamage*(1-defenceReduction);
		AttackManager.Instance.SetArmorDamage(Mathf.Round (weaponArmorDamage/m_ArmorReductionMultiplier));
	}
	public void PrepareSystemDamage(float systemDamage)
	{
		if (AttackManager.Instance.GetWeaponTargeted() != null)
			AttackManager.Instance.SetSystemDamage (systemDamage);
	}
	public void PrepareShieldRepair(float repair, float multiplier)
	{
		AttackManager.Instance.SetShieldRepair(Mathf.RoundToInt(repair*multiplier));
	}
	public void PrepareArmorRepair(float repair, float multiplier)
	{
		AttackManager.Instance.SetArmorRepair(Mathf.RoundToInt(repair*multiplier));
	}
	public void PrepareSystemRepair(float repair, float multiplier)
	{
		AttackManager.Instance.SetSystemRepair(Mathf.RoundToInt(repair*multiplier));
	}
	public void PrepareBuffToAdd(Buff buffToAdd)
	{
		AttackManager.Instance.SetBuffToAdd (buffToAdd);
	}
	public void ApplyArmorDamage(float damage)
	{
		m_ArmorCurrent -= damage;
		if (m_ArmorCurrent <= 0)
			Destroy (this.gameObject);
	}
	public void ApplyShieldDamage(float damage)
	{
		Debug.Log ("applying shield damage = " + damage);
		m_ShieldCurrent -= damage;
		if (m_ShieldCurrent <= 0)
			Destroy (m_Shield);
	}
	public void ApplyArmorRepare(float repair)
	{
		m_ArmorCurrent = Mathf.Clamp (m_ArmorCurrent + repair, 0, m_ArmorMax);
	}
	public void ApplyShieldRepare(float repair)
	{
		m_ShieldCurrent+= repair;
	}
	public void ApplyBuffToAdd(Buff buffToAdd)
	{
		GetBuffController ().AddBuff (buffToAdd);
	}

	/******************* Set Function *******************/
	public virtual void SetTiles(Tile tile)
	{
		m_MoveController.SetTiles (tile);
	}
	public void SetName(string name)
	{
		m_Name = name;
	}
	public void SetSize(string size)
	{
		if(size == "Small"
		   || size == "Large"
		   || size == "")
			m_Size = size;
		else 
			Debug.LogError ("invalid argument");
		SetClearance (m_Size);
	}
	public void SetClearance(string size)
	{
		m_MoveController.SetClearance (size);
	}
	public int GetClearance()
	{
		return m_MoveController.GetClearance();
	}
	public void SetClass(string objectClass)
	{
		m_Class = objectClass;
	}
	public void SetFaction(string faction)
	{
		if (faction == "Player"
		    || faction == "Friend"
		    || faction == "Enemy"
		    || faction == "Neutral")
		{
			m_Faction = faction;
			m_Body.tag = faction;
		}
		else
			Debug.LogError ("invalid argument");
	}
	public void SetObjectType(string objectType)
	{
		if(objectType == "Decor"
		   || objectType == "Ship")
			m_ObjectType = objectType;
		else 
			Debug.LogError ("invalid argument");

	}
	public void SetHUDSprite(string spritePath)
	{
		m_HUDController.SetHUDSprite (spritePath);
	}
	public void SetArmorPoints(string arg, float value = 0, string operation="")
	{
		if(arg == "Initialize")
		{
			m_ArmorMax = Mathf.RoundToInt(value);
			m_ArmorCurrent = Mathf.RoundToInt(value);
		}
		else if(arg == "Max")
		{
			if(operation == "+")
				m_ArmorMax += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_ArmorMax -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_ArmorMax = Mathf.RoundToInt(m_ArmorMax*value);
			else if(operation == "/")
				m_ArmorMax = Mathf.RoundToInt(m_ArmorMax/value);
		}
		else if(arg == "Current")
		{
			if(operation == "+")
				m_ArmorCurrent += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_ArmorCurrent -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_ArmorCurrent = Mathf.RoundToInt(m_ArmorCurrent*value);
			else if(operation == "/")
				m_ArmorCurrent = Mathf.RoundToInt(m_ArmorCurrent/value);
		}
		else if(arg == "Reset")
			m_ArmorCurrent = m_ArmorMax;
		else
			Debug.LogError ("invalid argument");

		if(m_ArmorCurrent > m_ArmorMax)
			m_ArmorCurrent = m_ArmorMax;
		if(m_ArmorMax < 0)
			m_ArmorMax = 0;
		if(m_ArmorCurrent < 0)
			m_ArmorCurrent = 0;
	}
	public void SetShieldPoints(float value = 0, string operation="")
	{
		
		if(operation == "+")
			m_ShieldCurrent += Mathf.RoundToInt(value);
		else if(operation == "-")
			m_ShieldCurrent -= Mathf.RoundToInt(value);
		else if(operation == "*")
			m_ShieldCurrent = Mathf.RoundToInt(m_ShieldCurrent*value);
		else if(operation == "/")
			m_ShieldCurrent = Mathf.RoundToInt(m_ShieldCurrent/value);
		
		if(m_ShieldCurrent < 0)
			m_ShieldCurrent = 0;
	}
	public void SetEvade(string arg, float value)
	{
		if(arg == "Initialize")
		{
			if(value >=0 && value <=100)
				m_Evade= Mathf.RoundToInt(value);
		}
		else if(arg == "+")
			m_Evade += Mathf.RoundToInt(value);
		else if(arg == "-")
			m_Evade -= Mathf.RoundToInt(value);
		else if(arg == "*")
			m_Evade *= Mathf.RoundToInt(value);
		else if(arg == "/")
			m_Evade /= Mathf.RoundToInt(value);
		else
			Debug.LogError ("invalid argument");
		
		if(m_Evade > 100)
			m_Evade = 100;
		else if(m_Evade < 0)
			m_Evade = 0;
	}
	public void SetDefence(string arg, float value)
	{
		if(arg == "Initialize")
		{
			if(value >=0 && value <=5)
				m_Defence=Mathf.RoundToInt(value);
		}
		else if(arg == "+")
			m_Defence += Mathf.RoundToInt(value);
		else if(arg == "-")
			m_Defence -= Mathf.RoundToInt(value);
		else if(arg == "*")
			m_Defence *= Mathf.RoundToInt(value);
		else if(arg == "/")
			m_Defence /= Mathf.RoundToInt(value);
		else
			Debug.LogError ("invalid argument");
		
		if(m_Defence > 5)
			m_Defence = 5;
		else if(m_Evade < 0)
			m_Defence = 0;
	}
	public void SetCriticalHitChance(string arg, float value)
	{
		if(arg == "Initialize")
		{
			if(value >=0 && value <=100)
				m_CritChance=Mathf.RoundToInt(value);
		}
		else if(arg == "+")
			m_CritChance += Mathf.RoundToInt(value);
		else if(arg == "-")
			m_CritChance -= Mathf.RoundToInt(value);
		else if(arg == "*")
			m_CritChance *= Mathf.RoundToInt(value);
		else if(arg == "/")
			m_CritChance /= Mathf.RoundToInt(value);
		else
			Debug.LogError ("invalid argument");
		
		if(m_CritChance > 100)
			m_CritChance = 100;
		else if(m_CritChance < 0)
			m_CritChance = 0;
	}
	public void SetMoveRange(string arg, float value)
	{
		if(arg == "Initialize")
		{
			if(value >=0)
				m_MoveRange=Mathf.RoundToInt(value);
		}
		else if(arg == "+")
			m_MoveRange += Mathf.RoundToInt(value);
		else if(arg == "-")
			m_MoveRange -= Mathf.RoundToInt(value);
		else if(arg == "*")
			m_MoveRange *= Mathf.RoundToInt(value);
		else if(arg == "/")
			m_MoveRange /= Mathf.RoundToInt(value);
		else
			Debug.LogError ("invalid argument");
		
		if(m_MoveRange < 0)
			m_MoveRange = 0;
	}
	public void SetEnergyPoints(string arg, float value = 0, string operation = "")
	{
		if(arg == "Initialize")
		{
			m_EnergyMax = Mathf.RoundToInt(value);
			m_EnergyCurrent = Mathf.RoundToInt(value);
		}
		else if(arg == "Max")
		{
			if(operation == "+")
				m_EnergyMax += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_EnergyMax -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_EnergyMax *= Mathf.RoundToInt(value);
			else if(operation == "/")
				m_EnergyMax /= Mathf.RoundToInt(value);
		}
		else if(arg == "Current")
		{
			if(operation == "+")
				m_EnergyCurrent += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_EnergyCurrent -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_EnergyCurrent *= Mathf.RoundToInt(value);
			else if(operation == "/")
				m_EnergyCurrent /= Mathf.RoundToInt(value);
		}
		else if(arg == "Reset")
		{
			m_EnergyCurrent = m_EnergyMax;
		}
		else
			Debug.LogError ("invalid argument");
		
		if(m_EnergyCurrent > m_EnergyMax)
			m_EnergyCurrent = m_EnergyMax;
		if(m_EnergyMax < 0)
			m_EnergyMax = 0;
		if(m_EnergyCurrent < 0)
			m_EnergyCurrent = 0;
	}
	public void SetTargetMarker(Marker marker)
	{
		m_TargetMarker = marker;
	}
	public void SetHasPlayed(bool arg)
	{
		m_HasPlayed = arg;
	}
	public void SetHasMoved(bool arg)
	{
		m_MoveController.SetHasMoved(arg);
	}
	public void SetCanPlay(bool arg)
	{
		m_CanPlay = arg;
	}
	public void SetReferenceTile(Tile referenceTile)
	{
		m_MoveController.SetReferenceTile (referenceTile);
	}
	public void SetCanMove(bool arg)
	{
		m_MoveController.SetCanMove(arg);
	}
	public void SetShield(GameObject shieldPrefab)
	{
		m_Shield = shieldPrefab;
		m_Shield.tag = m_Faction;
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
	public string GetFaction()
	{
		return m_Faction;
	}
	public string GetObjectType()
	{
		return m_ObjectType;
	}
	public float GetArmorPoints(string arg)
	{
		if(arg == "Max")
		{
			return m_ArmorMax;
		}
		else if(arg == "Current")
		{
			return m_ArmorCurrent;
		}
		else
		{
			return 0;
		}
	}
	public virtual List<Tile> GetTiles()
	{
		return m_MoveController.GetTiles();
	}
	public Sprite GetHUDSprite()
	{
		return m_HUDController.GetHUDSprite();
	}
	public Collider GetArmorCollider()
	{
		return m_ArmorCollider;
	}
	public Collider GetShieldCollider()
	{
		return m_ShieldCollider;
	}

	public Tile GetReferenceTile()
	{
		return m_MoveController.GetReferenceTile();
	}
	public bool GetHasMoved()
	{
		return m_MoveController.GetHasMoved();
	}
	public bool GetHasPlayed()
	{
		return m_HasPlayed;
	}
	public bool GetCanPlay()
	{
		return m_CanPlay;
	}
	public bool GetCanMove()
	{
		return m_MoveController.GetCanMove();
	}
	public GameObject GetBody()
	{
		return m_Body;
	}
	public float GetMoveSpeed()
	{
		return m_MoveController.GetMoveSpeed();
	}
	public float GetRotationSpeed()
	{
		return m_MoveController.GetRotationSpeed();
	}
	public HUDController GetHUDController()
	{
		return m_HUDController;
	}

	public int GetEnergyPoints(string arg)
	{
		if(arg == "Max")
			return m_EnergyMax;
		else if(arg == "Current")
			return m_EnergyCurrent;
		else
			return 0;
	}
	public float GetShieldPoints()
	{
		return m_ShieldCurrent;
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
	public MoveController GetMoveController()
	{
		return m_MoveController;
	}
	public BuffController GetBuffController()
	{
		return m_BuffController;
	}
	public WeaponController GetWeaponController()
	{
		return m_WeaponController;
	}
	public PassiveController GetPassiveController()
	{
		return m_PassiveController;
	}
	public Sprite GetHUDMarker()
	{
		return m_HUDController.GetHUDMarker();
	}
	public Marker GetTargetMarker()
	{
		return m_TargetMarker;
	}
}
