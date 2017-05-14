using UnityEngine;
using System.Collections;

public class FactionTestOld : EnemyFactionOld {

	public FactionTestOld() : base()
	{
		// Enemy Ships
		EnemyShip EnemyFighterTest = new EnemyShip ( "SmallEnemyShipTest",1, new float[4,3]{{70,60,50},{70,60,50},{70,60,50},{70,60,50}});
		EnemyShip EnemyFregateTest = new EnemyShip ( "LargeEnemyShipTest",3,new float[4,3]{{70,60,50},{70,60,50},{70,60,50},{70,60,50}});

		FactionShips.Add (EnemyFighterTest);
		FactionShips.Add (EnemyFregateTest);
	}
}
