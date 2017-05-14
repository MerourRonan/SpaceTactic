using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour {

	protected GameObject m_Feature;
	protected Image m_FeatureImage;
	protected Text m_FeatureText;
	protected Image m_IntegrityImage;

	void Awake()
	{
		m_Feature = transform.Find ("Feature").gameObject;
		m_FeatureImage = transform.Find("Feature/Image").GetComponent<Image> ();
		m_FeatureText = transform.Find("Feature").GetComponentInChildren<Text> ();
		m_IntegrityImage = transform.Find("Integrity").GetComponent<Image> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AssignWeapon(Weapon weapon)
	{
		m_Feature.SetActive(true);
		m_IntegrityImage.gameObject.SetActive (false);
		if(weapon.IsAmmoWeapon() == true && weapon.IsCoolDownWeapon() ==  true)
		{
			if( weapon.CheckCoolDown() == false)
			{
				m_FeatureText.text = weapon.GetCoolDown("Current").ToString();
			}
			else
			{
				m_FeatureText.text = weapon.GetAmmo("Current").ToString();
				m_FeatureImage.sprite = weapon.GetAmmoSprite();
			}
		}
		else if(weapon.IsAmmoWeapon() == true)
		{
			m_FeatureText.text = weapon.GetAmmo("Current").ToString();
			m_FeatureImage.sprite = weapon.GetAmmoSprite();
		}
		else if(weapon.IsCoolDownWeapon() ==  true)
		{
			m_FeatureText.text = weapon.GetCoolDown("Current").ToString();
		}
		else
		{
			m_Feature.SetActive(false);
		}

		if(weapon.GetSize() == "Large")
		{
			LargeWeapon largeWeaponAssign = (LargeWeapon) weapon;
			m_Feature.SetActive(false);
			m_IntegrityImage.gameObject.SetActive (true);
			m_IntegrityImage.sprite = largeWeaponAssign.GetIntegritySprite();
		}
	}
}
