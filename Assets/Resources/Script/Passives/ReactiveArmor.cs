using UnityEngine;
using System.Collections;

public class ReactiveArmor : Passives {

	public void Awake()
	{
		base.Awake ();
		m_PassiveInfo = "Increase armor by 10% and defence by 1";
		m_PassiveSprite = Resources.Load<Sprite> ("Sprite/Decor/Rock1");
	}
	
	public void Start()
	{
		base.Start ();
	}

	public override void ApplyPassives(bool arg)
	{
		if(arg == true)
		{
			GetShipScript().SetArmorPoints("Max",1.1f,"*");
			GetShipScript().SetArmorPoints("Reset");
			GetShipScript().SetDefence("+",1);
		}
		else if(arg == false)
		{
			GetShipScript().SetArmorPoints("Max",1.1f,"/");
			GetShipScript().SetDefence("-",1);
		}
	}

}
