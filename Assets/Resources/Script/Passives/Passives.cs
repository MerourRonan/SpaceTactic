using UnityEngine;
using System.Collections;

public abstract class Passives : MonoBehaviour {

	private GameObject m_MyShip;
	private Ship m_ShipScript;

	private string m_PassiveName;
	protected string m_PassiveInfo;
	protected Sprite m_PassiveSprite;

	public abstract void ApplyPassives(bool arg);

	public virtual void Awake()
	{
		m_MyShip = transform.parent.parent.gameObject;
		m_ShipScript = m_MyShip.GetComponent<Ship> ();
	}
	
	public virtual void Start()
	{
		ApplyPassives (true);
	}

	public void SetPassiveName(string name)
	{
		if (name == "ReactiveArmor") 
			m_PassiveName = name;
		else
			Debug.LogError ("invalid argument");	
	}

	public string GetName()
	{
		return m_PassiveName;
	}

	public string GetInfo()
	{
		return m_PassiveInfo;
	}
	
	public Ship GetShipScript()
	{
		return m_ShipScript;
	}
	
	public GameObject GetMyShip()
	{
		return m_MyShip;
	}
}
