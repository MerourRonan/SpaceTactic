using UnityEngine;
using System.Collections;


public class Fleet {

	Squadron[] m_Squadrons;

	public Fleet()
	{
		m_Squadrons = new Squadron[4]{null,null,null,null};
	}

	public void SetSquadron(int number, Squadron squadron)
	{
		if(number >= 0 && number <4)
		{
			m_Squadrons[number] = squadron;
		}
		else
		{
			Debug.Log("ERROR : squadron number to high");
		}
		//m_Squadrons [number] = squadron;
	}

	public Squadron GetSquadron(int number)
	{
		return m_Squadrons [number];
	
	}
	public Squadron[] GetSquadron()
	{
		return m_Squadrons;
		
	}
}
