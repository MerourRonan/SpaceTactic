using UnityEngine;
using System.Collections;

public class ShipDisplayer : HUDController {

	private Ship m_MyShip;
	
	void Awake () {

		m_MyShip = transform.GetComponent<Ship> ();
	
	}

	public override void DisplayHUD(bool active)
	{
		if(active == true)
		{
			HUD.Instance.ActiveTargetPanel(true);
			HUD.Instance.ActiveActionPanel(true);
			HUD.Instance.ActiveInfoPanel(true);

			TargetPanel.Instance.DisplayArmorBar(true, m_MyShip.GetArmorPoints ("Current"), m_MyShip.GetArmorPoints ("Max"));
			TargetPanel.Instance.DisplayBuffs(true, m_MyShip.GetBuffController().GetBonus(),m_MyShip.GetBuffController().GetMalus());
			TargetPanel.Instance.DisplayPassives(true, m_MyShip.GetPassiveController().GetPassives());
			TargetPanel.Instance.DisplayFeature (true, "MOV", m_MyShip.GetMoveRange ());
			TargetPanel.Instance.DisplayFeature (true, "ESQ", m_MyShip.GetEvade ());
			TargetPanel.Instance.DisplayFeature (true, "DEF", m_MyShip.GetDefence ());
			TargetPanel.Instance.DisplayFeature (true, "CRT", m_MyShip.GetCritChance ());
			TargetPanel.Instance.DisplayShieldBar (true, 0, 0);
			TargetPanel.Instance.DisplayTargetImage(true, m_MyShip.GetHUDSprite());

			ActionPanel.Instance.DisplayEnergyBar (true, m_MyShip.GetEnergyPoints("Current"), m_MyShip.GetEnergyPoints ("Max"));
			ActionPanel.Instance.DisplaySlots (true, m_MyShip.GetWeaponController().GetEquipments());
			ActionPanel.Instance.DisplayEngineButton (!m_MyShip.GetHasMoved());
			/*if(m_MyShip.GetHasMoved() == true)
				ActionPanel.Instance.DisplayEngineButton (false);
			else
				ActionPanel.Instance.DisplayEngineButton (true);*/
			
			InfoPanel.Instance.SetInfos (m_MyShip.GetWeaponController().GetEquipments(), m_MyShip.GetPassiveController().GetPassives(), m_MyShip.GetBuffController().GetBonus(), m_MyShip.GetBuffController().GetMalus());
		}
		else
		{
			HUD.Instance.ActiveTargetPanel(false);
			HUD.Instance.ActiveActionPanel(false);
			HUD.Instance.ActiveInfoPanel(false);
		}
	}

	public void DisplayActionRange(bool active, float range)
	{
		LineRenderer rangeDisplay = transform.gameObject.AddComponent<LineRenderer> ();
		rangeDisplay.SetVertexCount (361);
		for(int iter =0; iter<361;iter++)
		{
			rangeDisplay.SetPosition(iter,new Vector3(range*Mathf.Cos(iter*Mathf.Deg2Rad),1,range*Mathf.Sin(iter*Mathf.Deg2Rad)));
		}
	}

}
