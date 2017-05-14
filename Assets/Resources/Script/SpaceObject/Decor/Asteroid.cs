using UnityEngine;
using System.Collections;

public class Asteroid : DecorObject {
	

	protected override void Awake () 
	{
		base.Awake ();
		InitializeSpaceObject ("Decor","Asteroid", "Large", "");
		InitializeHUDSprite ("Sprite/Decor/Rock1");
		InitializeArmor (1000);
		//transform.parent.tag = "Decor";
	}
	
}
