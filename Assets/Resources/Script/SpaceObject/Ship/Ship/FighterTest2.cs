using UnityEngine;
using System.Collections;

public class FighterTest2 : SmallShip {
	
	protected override void Awake()
	{
		base.Awake ();
		InitializeShip (new FighterTestStats2 ());
	}
	
	protected override void Start()
	{
		base.Start ();
	}
}
