using UnityEngine;
using System.Collections;

public class AttackPanelInfo {

	public bool DisplaySlotPanel;
	public int ArmorBefore;
	public int ArmorAfter;
	public int ShieldBefore;
	public int ShieldAfter;
	public float Damage;
	public int Defence;
	public float Accuracy;
	public int IntegrityBefore;
	public int IntegrityAfter;

	public AttackPanelInfo()
	{
		DisplaySlotPanel = false;
		ArmorBefore = 0;
		ArmorAfter = 0;
		ShieldBefore = 0;
		ShieldAfter = 0;
		Damage = 0;
		Defence = 0;
		Accuracy = 0;
		IntegrityBefore = 0;
		IntegrityAfter = 0;
	}

}
