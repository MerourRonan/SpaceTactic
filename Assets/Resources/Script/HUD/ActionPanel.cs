using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour {

	public static ActionPanel Instance;
	protected GameObject m_EnergyBar;
	protected GameObject m_Slots;
	protected GameObject m_EngineButton;


	void Awake()
	{
		Instance = this;

		m_EnergyBar = transform.FindChild ("EnergyBar").gameObject;
		m_Slots = transform.FindChild ("Slots").gameObject;
		m_EngineButton = transform.FindChild ("EngineButton").gameObject;
	}

	public void ActiveActionPanel()
	{
		DisplaySlots (false);
		DisplayEnergyBar (false);
		DisplayEngineButton(false);
	}

	public void DisplaySlots(bool active, Weapon[] weapons=null)
	{
		if (active == false)
			m_Slots.SetActive (false);
		else if(active == true && weapons != null)
		{
			m_Slots.SetActive (true);
			for(int weaponNumber = 0; weaponNumber<6;weaponNumber++)
			{
				m_Slots.transform.FindChild ("Slot"+weaponNumber.ToString()).gameObject.SetActive (false);
				if(weaponNumber < weapons.Length && weapons[weaponNumber]!= null)
				{
					Transform button = m_Slots.transform.FindChild("Slot"+weaponNumber.ToString());
					button.GetComponent<Button>().interactable = true;
					button.gameObject.SetActive (true);
					button.GetComponent<SlotButton>().AssignWeapon(weapons[weaponNumber]);
					if(weapons[weaponNumber].HasBeenUsed == true)
						button.GetComponent<Button>().interactable = false;
				}
			}
		}
	}

	public void DisplayEngineButton(bool active)
	{
		m_EngineButton.SetActive(active);
	}

	public void DisplayEnergyBar(bool active,int currentValue=0, int maxValue=0)
	{
		if(active == false)
		{
			m_EnergyBar.SetActive (false);
		}
		else
		{
			m_EnergyBar.SetActive (true);
			m_EnergyBar.transform.FindChild("Number").GetComponent<Text> ().text = ("("+currentValue+"/"+maxValue+")");
		}
	}
}
