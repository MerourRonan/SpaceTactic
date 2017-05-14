using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TargetPanel : MonoBehaviour {

	public static TargetPanel Instance;

	protected GameObject m_TargetImage;
	protected GameObject m_BuffPanel;
	protected GameObject m_PassivePanel;
	protected GameObject m_ArmorBar;
	protected GameObject m_ShieldBar;
	protected GameObject m_Features;


	void Awake()
	{
		Instance = this;
		m_TargetImage = transform.FindChild ("ShipImage").gameObject;
		m_BuffPanel = transform.FindChild ("Buffs").gameObject;
		m_PassivePanel = transform.FindChild ("Passives").gameObject;
		m_ArmorBar = transform.FindChild ("ArmorBar").gameObject;
		m_ShieldBar = transform.FindChild ("ShieldBar").gameObject;
		m_Features = transform.FindChild ("Features").gameObject;

		ActiveTargetPanel ();
	}

	public void ActiveTargetPanel()
	{
		DisplayBuffs (false);
		DisplayPassives (false);
		DisplayArmorBar(false);
		DisplayShieldBar(false);
		DisplayTargetImage(false);
		DisplayFeature(false);
	}

	public void DisplayBuffs(bool active, List<Buff> bonusList=null, List<Buff> malusList=null)
	{
		if (active == false)
			m_BuffPanel.SetActive (false);
		else
		{
			m_BuffPanel.SetActive (true);
			for(int buffNumber = 0; buffNumber<7;buffNumber++)
			{
				m_BuffPanel.transform.FindChild ("Bonus/Bonus"+buffNumber.ToString()).gameObject.SetActive (false);
				m_BuffPanel.transform.FindChild ("Malus/Malus"+buffNumber.ToString()).gameObject.SetActive (false);
				if(bonusList != null && buffNumber<bonusList.Count)
				{
					m_BuffPanel.transform.FindChild ("Bonus/Bonus"+buffNumber.ToString()).gameObject.SetActive (true);
				}
				if(malusList != null && buffNumber <malusList.Count)
				{
					m_BuffPanel.transform.FindChild("Malus/Malus"+buffNumber.ToString()).gameObject.SetActive (true);
				}
			}
		}
	}

	public void DisplayPassives(bool active, Passives[] passives = null)
	{
		if (active == false)
			m_PassivePanel.SetActive (false);
		else
		{
			m_PassivePanel.SetActive(true);
			if(passives != null)
			{
				for(int passiveNumber = 0; passiveNumber<3;passiveNumber++)
				{
					m_PassivePanel.transform.FindChild ("Passive"+passiveNumber.ToString()).gameObject.SetActive (false);
					if(passiveNumber < passives.Length && passives[passiveNumber]!= null)
					{
						m_PassivePanel.transform.FindChild ("Passive"+passiveNumber.ToString()).gameObject.SetActive (true);
					}
				}
			}
		}
	}

	public void DisplayArmorBar(bool active,float currentValue=0, float maxValue=0)
	{
		if(active == false)
		{
			m_ArmorBar.SetActive (false);
		}
		else
		{
			m_ArmorBar.SetActive (true);
			m_ArmorBar.transform.FindChild("Number").GetComponent<Text> ().text = ("("+currentValue+"/"+maxValue+")");
		}
	}
	
	public void DisplayShieldBar(bool active,float currentValue=0, float maxValue=0)
	{
		if(active == false)
		{
			m_ShieldBar.SetActive (false);
		}
		else if(currentValue >0)
		{
			m_ShieldBar.SetActive (true);
			m_ShieldBar.transform.FindChild("Number").GetComponent<Text> ().text = ("("+currentValue+"/"+maxValue+")");
		}
	}

	public void DisplayTargetImage(bool active, Sprite HUDSprite=null)
	{
		if(active == false)
		{
			m_TargetImage.SetActive (false);
		}
		else
		{
			m_TargetImage.SetActive (true);
			m_TargetImage.GetComponent<Image> ().sprite = HUDSprite;
		}
	}

	public void DisplayFeature(bool active,string featureName="", int value=0)
	{
		if(active == false)
		{
			m_Features.SetActive(false);
		}
		else
		{
			m_Features.SetActive(true);
			m_Features.transform.FindChild(featureName).gameObject.SetActive(true);
			m_Features.transform.FindChild(featureName).gameObject.GetComponent<Text> ().text = (featureName+" : "+value);
		}
	}
}
