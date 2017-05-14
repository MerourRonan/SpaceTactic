using UnityEngine;
using System.Collections;

public class FighterTestStats2 : ShipStats {
	
	public FighterTestStats2()
	{
		IntializeShip ("FighterTest", "Small", "Fighter","fightersFS");
		//IntializePassive (1,  Passives[1]{ ReactiveArmor()});
		//InitializeSlot (2,  Weapon[2]{ LargeLaserTest(),null});
		IntializeEnergyArmor (8, 20);
		InitializeStats (0, 0, 10, 20);
	}
}
