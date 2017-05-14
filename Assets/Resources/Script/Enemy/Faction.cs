using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Faction {
	
	protected List<Squadron> m_EasySquadrons;
	protected List<Squadron> m_MediumSquadrons;
	protected List<Squadron> m_HardSquadrons;
	protected List<Squadron> m_ExtremeSquadrons;



	public Faction()
	{
		m_EasySquadrons = new List<Squadron> ();
		m_MediumSquadrons = new List<Squadron> ();
		m_HardSquadrons = new List<Squadron> ();
		m_ExtremeSquadrons = new List<Squadron> ();


	}


	public List<Squadron> GetSquadronsList(int difficultyValue)
	{
		switch (difficultyValue)
		{
		case 0:
			return m_EasySquadrons;
			break;
		case 1:
			return m_EasySquadrons;
			break;
		case 2:
			return m_EasySquadrons;
			break;
		case 3:
			return m_EasySquadrons;
			break;
		default:
			Debug.LogError("invalid difficulty value = " + difficultyValue);
			return null;
			break;
		}
	}

}
