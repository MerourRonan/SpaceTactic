using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DecorObject : SpaceObject {
	
	/******************* Unity Function *******************/
	protected override void Awake()
	{
		base.Awake ();
		transform.tag = "Decor";
		m_ObjectType = "Decor";
	}

	protected override void Start()
	{
		base.Start ();
	}


	/******************* Object Function *******************/
	public void Selected()
	{
		DisplayHUDInfo();
	}
	
	public void DisplayHUDInfo()
	{
		/*HUD.Instance.DisplayAll (true,"Decor");
		HUD.Instance.DisplayImage (true, GetHUDSprite());
		HUD.Instance.DisplayArmorBar (true, GetArmorPoints("Current"), GetArmorPoints("Max"));*/
	}
}
