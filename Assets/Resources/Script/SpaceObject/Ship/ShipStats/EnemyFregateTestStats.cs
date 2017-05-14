using UnityEngine;
using System.Collections;

public class EnemyFregateTestStats : ShipStats {

	public EnemyFregateTestStats()
	{
		IntializeShip ("EnemyFregateTest", "Large", "Fregate","fregateFS");
		//InitializeSlot (1,null);
		//IntializePassive (3,null);
		IntializeEnergyArmor (25, 250);
		InitializeStats (5,5,10,4);
	}
}
