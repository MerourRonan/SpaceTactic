using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {

	public static PlayerInfo Instance;
	List<Fleet> PlayerFleets;
	int FleetSelected;
	string m_Difficulty;
	int m_DifficultyValue;
	string m_EnemyFaction;

	void Awake()
	{
		Instance = this;
		SetDifficulty ("Easy");
		SetEnemyFaction ("Test");

		PlayerFleets = new List<Fleet> ();
		Fleet fleet0 = new Fleet ();
		Squadron squadron1 = new Squadron ();

		/*ShipFitting fighter = new ShipFitting (new FighterTestStats(), 
		                                       new string[2]{"SmallLaserTest","Overcharge"},
												new string[1]{"ReactiveArmor"});
		ShipFitting fregate = new ShipFitting (new FregateTestStats(),
		                                       new string[1]{"LargeLaserTest"},
												new string[1]{"ReactiveArmor"});
*/
		//fighter.SetEquipment (0, new SmallLaserTest());
		//fregate.SetEquipment (0, new LargeLaserTest());

		/*squadron1.SetShip (0, "LargePlayerShipTest2");
		squadron1.SetShip (1, "SmallPlayerShipTest");
		squadron1.SetShip (2, "SmallPlayerShipTest");
		squadron1.SetShip (3, "SmallPlayerShipTest");

		fleet0.SetSquadron (0, squadron1);
		/*fleet0.SetSquadron (1, squadron1);
		fleet0.SetSquadron (2, squadron1);
		fleet0.SetSquadron (3, squadron1);*/

		PlayerFleets.Add (fleet0);
		FleetSelected = 0;

	}

	public void SetDifficulty(string difficulty)
	{
		switch(difficulty)
		{
		case "Easy":
			m_DifficultyValue = 0;
			break;
		case "Medium":
			m_DifficultyValue = 1;
			break;
		case "Hard":
			m_DifficultyValue = 2;
			break;
		case "Extrem":
			m_DifficultyValue = 3;
			break;
		default:
			Debug.LogError ("invalide argument");
			break;
		}
	}

	public void SetEnemyFaction(string enemyFaction)
	{
		m_EnemyFaction = enemyFaction;
	}

	public Fleet GetActivePlayerFleet()
	{
		return PlayerFleets[FleetSelected];
	}

	public string GetDifficulty()
	{
		return m_Difficulty;
	}

	public int GetDifficultyValue()
	{
		return m_DifficultyValue;
	}
	
	public string GetEnemyFaction()
	{
		return m_EnemyFaction;
	}

}
