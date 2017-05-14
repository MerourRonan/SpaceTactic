using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFactionOld {

	protected List<EnemyShip> FactionShips;

	public EnemyFactionOld()
	{
		FactionShips = new List<EnemyShip> ();
	}

	public EnemyShip RandomShip(int threatZoneValue)
	{
		int difficultyValue = PlayerInfo.Instance.GetDifficultyValue ();
		float randomRange = 0;
		float top = 0;

		foreach(EnemyShip enemyShip in FactionShips)
		{
			randomRange+=enemyShip.GetChanceOfSpawn(difficultyValue,threatZoneValue);
		}
		float random = UnityEngine.Random.Range (0, randomRange);
		foreach(EnemyShip enemyShip in FactionShips)
		{
			top+=enemyShip.GetChanceOfSpawn(difficultyValue,threatZoneValue);
			if(random < top)
				return enemyShip;
		}
		Debug.LogError ("error in RandomShip");
		return null;
	}



	/*public EnemyShip EnemyShipName(int threatZoneValue)
	{
		return RandomShip (threatZoneValue);
	}*/
}
