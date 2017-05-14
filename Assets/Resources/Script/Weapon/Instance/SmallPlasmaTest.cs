using UnityEngine;
using System.Collections;

public class SmallPlasmaTest : Weapon {
	
	public void Awake()
	{
		base.Awake ();
		InitialiseWeaponProperties ("SmallPlasmaTest", "Small", 5, null, null, "Small Plasma Test efficient against small target at small range");
		InitialiseTargetingProperties (1, 3, 90, "Small", "Other");
		InitializeAmmoProperties (true,10,1);
		InitializeCoolDownProperties (false);
		InitializeEffectProperties ("Damage", "Laser");
		InitializeCanDamageProperties (true, true, false);
		InitializeDamageProperties (50, 1, 1, 1, 1);
		InitializeSpecialProperties (0, false);
		InitializeRepareProperties (0, 0, 0, 0, 0, 0);
		InitializeAnimationProperties ("Fixed", 0.1f, 20, "LaserShot");
		
	}
	
	public void Start()
	{
		base.Start ();
	}
	
}
