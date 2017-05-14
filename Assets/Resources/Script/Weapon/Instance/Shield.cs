using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shield : LargeWeapon {

	public void Awake()
	{
		base.Awake ();
		InitialiseWeaponProperties ("Shield", "Large", 1, null, null, "Shield that takes damage instead of armor");
		InitialiseTargetingProperties (0, 0, 100, "Large", "Self");
		InitializeAmmoProperties (false);
		InitializeCoolDownProperties (false);
		InitializeEffectProperties ("Shield", "Shield", new BuffShield ());
		InitializeCanDamageProperties (false, false, false);
		InitializeDamageProperties (5, 1, 1, 1, 1);
		InitializeSpecialProperties (0, false);
		InitializeRepareProperties (0, 0, 0, 0, 0, 0);
		InitializeAnimationProperties ("NoMove", 0, 1, "ShieldFregate");
		InitializeIntegrityProperties (100, 25);
		
	}
	
	public void Start()
	{
		base.Start ();
	}

	public override void ApplyEffect(SpaceObject target, LargeWeapon systemTargeted=null)
	{
		Debug.Log ("apply effect shield");
		target.ApplyBuffToAdd (m_BuffEffect);

	}

	public override HashSet<Tile> ComputeAttackRange( SpaceObject objectSelected)
	{
		HashSet<Tile> tiles = new HashSet<Tile> (m_SpaceObject.GetTiles());
		return tiles;
	}
	public override List<SpaceObject> ComputeTargetInRange(HashSet<Tile> attackRangeTiles)
	{
		List<SpaceObject> targetInRange = new List<SpaceObject> ();
		targetInRange.Add (m_SpaceObject);
		return targetInRange;
	}
}
