using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionManager : MonoBehaviour {

	public static ActionManager Instance;

	protected string m_ActionManagerUser;
	protected Tile m_TileSelected;
	protected Tile m_TileUnderMouse;
	protected SpaceObject m_SpaceObjectSelected;
	protected Weapon m_WeaponSelected;
	protected int m_SlotSelectedNumber;
	protected bool m_EngineSelected;

	//protected GameObject m_SpaceObjectTargeted;
	//protected LargeWeapon m_LargeWeaponTargeted;
	//protected List<GameObject> m_TilesTargeted;

	void Awake()
	{
		Instance = this;
		m_SlotSelectedNumber = -1;
		//m_TilesTargeted = new List<GameObject> ();

	}

	public void SelectTile(Tile tile)
	{
		if (tile == null)
			return;

		ResetOnObjectSelection ();
		m_TileSelected = tile;
		GridDisplayer.Instance.DisplaySelectionBracket (true);
		if(tile.GetSpaceObjectOnTile ()!=null && tile.GetSpaceObjectOnTile().tag == "Player")
		{
			m_SpaceObjectSelected = tile.GetSpaceObjectOnTile ();
			m_SpaceObjectSelected.GetHUDController().DisplayHUD(true);
		}
	}

	public void StartAction()
	{
		if(m_EngineSelected == true)
		{
			int clearance = m_SpaceObjectSelected.GetClearance();
			Tile destinationTile = GridPathFinder.Instance.RecomputingDestination(m_TileUnderMouse,clearance);
			StartCoroutine(MoveManager.Instance.StartMove(destinationTile,m_SpaceObjectSelected));
		}
		else if(m_WeaponSelected != null)
		{
			if(m_TileUnderMouse != null)
			{
				Debug.Log("changing target");
				AttackManager.Instance.ChangeTarget(m_TileUnderMouse.GetSpaceObjectOnTile());
			}
		}
	}

	public void SelectEngine()
	{
		int shipClearance = m_SpaceObjectSelected.GetClearance ();

		ResetOnButtonSelection ();
		m_EngineSelected = true;

		Grid2.Instance.ReleaseTile (m_SpaceObjectSelected.GetReferenceTile (), m_SpaceObjectSelected);

		GridPathFinder.Instance.ComputeMoveRange (m_SpaceObjectSelected);
		Debug.Log ("move range tile count " + GridPathFinder.Instance.GetMoveRangeTiles ().Count);
		GridPathFinder.Instance.ComputeDestinationTiles (m_TileUnderMouse, shipClearance, GridPathFinder.Instance.GetMoveRangeTiles ());
		GridPathFinder.Instance.ComputePathFinding(m_SpaceObjectSelected,GridPathFinder.Instance.RecomputingDestination (m_TileUnderMouse, shipClearance),GridPathFinder.Instance.GetMoveRangeTiles () );

		GridDisplayer.Instance.DisplayMoveRange (true);
		GridDisplayer.Instance.DisplayPathFindingLine (true,m_SpaceObjectSelected);
		GridDisplayer.Instance.DisplayDestinationTiles (true);

		Grid2.Instance.LockTile (m_SpaceObjectSelected.GetReferenceTile (), m_SpaceObjectSelected);
	}

	public void SelectSlot(int slotNumber)
	{
		
		if (m_SlotSelectedNumber != slotNumber) {
			ResetOnButtonSelection ();
			m_SlotSelectedNumber = slotNumber;
			if (slotNumber >= 0 && slotNumber <= 5)
				m_WeaponSelected = m_SpaceObjectSelected.GetWeaponController ().GetEquipment (slotNumber);
			AttackManager.Instance.ComputeAttackRange (m_SpaceObjectSelected);
			GridDisplayer.Instance.DisplayAttackRange (true);
			m_SpaceObjectSelected.GetHUDController ().DisplayActionRange (true, m_WeaponSelected.GetRange ("Max"));
		} else {
			ResetOnButtonSelection ();
		}
		Debug.Log ("m_SlotSelectedNumber = " + m_SlotSelectedNumber);
		Debug.Log ("slotNumber = " + slotNumber);
	}

	public void ValidAction()
	{

		if (m_WeaponSelected != null)
			AttackManager.Instance.ValidAttack ();
		m_SpaceObjectSelected.GetHUDController().DisplayHUD(true);
	}

	protected void ResetOnObjectSelection()
	{
		HUD.Instance.ActiveTargetPanel(false);
		HUD.Instance.ActiveActionPanel(false);
		HUD.Instance.ActiveInfoPanel(false);
		HUDMarkerManager.Instance.DestroyTargetMarkers ();

		GridDisplayer.Instance.DisplayMoveRange (false);
		GridDisplayer.Instance.DisplayPathFindingLine (false);
		GridDisplayer.Instance.DisplayAttackRange (false);
		GridDisplayer.Instance.DisplaySelectionBracket (false);

		AttackManager.Instance.ResetManager ();

		m_EngineSelected = false;
		m_TileSelected = null;
		m_SpaceObjectSelected = null;
		m_WeaponSelected = null;

		//m_SpaceObjectTargeted = null;
		//m_LargeWeaponTargeted = null;
		//m_TilesTargeted.Clear ();
	}

	public void ResetOnButtonSelection()
	{
		GridDisplayer.Instance.DisplayMoveRange (false);
		GridDisplayer.Instance.DisplayPathFindingLine (false);
		GridDisplayer.Instance.DisplayAttackRange (false);

		HUDMarkerManager.Instance.DestroyTargetMarkers ();
		AttackManager.Instance.ResetManager ();

		m_EngineSelected = false;
		m_WeaponSelected = null;
		m_SlotSelectedNumber = -1;
		//m_SpaceObjectTargeted = null;
		//m_LargeWeaponTargeted = null;
		//m_TilesTargeted.Clear ();
	}

	public void SetActionManagerUser(string user)
	{
		m_ActionManagerUser = user;
	}

	public void SetTileUnderMouse(Tile tile)
	{
		if(tile != m_TileUnderMouse)
		{
			GridDisplayer.Instance.DisplayUnderMouseBracket (false);
			m_TileUnderMouse = tile;
			GridDisplayer.Instance.DisplayUnderMouseBracket (true);
			if (m_EngineSelected == true)
			{
				int shipClearance = m_SpaceObjectSelected.GetClearance ();
				Grid2.Instance.ReleaseTile (m_SpaceObjectSelected.GetReferenceTile (), m_SpaceObjectSelected);
				GridPathFinder.Instance.ComputeDestinationTiles (m_TileUnderMouse, shipClearance, GridPathFinder.Instance.GetMoveRangeTiles ());
				GridPathFinder.Instance.ComputePathFinding(m_SpaceObjectSelected,GridPathFinder.Instance.RecomputingDestination (m_TileUnderMouse, shipClearance),GridPathFinder.Instance.GetMoveRangeTiles ());
				GridDisplayer.Instance.DisplayPathFindingLine (true,m_SpaceObjectSelected);
				GridDisplayer.Instance.DisplayDestinationTiles (true);
				Grid2.Instance.LockTile (m_SpaceObjectSelected.GetReferenceTile (), m_SpaceObjectSelected);
			}
		}
	}

	public void Unselect()
	{
		if (m_WeaponSelected != null || m_EngineSelected == true)
			ResetOnButtonSelection ();
		else if (m_SpaceObjectSelected != null)
			ResetOnObjectSelection ();
	}

	/**** Set ****/
	public void SetSpaceObjectSelected(SpaceObject spaceObject)
	{
		m_SpaceObjectSelected = spaceObject;
	}

	/**** Get ****/
	public Tile GetTileSelected()
	{
		return m_TileSelected;
	}
	public Tile GetTileUnderMouse()
	{
		return m_TileUnderMouse;
	}
	public Weapon GetWeaponSelected()
	{
		return m_WeaponSelected;
	}
	public SpaceObject GetSpaceObjectSelected()
	{
		return m_SpaceObjectSelected;
	}
}
