using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour {

	public static PlayerControl Instance;

	GameObject ObjectSelected;
	Weapon WeaponSelected;

	GameObject ObjectToAttack;
	Weapon EquipmentToAttack;
	int EquipmentToAttackNumber;

	GameObject m_TileSelected;
	GameObject m_TileBelowMouse;
	string Command;
	int SlotSelected;
	
	void Awake()
	{
		Instance = this;
		ObjectSelected = null;
		m_TileSelected = null;
		m_TileBelowMouse = null;
		EquipmentToAttackNumber = -1;
	}

	public IEnumerator ControlMouse()
	{
		while(true)
		{
			if(GameManager.Instance.GetRunning() == false)
			{
				SetTileBelowMouse ();
				if(Input.GetMouseButtonUp(0))
				{
					ClicSelect();
				}
				if(Input.GetMouseButtonUp(1))
				{
					ClicAction();
				}
				if(Input.GetKeyDown(KeyCode.Escape))
				{
					ActionManager.Instance.Unselect();
				}
				if(Input.GetKeyDown(KeyCode.Tab))
				{
					AttackManager.Instance.ChangeTarget();
				}
			}
			yield return null;
		}
	}

	void ClicSelect()
	{
		//Debug.Log (UnityEngine.E);
		if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit,Mathf.Infinity,(1<<LayerMask.NameToLayer ("Tile"))))
			{
				//SetTileSelected(hit.transform.gameObject);
				ActionManager.Instance.SelectTile(hit.transform.GetComponent<Tile>());
			}
		}
	}

	void ClicAction()
	{
		if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
		{
			ActionManager.Instance.StartAction();
		}
	}

	public void ValidAction()
	{
		//StartCoroutine(WeaponSelected.UseEquipments(ObjectToAttack,EquipmentToAttack));
	}

	public void SelectSlot(int slotNumber)
	{
		/*ClicReset ();
		Command = "Slot";
		if (slotNumber >= 0 && slotNumber <= 5)
			SlotSelected = slotNumber;
		WeaponSelected = ObjectSelected.GetComponent<Ship> ().GetEquipment (slotNumber);
		//Grid.Instance.ActiveAttackRange (TileSelected, WeaponSelected.GetRange ("Min"), WeaponSelected.GetRange ("Max"));
		Grid.Instance.ComputeDisplayAttackRange(m_TileSelected, WeaponSelected.GetRange ("Min"), WeaponSelected.GetRange ("Max"));*/
	}

	void SetTileBelowMouse()
	{
		/*if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit,Mathf.Infinity,(1<<LayerMask.NameToLayer ("Tile"))) && hit.transform.gameObject != TileBelowMouse)
			{
				if(TileBelowMouse != null)
				{
					GetTileBelowMouse().GetComponent<Tile>().SetIsBelowMouse(false);
					TileBelowMouse = null;
					if(Command == "Engine")
					{
						Grid.Instance.ResetDisplayPathfinding();
					}
				}
				if(hit.transform.gameObject != null)
				{
					TileBelowMouse = hit.transform.gameObject;
					TileBelowMouse.GetComponent<Tile>().SetIsBelowMouse(true);
					if(Command == "Engine")
					{
						Grid.Instance.ComputeDisplayPathFinding(ObjectSelected,TileBelowMouse);
					}
				}
			}
		}*/

		if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit,Mathf.Infinity,(1<<LayerMask.NameToLayer ("Tile"))) && hit.transform.gameObject != null)
			{
				ActionManager.Instance.SetTileUnderMouse(hit.transform.GetComponent<Tile>());
			}
		}
	}



	public GameObject GetObjectSelected()
	{
		return ObjectSelected;
	}

	public GameObject GetTileSelected()
	{
		return m_TileSelected;
	}

	public GameObject GetTileBelowMouse()
	{
		return m_TileBelowMouse;
	}

	public void EndTurnReset()
	{
		ObjectSelected = null;
		m_TileSelected = null;
		m_TileBelowMouse = null;
		Command = "";
		SlotSelected = 0;
	}

	public GameObject GetObjectToAttack()
	{
		return ObjectToAttack;
	}

	public Weapon GetWeaponSelected()
	{
		return WeaponSelected;
	}

	public void SetEquipmentToAttack(int number=-1)
	{
		/*if(EquipmentToAttackNumber == number)
		{
			EquipmentToAttack = null;
			EquipmentToAttackNumber =-1;
		}
		else
		{
			EquipmentToAttackNumber = number;
			Weapon[] equipments = ObjectToAttack.GetComponent<Ship> ().GetEquipments ();
			if (number == -1)
				EquipmentToAttack = null;
			else if (equipments [number] != null)
				EquipmentToAttack = equipments [number];
			else
				Debug.LogError ("Error : number = " + number);
		}*/
	}


}
