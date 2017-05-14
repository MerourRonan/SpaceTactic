using UnityEngine;
using System.Collections;

public class LargeLaserTest : LargeWeapon {

	public void Awake()
	{
		base.Awake ();
		InitialiseWeaponProperties ("LargeLaserTest", "Large", 1, null, null, "Large Laser Test efficient against large target at long range");
		InitialiseTargetingProperties (1, 20, 70, "Large", "Other");
		InitializeAmmoProperties (false);
		InitializeCoolDownProperties (false);
		InitializeEffectProperties ("Damage", "Laser");
		InitializeCanDamageProperties (true, true, false);
		InitializeDamageProperties (200, 5, 1, 1, 1);
		InitializeSpecialProperties (0, false);
		InitializeRepareProperties (0, 0, 0, 0, 0, 0);
		InitializeAnimationProperties ("Gimbaled", 0.1f, 20, "LaserShot");
		InitializeIntegrityProperties (100, 25);
	}
	
	public void Start()
	{
		base.Start ();
	}
}
