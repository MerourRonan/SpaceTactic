using UnityEngine;
using System.Collections;

public class BuffEnergyBoost : Buff {

	public BuffEnergyBoost() : base("EnergyBoost")
	{
		m_BuffInfo = "Increase ship's energy by 100%";
		m_BuffType = "Bonus";
		m_ApplyInstant = true;
		SetDuration ("Max", 1, "+");
		SetDuration ("Reset");
	}

	protected override void ApplyEffect()
	{
		GetShipScript ().SetEnergyPoints ("Max", 2, "*");
		GetShipScript ().SetEnergyPoints ("Current", 2, "*");
	}

	protected override void UnapplyEffect()
	{
		GetShipScript ().SetEnergyPoints ("Max", 2, "/");
		GetShipScript ().SetEnergyPoints ("Reset");
	}
}
