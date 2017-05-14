using UnityEngine;
using System.Collections;

public class FighterTest : SmallShip {

	protected override void Awake()
	{
		base.Awake ();
		InitializeShip (new FighterTestStats ());
	}

	protected override void Start()
	{
		base.Start ();
	}
}
