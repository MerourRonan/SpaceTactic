using UnityEngine;
using System.Collections;

public abstract class Buff {

	private GameObject m_ShipObject;
	private Ship m_ShipScript;

	private string m_BuffName;
	protected string m_BuffType; // Malus, Bonus
	protected string m_BuffInfo;
	protected Sprite m_BuffSprite;

	protected bool m_ApplyInstant;
	protected bool m_ApplyEachTurn;

	protected bool m_CanBeStacked;
	protected int m_MaxStacksNumber;
	protected int m_CurrentStacksNumber;

	protected bool m_AvailableRemove;

	private int m_MaxDuration;
	private int m_CurrentDuration;

	public Buff(string name)
	{
		SetBuffName (name);
		m_AvailableRemove = false;
	}

	protected abstract void ApplyEffect ();
	protected abstract void UnapplyEffect ();

	public virtual void ApplyBuff(string arg)
	{
		if(arg == "FirstApply")
		{
			SetDuration("Reset");
			ApplyEffect();
		}
		else if(arg == "ResetApply")
		{
			if(m_CanBeStacked == true)
			{
				UnapplyEffect();
				SetStacksNumber("Current",1,"+");
			}
			SetDuration("Reset");
			ApplyEffect();
		}
		else if(arg == "EachTurnApply")
		{
			if(m_ApplyEachTurn == true)
			{
				ApplyEffect();
			}
			SetDuration("Current",1,"-");
		}
		else
			Debug.LogError ("invalid argument");
	}

	public virtual void UnapplyBuff(string arg="")
	{
		UnapplyEffect ();
	}
	
	public void SetBuffName(string name)
	{
		if (name == "EnergyBoost"
		    || name == "Shield") 
			m_BuffName = name;
		else
			Debug.LogError ("invalid argument");	
	}
	
	public void SetShip(GameObject ship)
	{
		m_ShipObject = ship;
		m_ShipScript = ship.GetComponent<Ship> ();
	}

	public void SetDuration (string type, int value=0, string operation="")
	{
		if(type == "Max")
		{
			if(operation == "+")
				m_MaxDuration += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_MaxDuration -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_MaxDuration *= Mathf.RoundToInt(m_MaxDuration*value);
			else if(operation == "/")
				m_MaxDuration /= Mathf.RoundToInt(m_MaxDuration/value);
		}
		else if(type == "Current")
		{
			if(operation == "+")
				m_CurrentDuration += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_CurrentDuration -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_CurrentDuration *= Mathf.RoundToInt(m_CurrentDuration*value);
			else if(operation == "/")
				m_CurrentDuration /= Mathf.RoundToInt(m_CurrentDuration/value);
		}
		else if(type == "Reset")
		{
			m_CurrentDuration = m_MaxDuration;
		}
		else
			Debug.LogError ("invalid argument");

		if(m_MaxDuration <= 0)
			m_MaxDuration = 0;
		if(m_CurrentDuration > m_MaxDuration)
			m_CurrentDuration = m_MaxDuration;
		if(m_CurrentDuration <= 0)
		{
			m_CurrentDuration = 0;
			m_AvailableRemove = true;
		}


	}

	public void SetStacksNumber (string type, int value=0, string operation="")
	{
		if(type == "Max")
		{
			if(operation == "+")
				m_MaxStacksNumber += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_MaxStacksNumber -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_MaxStacksNumber *= Mathf.RoundToInt(m_MaxStacksNumber*value);
			else if(operation == "/")
				m_MaxStacksNumber /= Mathf.RoundToInt(m_MaxStacksNumber/value);
		}
		else if(type == "Current")
		{
			if(operation == "+")
				m_CurrentStacksNumber += Mathf.RoundToInt(value);
			else if(operation == "-")
				m_CurrentStacksNumber -= Mathf.RoundToInt(value);
			else if(operation == "*")
				m_CurrentStacksNumber = Mathf.RoundToInt(m_CurrentStacksNumber*value);
			else if(operation == "/")
				m_CurrentStacksNumber = Mathf.RoundToInt(m_CurrentStacksNumber/value);
		}
		else if(type == "Reset")
		{
			m_CurrentStacksNumber = m_MaxStacksNumber;
		}
		else
			Debug.LogError ("invalid argument");
		
		if(m_MaxStacksNumber <= 0)
			m_MaxStacksNumber = 0;
		if(m_CurrentStacksNumber > m_MaxStacksNumber)
			m_CurrentStacksNumber = m_MaxStacksNumber;
		if(m_CurrentStacksNumber <= 0)
		{
			m_CurrentStacksNumber = 0;
			m_AvailableRemove = true;
		}
		
		
	}

	public bool AvailableForRemove()
	{
		return m_AvailableRemove;
	}
	
	public string GetName()
	{
		return m_BuffName;
	}
	
	public string GetInfo()
	{
		return m_BuffInfo;
	}
	
	public Ship GetShipScript()
	{
		return m_ShipScript;
	}
	
	public GameObject GetShipObject()
	{
		return m_ShipObject;
	}

	public string GetBuffType()
	{
		return m_BuffType;
	}
}
