using UnityEngine;
using System.Collections;

public class LargeWeapon : Weapon {

	/**** Integrity Properties ****/
	protected float m_IntegrityMax;
	protected float m_IntegrityCurrent;
	protected float m_RepareValue;
	protected bool m_IsDestroy;
	protected Sprite[] m_IntegritySprites;

	public void Awake()
	{
		base.Awake ();
		//m_IntegritySprites = new Sprite[5];
		//m_IntegritySprites[0] = Resources.Load("Sprite/HUDIcone/IntegritySprite/IntegritySprite_0") as Sprite;
		m_IntegritySprites = Resources.LoadAll<Sprite>("Sprite/HUDIcone/IntegritySprite");
		if (m_IntegritySprites[0] == null)
			Debug.LogError ("sprite array null");
	}
	
	public void Start()
	{
		base.Start ();
	}

	/*override protected bool CheckAmmoCoolDown()
	{
		return true;
	}*/

	protected void InitializeIntegrityProperties(float integrityMax,float repareValue)
	{
		m_IntegrityMax = integrityMax;
		m_IntegrityCurrent = integrityMax;
		m_RepareValue = repareValue;
		m_IsDestroy = false;

	}

	public float GetIntegrity(string arg)
	{
		if(arg == "Max")
		{
			return m_IntegrityMax;
		}
		else if(arg == "Current")
		{
			return m_IntegrityCurrent;
		}
		else
		{
			return 0;
		}
	}

	public void SetIntegrity(string arg, float value = 0)
	{
		if(arg == "Max")
		{
			m_IntegrityMax += value;
		}
		else if(arg == "Current")
		{
			m_IntegrityCurrent += value;
			if(m_IntegrityCurrent > m_IntegrityMax)
				m_IntegrityCurrent = m_IntegrityMax;
			if(m_IntegrityCurrent <= 0)
				m_IsDestroy = true;
		}
		else if(arg == "Reset")
		{
			m_IntegrityCurrent = m_IntegrityMax;
		}
	}

	public Sprite GetIntegritySprite(int number=0)
	{
		return m_IntegritySprites[5-Mathf.RoundToInt(m_IntegrityCurrent/20)+number];
	}


}
