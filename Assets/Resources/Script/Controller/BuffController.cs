using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffController : MonoBehaviour {

	protected List<Buff> m_Bonus;
	protected List<Buff> m_Malus;

	void Awake()
	{
		m_Bonus = new List<Buff> ();
		m_Malus = new List<Buff> ();
	}

	public void AddBuff( Buff buff)
	{
		Debug.Log ("adding buff  : " + buff.GetName());
		if(buff.GetBuffType() == "Bonus")
		{
			if(m_Bonus.Contains(buff) == false)
			{
				buff.SetShip(this.gameObject);
				buff.ApplyBuff("FirstApply");
				m_Bonus.Add(buff);
			}
			else if(m_Bonus.Contains(buff) == true)
			{
				int indexBuff = m_Bonus.IndexOf(buff);
				m_Bonus[indexBuff].ApplyBuff("ResetApply");
			}
		}
		else if(buff.GetBuffType() == "Malus")
		{
			if(m_Malus.Contains(buff) == false)
			{
				buff.SetShip(this.gameObject);
				buff.ApplyBuff("FirstApply");
				m_Malus.Add(buff);
			}
			else if(m_Malus.Contains(buff) == true)
			{
				int indexBuff = m_Malus.IndexOf(buff);
				m_Malus[indexBuff].ApplyBuff("ResetApply");
			}
		}
		else
			Debug.LogError("invalid argument");
		transform.GetComponent<HUDController>().DisplayHUD(true);
	}
	
	public void TurnApplyBuffs()
	{
		List<Buff> TempBonus = new List<Buff> (m_Bonus);
		List<Buff> TempMalus = new List<Buff> (m_Malus);
		foreach(Buff bonus in TempBonus)
		{
			if(bonus.AvailableForRemove() == true)
			{
				bonus.UnapplyBuff();
				m_Bonus.Remove(bonus);
			}
			else
				bonus.ApplyBuff("EachTurnApply");
		}
		foreach(Buff malus in TempMalus)
		{
			if(malus.AvailableForRemove() == true)
			{
				malus.UnapplyBuff();
				m_Malus.Remove(malus);
			}
			else
				malus.ApplyBuff("EachTurnApply");
		}
	}

	public List<Buff> GetBonus()
	{
		return m_Bonus;
	}
	
	public List<Buff> GetMalus()
	{
		return m_Malus;
	}
}
