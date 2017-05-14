using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

	public static InfoPanel Instance;

	Text m_TextInfo;

	string m_InfoArmor;
	string m_InfoEnergy;
	string m_InfoShield;
	string m_InfoEngines;
	
	string m_InfoMOV;
	string m_InfoESQ;
	string m_InfoDEF;
	string m_InfoCRT;

	List<string> m_SlotsInfo;
	List<string> m_PassivesInfo;
	List<string> m_BonusInfo;
	List<string> m_MalusInfo;

	void Awake()
	{
		Instance = this;
		m_TextInfo = transform.GetComponentInChildren<Text> ();
		InitializeInfos ();

		m_SlotsInfo = new List<string> ();
		m_PassivesInfo = new List<string> ();
		m_BonusInfo = new List<string> ();
		m_MalusInfo = new List<string> ();
	}

	void InitializeInfos()
	{
		m_InfoArmor = "Hull integrity of the ship, when it reach zero, the ship is destroyed";
		m_InfoShield = "Absorbe incoming damage when active";
		m_InfoEnergy = "Power of the ship, each equipment of the ship needs energy to be used";

		m_InfoEngines = "Engines allow the ship to move";

		m_InfoMOV = "Move range of the ship (in tile)";
		m_InfoESQ = "Evade chance of the ship, in case of dodge the ship doesn't take damages";
		m_InfoDEF = "Armor plate of the ship, high value reduces incoming damages";
		m_InfoCRT = "Critical hit chance of the ship, a critical hit double damages";
	}


	/*public void DisplayInfoPanel(bool active)
	{
		gameObject.SetActive (active);
		if (active == true) 
		{
			SelectInfo(m_InfoName,m_Number);
		}
	}*/

	public void SetInfos(Weapon[] weapons, Passives[] passives, List<Buff> bonus, List<Buff> malus )
	{
		gameObject.SetActive (true);
		ClearInfos ();
		for(int number=0;number <7;number++)
		{
			if(weapons!= null && number < weapons.Length && weapons[number]!= null)
			{
				m_SlotsInfo.Add((weapons[number].GetInfo()));
			}
			if(passives!= null && number < passives.Length && passives[number]!= null)
			{
				m_PassivesInfo.Add(passives[number].GetInfo());
			}
			if(bonus!= null && number < bonus.Count)
			{
				m_BonusInfo.Add(bonus[number].GetInfo ());
			}
			if(malus!= null && number < malus.Count)
			{
				m_MalusInfo.Add(malus[number].GetInfo ());
			}
		}
		gameObject.SetActive (false);

	}

	public void DisplayInfo(string infoName)
	{
		gameObject.SetActive (true);
		if (infoName == "Armor")
			m_TextInfo.text = m_InfoArmor;
		else if (infoName == "Energy")
			m_TextInfo.text = m_InfoEnergy;
		else if (infoName == "Shield")
			m_TextInfo.text = m_InfoShield;
		else if (infoName == "Engines")
			m_TextInfo.text = m_InfoEngines;
		else if (infoName == "MOV")
			m_TextInfo.text = m_InfoMOV;
		else if (infoName == "ESQ")
			m_TextInfo.text = m_InfoESQ;
		else if (infoName == "DEF")
			m_TextInfo.text = m_InfoDEF;
		else if (infoName == "CRT")
			m_TextInfo.text = m_InfoCRT;
	}

	public void DisplaySlotInfo(int number)
	{
		gameObject.SetActive (true);
		m_TextInfo.text = m_SlotsInfo [number];
	}

	public void DisplayPassiveInfo(int number)
	{
		gameObject.SetActive (true);
		m_TextInfo.text = m_PassivesInfo [number];
	}

	public void DisplayBonusInfo(int number)
	{
		gameObject.SetActive (true);
		m_TextInfo.text = m_BonusInfo [number];
	}

	public void DisplayMalusInfo(int number)
	{
		gameObject.SetActive (true);
		m_TextInfo.text = m_MalusInfo [number];
	}

	public void HideInfoPanel()
	{
		gameObject.SetActive (false);
	}
	
	public void ClearInfos()
	{
		m_SlotsInfo.Clear ();
		m_PassivesInfo.Clear ();
		m_BonusInfo.Clear ();
		m_MalusInfo.Clear ();
	}
}
