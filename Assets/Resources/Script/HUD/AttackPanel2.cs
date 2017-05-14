using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackPanel2 : MonoBehaviour {

	public static AttackPanel2 Instance;

	protected GameObject m_SlotSelection;
	//protected int m_SlotSelected;

	protected Text m_ActionText;
	protected Text m_ActionValue;
	protected Text m_AccuracyText;
	protected Text m_ArmorText;
	protected Text m_ShielText;
	protected Text m_SystemText;
	protected Text m_EffectText;


	void Awake()
	{
		Instance = this;
		m_SlotSelection = transform.Find ("SlotSelection").gameObject;
		m_ActionText = transform.Find ("Report/Action/Name").GetComponent<Text> ();
		m_ActionValue = transform.Find ("Report/Action/Value").GetComponent<Text> ();
		m_EffectText = transform.Find ("Report/Effect").GetComponent<Text> ();
		m_AccuracyText = transform.Find ("Report/Accuracy").GetComponent<Text> ();
		m_ArmorText = transform.Find ("Report/Armor").GetComponent<Text> ();
		m_ShielText = transform.Find ("Report/Shield").GetComponent<Text> ();
		m_SystemText = transform.Find ("Report/System").GetComponent<Text> ();
		gameObject.SetActive (false);
	}

	public void ActiveAttackPanel()
	{
		DisplayAction();
		DisplayEffect();
		DisplayAccuracy();
		DisplayArmor();
		DisplayShield();
		DisplaySystem();
		DisplaySlots ();
	}

	public void DisplayAction(string actionName="", float value1=0f,float value2=0f)
	{
		m_ActionValue.text = "";
		if(actionName != null && actionName != "")
		{
			m_ActionText.text =actionName+" : ";
			if (value1 > 0 || value2 >0)
			{
				m_ActionValue.text = value1.ToString ()+"/"+value2.ToString ();
			}
		}
	}
	public void DisplayEffect(Buff buffEffect=null)
	{
		m_EffectText.text = "Effect : -";
		if(buffEffect != null)
			m_EffectText.text = "Effect : "+buffEffect.GetName();

	}
	public void DisplayAccuracy(float value=0f)
	{
		m_AccuracyText.text = "";
		if (value > 0)
			m_AccuracyText.text = "Accuracy : " + value.ToString() + "%";

	}
	public void DisplayArmor(float valueBefore=0f, float valueAfter=0f)
	{
		m_ArmorText.text = "";
		if (valueBefore > 0)
			m_ArmorText.text = "Armor : " + valueBefore.ToString () + "->" + valueAfter.ToString ();

	}
	public void DisplayShield(float valueBefore=0f, float valueAfter=0f)
	{
		m_ShielText.text = "";
		if (valueBefore > 0)
			m_ShielText.text = "Shield : " + valueBefore.ToString () + "->" + valueAfter.ToString ();
	}
	public void DisplaySystem(float valueBefore=0f, float valueAfter=0f)
	{
		Debug.Log (valueBefore + "/" + valueAfter);
		m_SystemText.text = "";
		if (valueBefore > 0)
			m_SystemText.text = "System : " + valueBefore.ToString () + "->" + valueAfter.ToString ();
	}

	public void DisplaySlots(Weapon[] weapons=null)
	{
		//m_SlotSelected = -1;
		for(int weaponNumber = 0; weaponNumber<6;weaponNumber++)
		{
			Transform button = m_SlotSelection.transform.FindChild ("Slot"+weaponNumber.ToString());
			button.gameObject.SetActive (false);
			if(weapons != null)
			{
				if(weaponNumber < weapons.Length && weapons[weaponNumber]!= null)
				{
					button.gameObject.SetActive (true);
				}
			}
		}
	}
}
