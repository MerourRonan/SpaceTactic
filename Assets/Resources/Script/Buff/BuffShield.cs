using UnityEngine;
using System.Collections;

public class BuffShield : Buff {

	public BuffShield() : base("Shield")
	{
		m_BuffInfo = "Create a shield that can absorb up to 50% of armor damages";
		m_BuffType = "Bonus";
		m_ApplyInstant = true;
		SetDuration ("Max", 1, "+");
		SetDuration ("Reset");
	}
	
	protected override void ApplyEffect()
	{
		GetShipScript ().SetShieldPoints (GetShipScript ().GetArmorPoints ("Max")/2, "+");
		Debug.Log ("ship shield = " + GetShipScript ().GetShieldPoints ());
	}
	
	protected override void UnapplyEffect()
	{
		GetShipScript ().SetShieldPoints (0, "*");
	}
}
