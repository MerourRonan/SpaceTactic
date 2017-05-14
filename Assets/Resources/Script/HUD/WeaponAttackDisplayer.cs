using UnityEngine;
using System.Collections;

public class WeaponAttackDisplayer : MonoBehaviour {

	protected Weapon m_Weapon;

	void Awake () {
		
		m_Weapon = transform.GetComponent<Weapon> ();
		
	}
	
	public virtual void DisplayAttack(bool active)
	{
		if(active == true)
		{
			HUD.Instance.ActiveAttackPanel(true);
			AttackPanel2.Instance.DisplayAction(m_Weapon.GetEffectType(),AttackManager.Instance.GetShieldDamage(),AttackManager.Instance.GetArmorDamage());
			AttackPanel2.Instance.DisplayEffect(m_Weapon.GetBuffEffect());
			AttackPanel2.Instance.DisplayAccuracy(AttackManager.Instance.GetAccuracy());
			AttackPanel2.Instance.DisplayArmor(AttackManager.Instance.GetSpaceObjectTargeted().GetArmorPoints("Current"),(AttackManager.Instance.GetSpaceObjectTargeted().GetArmorPoints("Current")-AttackManager.Instance.GetArmorDamage()));
			AttackPanel2.Instance.DisplayShield(AttackManager.Instance.GetSpaceObjectTargeted().GetShieldPoints(),(AttackManager.Instance.GetSpaceObjectTargeted().GetShieldPoints()-AttackManager.Instance.GetShieldDamage()));
			if(AttackManager.Instance.GetWeaponTargeted() != null)
			{
				Debug.Log("display system");
				AttackPanel2.Instance.DisplaySystem(AttackManager.Instance.GetWeaponTargeted().GetIntegrity("Current"),(AttackManager.Instance.GetWeaponTargeted().GetIntegrity("Current")-AttackManager.Instance.GetSystemDamage()));
			}
			if(m_Weapon.CanDamageSystem() && AttackManager.Instance.GetSpaceObjectTargeted().GetSize() != "Small")
				AttackPanel2.Instance.DisplaySlots(AttackManager.Instance.GetSpaceObjectTargeted().GetWeaponController().GetEquipments());
		}
		else
		{
			HUD.Instance.ActiveAttackPanel(false);
		}
	}
}
