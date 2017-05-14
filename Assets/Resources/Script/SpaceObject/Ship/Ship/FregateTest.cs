using UnityEngine;
using System.Collections;

public class FregateTest : LargeShip {

	protected override void Awake()
	{
		base.Awake ();
		InitializeShip (new FregateTestStats ());
	}
	
	protected override void Start()
	{
		base.Start ();
	}
}
