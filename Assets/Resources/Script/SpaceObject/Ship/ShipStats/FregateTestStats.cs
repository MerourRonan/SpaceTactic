using UnityEngine;
using System.Collections;

public class FregateTestStats : ShipStats {

	public FregateTestStats()
	{
		IntializeShip ("FregateTest", "Large", "Fregate","fregateFS");
		//InitializeSlot (1,null);
		//IntializePassive (3,null);
		IntializeEnergyArmor (25, 250);
		InitializeStats (5,5,10,10);
	}
	
}
