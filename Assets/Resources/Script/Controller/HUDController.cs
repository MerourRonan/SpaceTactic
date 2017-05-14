using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {

	private SpaceObject m_MySpaceObject;

	private Sprite m_HUDSprite;
	protected Sprite m_HUDMarker;
	protected LineRenderer m_RangeDisplay;
	
	void Awake () {
		
		m_MySpaceObject = transform.GetComponent<Ship> ();
		m_HUDMarker = Resources.Load<Sprite>("Sprite/HUDIcone/TriangleMarker");
		m_RangeDisplay = GameObject.Find ("Grid").GetComponent<LineRenderer> ();
	}
	
	public virtual void DisplayHUD(bool active)
	{
		if(active == true)
		{
			HUD.Instance.ActiveTargetPanel(true);
			HUD.Instance.ActiveActionPanel(true);
			HUD.Instance.ActiveInfoPanel(true);
			
			TargetPanel.Instance.DisplayArmorBar(true, m_MySpaceObject.GetArmorPoints ("Current"), m_MySpaceObject.GetArmorPoints ("Max"));
			TargetPanel.Instance.DisplayBuffs(true, m_MySpaceObject.GetBuffController().GetBonus(),m_MySpaceObject.GetBuffController().GetMalus());
			TargetPanel.Instance.DisplayPassives(true, m_MySpaceObject.GetPassiveController().GetPassives());
			TargetPanel.Instance.DisplayFeature (true, "MOV", m_MySpaceObject.GetMoveRange ());
			TargetPanel.Instance.DisplayFeature (true, "ESQ", m_MySpaceObject.GetEvade ());
			TargetPanel.Instance.DisplayFeature (true, "DEF", m_MySpaceObject.GetDefence ());
			TargetPanel.Instance.DisplayFeature (true, "CRT", m_MySpaceObject.GetCritChance ());
			TargetPanel.Instance.DisplayShieldBar (true, m_MySpaceObject.GetShieldPoints(), m_MySpaceObject.GetArmorPoints("Max")/2);
			TargetPanel.Instance.DisplayTargetImage(true, m_MySpaceObject.GetHUDSprite());
			
			ActionPanel.Instance.DisplayEnergyBar (true, m_MySpaceObject.GetEnergyPoints("Current"), m_MySpaceObject.GetEnergyPoints ("Max"));
			ActionPanel.Instance.DisplaySlots (true, m_MySpaceObject.GetWeaponController().GetEquipments());
			ActionPanel.Instance.DisplayEngineButton (!m_MySpaceObject.GetHasMoved());
			/*if(m_MyShip.GetHasMoved() == true)
				ActionPanel.Instance.DisplayEngineButton (false);
			else
				ActionPanel.Instance.DisplayEngineButton (true);*/
			
			InfoPanel.Instance.SetInfos (m_MySpaceObject.GetWeaponController().GetEquipments(), m_MySpaceObject.GetPassiveController().GetPassives(), m_MySpaceObject.GetBuffController().GetBonus(), m_MySpaceObject.GetBuffController().GetMalus());
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
		m_RangeDisplay.enabled = true;
		m_RangeDisplay.SetWidth (0.1f, 0.1f);
		m_RangeDisplay.SetVertexCount (361);
		m_RangeDisplay.SetColors (new Color (230, 230, 0, 1),new Color (230, 230, 0, 1));
		for(int iter =0; iter<361;iter++)
		{
			m_RangeDisplay.SetPosition(iter,transform.position+new Vector3(range*Mathf.Cos(iter*Mathf.Deg2Rad),0.1f,range*Mathf.Sin(iter*Mathf.Deg2Rad)));
		}
	}

	public void SetHUDSprite(string spritePath)
	{
		m_HUDSprite = Resources.Load<Sprite>(spritePath);
		if (m_HUDSprite == null)
			Debug.LogError ("invalid spritePath");
	}

	public Sprite GetHUDSprite()
	{
		return m_HUDSprite;
	}

	public Sprite GetHUDMarker()
	{
		return m_HUDMarker;
	}
}
