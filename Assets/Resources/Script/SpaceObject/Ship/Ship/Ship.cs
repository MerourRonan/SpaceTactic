using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ship : SpaceObject {

	/******************* Unity Function *******************/
	protected override  void Awake()
	{
		base.Awake ();
		transform.Find ("Body").gameObject.layer = LayerMask.NameToLayer ("Ship");
	}

	protected override void Start()
	{
		base.Start ();
	}

	/******************* Object Function *******************/
	public void InitializeShip(ShipStats shipStats)
	{
		InitializeSpaceObject ("Ship", shipStats.GetName (), shipStats.GetSize (), shipStats.GetClass ());

		SetArmorPoints ("Initialize", shipStats.GetArmorPoints ());
		SetEnergyPoints("Initialize", shipStats.GetEnergyPoints ());

		SetEvade ("Initialize",shipStats.GetEvade ());
		SetDefence ("Initialize",shipStats.GetDefence ());
		SetCriticalHitChance ("Initialize",shipStats.GetCritChance ());
		SetMoveRange ("Initialize",shipStats.GetMoveRange ());
	}




	/*public bool GetHasMoved()
	{
		return m_HasMoved;
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
		return m_CanMove;
	}

	public GameObject GetTile()
	{
		return GetTiles()[0];
	}

	public Sprite GetHUDMarker()
	{
		return m_HUDMarker;
	}

	public GameObject GetPrimaryTile()
	{
		return m_ReferenceTile;
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

	public DamageController GetDamageController()
	{
		return m_DamageController;
	}*/
}
