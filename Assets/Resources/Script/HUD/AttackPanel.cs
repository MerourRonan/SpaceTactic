using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackPanel : MonoBehaviour {

	public static AttackPanel Instance;
	private GameObject SlotsPanel;
	private RectTransform MyRectTransform;
	private GameObject m_PositionTile;

	protected int m_SlotNumber;

	protected Text m_ArmorText;
	protected Text m_ShieldText;
	protected Text m_DamageText;
	protected Text m_DefenceText;
	protected Text m_AccuracyText;


	// Use this for initialization
	void Awake () {
		Instance = this;
		m_SlotNumber = -1;
		MyRectTransform = transform.GetComponent<RectTransform> ();
		SlotsPanel = transform.Find ("SlotSelection").gameObject;
		m_ArmorText = transform.Find ("Report/Armor").GetComponent<Text> ();
		m_ShieldText = transform.Find ("Report/Shield").GetComponent<Text> ();
		m_DamageText = transform.Find ("Report/Damage").GetComponent<Text> ();
		m_DefenceText = transform.Find ("Report/Defence").GetComponent<Text> ();
		m_AccuracyText = transform.Find ("Report/Accuracy").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		SetPosition ();
	}

	public void SetPosition(GameObject tile = null)
	{
		if (tile != null)
			m_PositionTile = tile;

		Vector3 panelPosition =  RectTransformUtility.WorldToScreenPoint(Camera.main, m_PositionTile.transform.position);
		transform.position = panelPosition - new Vector3(1,0,0)*MyRectTransform.sizeDelta.x;

	}

	public void DisplaySlots(Weapon[] weapons=null)
	{
		if(weapons != null)
		{
			for(int weaponNumber = 0; weaponNumber<6;weaponNumber++)
			{
				Transform button = SlotsPanel.transform.FindChild ("Slot"+weaponNumber.ToString());
				button.gameObject.SetActive (false);
				if(weaponNumber < weapons.Length && weapons[weaponNumber]!= null)
				{
					button.gameObject.SetActive (true);
				}
			}
		}
	}

	public void SetEquipmentToAttack(int number)
	{
		if(m_SlotNumber == number)
		{
			SlotsPanel.transform.FindChild ("SlotDamage").gameObject.SetActive(false);
			m_SlotNumber = -1;
		}
		else
		{
			m_SlotNumber = number;
			Weapon weaponSelected = PlayerControl.Instance.GetWeaponSelected ();
			LargeWeapon equipment = (LargeWeapon) PlayerControl.Instance.GetObjectToAttack ().GetComponent<Ship> ().GetWeaponController().GetEquipments ()[number];
			/*SlotsPanel.transform.FindChild ("SlotDamage").gameObject.SetActive(true);
			SlotsPanel.transform.FindChild ("SlotDamage/IntegrityBefore").GetComponent<Image> ().sprite = equipment.GetIntegritySprite ();
			SlotsPanel.transform.FindChild ("SlotDamage/IntegrityAfter").GetComponent<Image> ().sprite = equipment.GetIntegritySprite ( weaponSelected.GetSystemDamage().);*/
		}
	}
}
