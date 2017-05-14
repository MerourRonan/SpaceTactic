using UnityEngine;
using System.Collections;

public class Overcharge : Weapon {

	public void Awake()
	{
		base.Awake ();
		InitialiseWeaponProperties ("Overcharge", "Small", 1, null, null, "Overcharge : Boost ship's energy by 2 for 1 turn.");
		InitialiseTargetingProperties (0, 0, 100, "Small", "Self");
		InitializeAmmoProperties (false);
		InitializeCoolDownProperties (true,1);
		InitializeEffectProperties ("Buff", "OverCharge",new BuffEnergyBoost());
		InitializeAnimationProperties ("Fixed", 0.1f, 20, "LaserShot");
	}
	
	public void Start()
	{
		base.Start ();
	}

	/*public override IEnumerator UseWeapon(GameObject target, GameObject targetTile)
	{
		if(HasbeenUsed == false
		   && EnoughEnergy() == true
		   && ValidTargetFaction(target) == true
		   && CheckAmmoCoolDown() == true)
		{
			Debug.Log("fire");
			if( m_Size == "Small" && target.GetComponent<Ship>().GetSize() == "Small")
			{
				Debug.Log("fire on small target");
				ApplyEffect(target);
				yield return true;
			}
			else if( m_Size == "Small" && target.GetComponent<Ship>().GetSize() != "Small")
			{
				Debug.Log("fire on large target");
				ApplyEffect(target);
				yield return true;
			}
		}
	}*/

	public void ApplyEffect(GameObject target, Weapon weapon)
	{
		Debug.Log("apply overcharge");
		GetSpaceObject ().GetComponent<Ship> ().GetBuffController().AddBuff (new BuffEnergyBoost());
	}
}
