using UnityEngine;
using System.Collections;

public class FighterTestStats : ShipStats {

	public FighterTestStats()
	{
		IntializeShip ("FighterTest", "Small", "Fighter","fightersFS");
		//IntializePassive (1,  Passives[1]{ ReactiveArmor()});
		//InitializeSlot (2,  Weapon[2]{ LargeLaserTest(),null});
		IntializeEnergyArmor (8, 12);
		InitializeStats (0, 0, 10, 15);
	}
}
