using UnityEngine;
using System.Collections;

public class EnemyFighterTestStats : ShipStats {

	public EnemyFighterTestStats()
	{
		IntializeShip ("EnemyFighterTest", "Small", "Fighter","fightersFS");
		//IntializePassive (1,null);
		//InitializeSlot (1,null);
		IntializeEnergyArmor (8, 12);
		InitializeStats (20, 0, 10, 40);
	}
}
