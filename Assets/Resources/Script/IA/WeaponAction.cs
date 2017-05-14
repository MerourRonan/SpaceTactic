using UnityEngine;
using System.Collections;

public class WeaponAction {

	public Weapon m_Weapon;
	public SpaceObject m_Target;
	public float m_Score;
	
	public WeaponAction(Weapon weapon)
	{
		m_Weapon = weapon;
		m_Target = null;
		m_Score = 0;
	}
}
