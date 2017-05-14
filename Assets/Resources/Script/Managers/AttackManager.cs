using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackManager : MonoBehaviour {

	public static AttackManager Instance;

	protected HashSet<Tile> m_AttackRangeTiles;
	protected List<SpaceObject> m_TargetsInRange;

	protected SpaceObject m_SpaceObjectTargeted;
	protected LargeWeapon m_WeaponTargeted;
	protected int m_TargetNumber;
	protected int m_WeaponNumber;

	protected float m_Accuracy;
	protected float m_ArmorDamage;
	protected float m_ArmorRepair;
	protected float m_ShieldDamage;
	protected float m_ShieldRepair;
	protected float m_SystemDamage;
	protected float m_SystemRepair;
	protected Buff m_BuffToAdd;
	protected bool m_Hit;

	void Awake()
	{
		Instance = this;
		m_AttackRangeTiles = new HashSet<Tile> ();
		m_TargetsInRange = new List<SpaceObject>();
		m_TargetNumber = 0;
	}

	public void ValidAttack()
	{
		StartCoroutine(AnimationManager.Instance.LaunchAnimations(ActionManager.Instance.GetWeaponSelected(),m_SpaceObjectTargeted.GetBody()));
	}

	public void ApplyEffect(Weapon weaponSelected)
	{
		weaponSelected.ApplyEffect (m_SpaceObjectTargeted, m_WeaponTargeted);
	}

	public void ComputeAttackRange( SpaceObject objectSelected)
	{
		m_AttackRangeTiles.Clear ();
		m_AttackRangeTiles = ActionManager.Instance.GetWeaponSelected().ComputeAttackRange(objectSelected);
		ComputeTargetInRange ();
	}
	private void ComputeTargetInRange()
	{
		m_TargetsInRange.Clear();
		m_TargetsInRange = ActionManager.Instance.GetWeaponSelected ().ComputeTargetInRange (m_AttackRangeTiles);
		foreach(SpaceObject target in m_TargetsInRange)
		{
			HUDMarkerManager.Instance.SpawnTargetMarker(target);
		}
		if(m_TargetsInRange.Count !=0)
		{
			m_TargetNumber = 0;
			m_WeaponNumber = -1;
			m_SpaceObjectTargeted = m_TargetsInRange[m_TargetNumber];
			m_SpaceObjectTargeted.GetTargetMarker().IsTargeted(true);
			ActionManager.Instance.GetWeaponSelected().PrepareEffect(m_SpaceObjectTargeted);
			ActionManager.Instance.GetWeaponSelected().GetWeaponAttackDisplayer().DisplayAttack(true);
			HUD.Instance.DisplayValidationButton(true);
		}
	}
	public void ChangeTarget()
	{
		if(m_TargetsInRange.Count !=0 && m_TargetNumber < m_TargetsInRange.Count-1)
		{
			m_TargetNumber++;
		}
		else if(m_TargetsInRange.Count !=0 && m_TargetNumber == m_TargetsInRange.Count-1)
		{
			m_TargetNumber=0;
		}
		if(m_TargetsInRange.Count !=0)
		{
			m_SpaceObjectTargeted.GetTargetMarker().IsTargeted(false);
			m_SpaceObjectTargeted = m_TargetsInRange[m_TargetNumber].GetComponent<SpaceObject>();
			m_SpaceObjectTargeted.GetTargetMarker().IsTargeted(true);
			SetWeaponTargeted(-1);
			ActionManager.Instance.GetWeaponSelected().PrepareEffect(m_SpaceObjectTargeted);
			ActionManager.Instance.GetWeaponSelected().GetWeaponAttackDisplayer().DisplayAttack(true);
			HUD.Instance.DisplayValidationButton(true);
		}
	}
	public void ChangeTarget(SpaceObject target)
	{

		if(m_TargetsInRange.Contains(target))
		{
			Debug.Log("Valid target");
			m_TargetNumber=0;
			m_SpaceObjectTargeted.GetTargetMarker().IsTargeted(false);
			m_SpaceObjectTargeted = target.GetComponent<SpaceObject>();
			m_SpaceObjectTargeted.GetTargetMarker().IsTargeted(true);
			SetWeaponTargeted(-1);
			ActionManager.Instance.GetWeaponSelected().PrepareEffect(m_SpaceObjectTargeted);
			ActionManager.Instance.GetWeaponSelected().GetWeaponAttackDisplayer().DisplayAttack(true);
			HUD.Instance.DisplayValidationButton(true);
		}
	}

	public void ResetManager()
	{
		HUD.Instance.DisplayValidationButton(false);
		if(ActionManager.Instance.GetWeaponSelected() != null)
			ActionManager.Instance.GetWeaponSelected().GetWeaponAttackDisplayer ().DisplayAttack (false);
		m_AttackRangeTiles.Clear();
		m_TargetsInRange.Clear();
		m_TargetNumber = 0;
		m_SpaceObjectTargeted = null;
		//m_WeaponSelected = null;
		HUD.Instance.ActiveAttackPanel(false);
		ResetVar ();
	}
	
	public void ResetVar()
	{
		m_ArmorDamage=0;
		m_ArmorRepair=0;
		m_ShieldDamage=0;
		m_ShieldRepair=0;
		m_BuffToAdd=null;
		m_WeaponTargeted=null;
		m_SystemDamage=0;
		m_SystemRepair=0;
		m_Hit = false;
	}

	/**** Set ****/
	public void SetWeaponTargeted(int number)
	{
		if(number == -1 || number == m_WeaponNumber)
		{
			m_WeaponNumber = -1;
			m_WeaponTargeted = null;
			ActionManager.Instance.GetWeaponSelected().PrepareEffect(m_SpaceObjectTargeted);
			ActionManager.Instance.GetWeaponSelected().GetWeaponAttackDisplayer().DisplayAttack(true);
		}
		else
		{
			m_WeaponNumber = number;
			m_WeaponTargeted = (LargeWeapon) m_SpaceObjectTargeted.GetWeaponController ().GetEquipment (number);
			ActionManager.Instance.GetWeaponSelected().PrepareEffect(m_SpaceObjectTargeted,m_WeaponTargeted);
			ActionManager.Instance.GetWeaponSelected().GetWeaponAttackDisplayer().DisplayAttack(true);
		}
		
	}
	public void SetAccuracy(float accuracy)
	{
		m_Accuracy = accuracy;
	}
	public void SetArmorDamage(float value)
	{
		m_ArmorDamage = value;
	}
	public void SetArmorRepair(float value)
	{
		m_ArmorRepair = value;
	}
	public void SetShieldDamage(float value)
	{
		m_ShieldDamage = value;
	}
	public void SetShieldRepair(float value)
	{
		m_ShieldRepair = value;
	}
	public void SetSystemDamage(float value)
	{
		m_SystemDamage = value;
	}
	public void SetSystemRepair(float value)
	{
		m_SystemRepair = value;
	}
	public void SetBuffToAdd(Buff buff)
	{
		m_BuffToAdd = buff;
	}

	/**** Get ****/
	public HashSet<Tile> GetAttackRangeTiles()
	{
		return m_AttackRangeTiles;
	}
	public SpaceObject GetSpaceObjectTargeted()
	{
		return m_SpaceObjectTargeted;
	}
	public LargeWeapon GetWeaponTargeted()
	{
		return m_WeaponTargeted;
	}
	public float GetArmorDamage()
	{
		return m_ArmorDamage;
	}
	public float GetArmorRepair()
	{
		return m_ArmorRepair;
	}
	public float GetShieldDamage()
	{
		return m_ShieldDamage;
	}
	public float GetShieldRepair()
	{
		return m_ShieldRepair;
	}
	public float GetAccuracy()
	{
		return m_Accuracy;
	}
	public int GetWeaponNumber()
	{
		return m_WeaponNumber;
	}
	public float GetSystemDamage()
	{
		return m_SystemDamage;
	}
	public float GetSystemRepair()
	{
		return m_SystemRepair;
	}
}
