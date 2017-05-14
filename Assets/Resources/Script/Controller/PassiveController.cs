using UnityEngine;
using System.Collections;

public class PassiveController : MonoBehaviour {

	protected Passives[] m_Passives;
	
	void Awake()
	{
		if(transform.Find ("Passives") != null)
			m_Passives = transform.Find ("Passives").GetComponentsInChildren<Passives> ();
	}

	public Passives GetPassive(int number)
	{
		return m_Passives[number];
	}
	
	public Passives[] GetPassives()
	{
		return m_Passives;
	}
}
