using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour {

	public static HUD Instance;

	Transform m_AttackPanel;
	Transform m_TargetPanel;
	Transform m_ActionPanel;
	Transform m_InfoPanel;
	Transform m_GameInfoPanel;
	Transform m_DamagePanel;
	Transform m_ValidationButton;
	Transform m_Markers;

	Text GUITurn, GUIPhase;
	GameObject m_EndOfTurn;

	GameObject[] m_SlotButtons;
	GameObject[] m_Passives;
	//GameObject[] m_Systems;
	//GameObject[] m_Bonus;
	//GameObject[] m_Malus;
	public GameObject[] m_Features;
	public GameObject[] m_Bars;
	GameObject m_Images;

	//info
	string ArmorInfo;
	string EnergyInfo;
	string MOVinfo;
	string ESQinfo;
	string DEFinfo;
	string CRTinfo;

	void Awake()
	{
		InitializeInterface ();
	}

	// Update is called once per frame
	void Update () {
	
		//DisplayPhaseAndTurnPanel ();
		//DisplayInterface (false);
	}

	void Start()
	{

	}

	void InitializeInterface()
	{
		Instance = this;
		m_SlotButtons = new GameObject[6];
		m_Passives = new GameObject[5];
		//m_Systems = new GameObject[4];
		m_Features = new GameObject[4];
		m_Bars = new GameObject[3];
		//m_Bonus = new GameObject[7];
		//m_Malus = new GameObject[7];

		m_AttackPanel = transform.Find ("AttackPanel");
		m_TargetPanel = transform.Find ("TargetPanel");
		m_ActionPanel = transform.Find ("ActionPanel");
		m_InfoPanel = transform.Find ("InfoPanel");
		m_GameInfoPanel = transform.Find ("GameInfoPanel");
		m_DamagePanel = transform.Find ("DamagePanel");
		m_ValidationButton = transform.Find ("ValidationButton");
		m_Markers = transform.Find ("Markers");
		
		m_Features[0] = GameObject.Find ("Interface/TargetPanel/TargetInfo/Features/MOV");
		m_Features[1] = GameObject.Find ("Interface/TargetPanel/TargetInfo/Features/ESQ");
		m_Features[2] = GameObject.Find ("Interface/TargetPanel/TargetInfo/Features/DEF");
		m_Features[3] = GameObject.Find ("Interface/TargetPanel/TargetInfo/Features/CRT");
		
		m_Bars[0] = GameObject.Find ("Interface/TargetPanel/TargetInfo/ArmorBar");
		m_Bars[1] = GameObject.Find ("Interface/TargetPanel/TargetInfo/ShieldBar");
		m_Bars[2] = GameObject.Find ("Interface/ActionPanel/EnergyBar");
		
		m_Images = GameObject.Find ("Interface/TargetPanel/TargetInfo/ShipImage");

		m_AttackPanel.gameObject.SetActive (false);
		m_TargetPanel.gameObject.SetActive (false);
		m_ActionPanel.gameObject.SetActive (false);
		m_InfoPanel.gameObject.SetActive (false);
		m_GameInfoPanel.gameObject.SetActive (true);
		m_DamagePanel.gameObject.SetActive (false);
		m_ValidationButton.gameObject.SetActive (false);
	}

	public void ActiveTargetPanel(bool active)
	{
		m_TargetPanel.gameObject.SetActive (active);
		if(active == true)
		{
			TargetPanel.Instance.ActiveTargetPanel();
		}
	}

	public void ActiveActionPanel(bool active)
	{
		m_ActionPanel.gameObject.SetActive (active);
		if(active == true)
		{
			ActionPanel.Instance.ActiveActionPanel();
		}
	}

	public void ActiveInfoPanel(bool active)
	{
		m_InfoPanel.gameObject.SetActive (active);
	}

	public void ActiveAttackPanel(bool active)
	{
		m_AttackPanel.gameObject.SetActive (active);
		if (active == true)
			AttackPanel2.Instance.ActiveAttackPanel ();
		
	}
	
	public void DisplayAll(bool active)
	{
		m_TargetPanel.gameObject.SetActive (active);
		m_ActionPanel.gameObject.SetActive (active);
		m_InfoPanel.gameObject.SetActive (active);
		if(active == true)
		{
			InfoPanel.Instance.ClearInfos ();
			m_InfoPanel.gameObject.SetActive (false);
		}
	}

	public void DisplayAll(bool active, string faction)
	{
		m_TargetPanel.gameObject.SetActive (active);
		m_InfoPanel.gameObject.SetActive (active);
		if(active == true)
		{
			InfoPanel.Instance.ClearInfos ();
			m_InfoPanel.gameObject.SetActive (false);
		}
		if(faction != "Player")
			m_ActionPanel.gameObject.SetActive (false);
		else
			m_ActionPanel.gameObject.SetActive (true);
	}

	public void DisplayHideAll(string reason)
	{
		if(reason == "StartAttack")
		{
			DisplayAll(false);
			ActiveAttackPanel(false);
			m_GameInfoPanel.gameObject.SetActive(false);
			m_Markers.gameObject.SetActive(false);

		}
		else if(reason == "EndAttack")
		{
			DisplayAll(true);
			m_GameInfoPanel.gameObject.SetActive(true);
			m_Markers.gameObject.SetActive(true);
		}
	}

	public void DisplayPhaseAndTurnPanel(string phase)
	{
		if (phase == "Player")
			m_GameInfoPanel.FindChild("ButtonEndTurn").gameObject.SetActive (true);
		else
			m_GameInfoPanel.FindChild("ButtonEndTurn").gameObject.SetActive (false);
		m_GameInfoPanel.FindChild("Turn").GetComponent<Text> ().text = "Turn : " + GameObject.Find("GameManager").GetComponent<GameManager>().GetTurn().ToString();
		m_GameInfoPanel.FindChild("Phase").GetComponent<Text> ().text = "Phase : " + GameObject.Find("GameManager").GetComponent<GameManager>().GetPhase();
	}



	public void DisplayValidationButton(bool active)
	{
		m_ValidationButton.gameObject.SetActive (active);
	}

	public void ActiveDamagePanel(SpaceObject target, float damage)
	{
		m_DamagePanel.gameObject.SetActive (true);
		StartCoroutine (m_DamagePanel.GetComponent<DamagePanel> ().ActiveDamagePanel (target,damage));
	}

	public void SpawnMarker(SpaceObject spaceObject)
	{
		GameObject markerPrefab = Resources.Load<GameObject>("Prefab/HUD/Marker");
		GameObject markerInstance = Instantiate(markerPrefab,spaceObject.transform.position,Quaternion.identity) as GameObject;
		markerInstance.GetComponent<Marker>().SetSpaceObject(spaceObject);
		markerInstance.GetComponent<Image>().sprite = spaceObject.GetComponent<Ship>().GetHUDMarker();
		markerInstance.transform.SetParent(GameObject.Find("HUD/Markers/IdentificationMarkers").transform);
	}
}
