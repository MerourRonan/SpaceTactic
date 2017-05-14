using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	protected bool m_GamePaused;
	protected bool m_GameOver;
	protected bool m_Running;

	protected int m_Turn;
	protected string m_Phase;

	public List<SpaceObject> m_PlayerShips;
	public List<SpaceObject> m_PlayerSmallShips;
	public List<SpaceObject> m_PlayerLargeShips;
	//List<GameObject> m_PlayerMissiles;
	public List<SpaceObject> m_EnemyShips;
	public List<SpaceObject> m_EnemySmallShips;
	public List<SpaceObject> m_EnemyLargeShips;
	//List<GameObject> m_EnemyMissiles;
	public List<SpaceObject> m_AllSpaceObjects;
	public List<SpaceObject> m_AllShips;
	//List<GameObject> m_AllMissiles;
	public List<SpaceObject> m_AllDecorObjects;


	void Awake()
	{
		Instance = this;
		m_PlayerShips=new List<SpaceObject>();
		m_PlayerSmallShips=new List<SpaceObject>();
		m_PlayerLargeShips=new List<SpaceObject>();
		//m_PlayerMissiles=new List<GameObject>();
		m_EnemyShips=new List<SpaceObject>();
		m_EnemySmallShips=new List<SpaceObject>();
		m_EnemyLargeShips=new List<SpaceObject>();
		//m_EnemyMissiles=new List<GameObject>();
		m_AllSpaceObjects=new List<SpaceObject>();
		m_AllDecorObjects=new List<SpaceObject>();
		m_AllShips=new List<SpaceObject>();

		m_GamePaused = false;
		m_GameOver = false;
		m_Running = false;
		m_Turn = 1;
		m_Phase = "Player";
	}

	// Use this for initialization
	void Start () {
		CreateLevel ();
		StartCoroutine (Play());
		//Utilities.RecoverBoxTiles (Grid.Instance.GetArrayTile()[3,3], "Center", 3)
	}

	public void EndOfTurn()
	{
		Debug.Log ("new phase");
		if(m_Phase == "Player")
		{
			m_Phase = "Enemy";
		}
		else if(m_Phase == "Enemy")
		{
			m_Phase = "Neutral";
		}
		else if(m_Phase == "Neutral")
		{
			m_Phase = "Player";
			m_Turn++;
		}
		HUD.Instance.DisplayPhaseAndTurnPanel (m_Phase);
	}
	
	void TurnReset(List<SpaceObject> shipList)
	{
		foreach(SpaceObject ship in shipList)
		{
			ship.TurnReset();
		}
	}

	IEnumerator Play()
	{
		while(true)
		{
			yield return StartCoroutine (PlayerPhase());
			yield return StartCoroutine (EnemyPhase());
			yield return StartCoroutine (NeutralPhase());
		}
	}

	IEnumerator PlayerPhase()
	{
		Debug.Log("Player Phase");
		TurnReset (m_PlayerShips);
		Coroutine controlMouse = StartCoroutine (PlayerControl.Instance.ControlMouse ());
		Coroutine controlCamera = StartCoroutine (MoveCamController.Instance.PlayerControlCamera ());
		while(GetPhase() == "Player")
			yield return null;
		StopCoroutine (controlMouse);
		StopCoroutine (controlCamera);
		PlayerControl.Instance.EndTurnReset ();
	}

	IEnumerator EnemyPhase()
	{
		Debug.Log("Enemy Phase");
		TurnReset (m_EnemyShips);
		//while(GetPhase() == "Enemy")
			yield return true;
		EndOfTurn ();
	}

	IEnumerator NeutralPhase()
	{
		Debug.Log("Neutral Phase");
		TurnReset (m_EnemyShips);
		//while(GetPhase() == "Neutral")
			yield return true;
		EndOfTurn ();
	}

	IEnumerator MissilePhase(string missileTag)
	{
		throw new System.NotImplementedException();
	}

	void CreateLevel()
	{
		GridBuilder.Instance.CreateClusterGrid ();
		FleetManager.Instance.SpawnFleets ();
		/*Grid.Instance.CreateGrid ();
		SpawnDecor.Instance.SpawnDecorObject ();
		Grid.Instance.FreeCheckingRaycast ();
		Grid.Instance.ComputeAllClearance ();
		SpawnFleet.Instance.SpawnFleets ();*/

		//Grid.Instance.LevelCreationResetTile ();				// Reset Tile to free state (overlapping problem with object bounds during spanwDecor)
		//Grid.Instance.ComputeAllClearance ();
		//Grid.Instance.TileOverlapSphere ();

	}

	public int GetTurn()
	{
		return m_Turn;
	}

	public string GetPhase()
	{
		return m_Phase;
	}

	public bool GetRunning()
	{
		return m_Running;
	}

	public void SetRunning(bool arg)
	{
		m_Running = arg;
	}



	public List<SpaceObject> GetList(string ListName)
	{
		if (ListName == "AllSpaceObjects") 
		{
			return m_AllSpaceObjects;
		}
		else if (ListName == "PlayerShips") 
		{
			return m_PlayerShips;
		}
		else if (ListName == "PlayerSmallShips") 
		{
			return m_PlayerSmallShips;
		}
		else if (ListName == "PlayerLargeShips") 
		{
			return m_PlayerLargeShips;
		}
		else if (ListName == "EnemyShips") 
		{
			return m_EnemyShips;
		}
		else if (ListName == "EnemySmallShips") 
		{
			return m_EnemySmallShips;
		}
		else if (ListName == "EnemyLargeShips") 
		{
			return m_EnemyLargeShips;
		}
		else if (ListName == "AllDecorObjects") 
		{
			return m_AllDecorObjects;
		}
		else if (ListName == "AllShips") 
		{
			return m_AllShips;
		}
		else
		{
			Debug.LogError("invalid argument");
			return null;
		}
	}

	public void AddToList(SpaceObject spaceObject)
	{
		string type = spaceObject.GetObjectType();
		m_AllSpaceObjects.Add (spaceObject);
		if (type == "Decor") 
		{
			m_AllDecorObjects.Add(spaceObject);
		}
		else if (type == "Ship") 
		{
			m_AllShips.Add(spaceObject);
			HUD.Instance.SpawnMarker(spaceObject);
			string faction = spaceObject.GetFaction();
			string size = spaceObject.GetSize();
			if(faction == "Player")
			{
				m_PlayerShips.Add(spaceObject);
				if(size == "Small")
				{
					m_PlayerSmallShips.Add(spaceObject);
				}
				else if(size == "Large")
				{
					m_PlayerLargeShips.Add(spaceObject);
				}
				else
				{
					Debug.LogError("invalid argument");
				}
			}
			else if(faction == "Enemy")
			{
				m_EnemyShips.Add(spaceObject);
				if(size == "Small")
				{
					m_EnemySmallShips.Add(spaceObject);
				}
				else if(size == "Large")
				{
					m_EnemyLargeShips.Add(spaceObject);
				}
				else
				{
					Debug.LogError("invalid size argument");
				}
			}
			else
			{
				Debug.LogError("invalid faction argument");
				Debug.Log("faction = " + faction);
			}
		}
		else
		{
			Debug.LogError("invalid argument : " + type);
		}
	}

	public void RemoveToList(SpaceObject spaceObject)
	{
		string type = spaceObject.GetObjectType();
		m_AllSpaceObjects.Remove (spaceObject);
		if (type == "Decor") 
		{
			m_AllDecorObjects.Remove(spaceObject);
		}
		else if (type == "Ship") 
		{
			m_AllShips.Remove(spaceObject);
			string faction = spaceObject.GetFaction();
			string size = spaceObject.GetSize();
			if(faction == "Player")
			{
				m_PlayerShips.Remove(spaceObject);
				if(size == "Small")
				{
					m_PlayerSmallShips.Remove(spaceObject);
				}
				else if(size == "Large")
				{
					m_PlayerLargeShips.Remove(spaceObject);
				}
				else
				{
					Debug.LogError("invalid argument");
				}
			}
			else if(faction == "Enemy")
			{
				m_EnemyShips.Remove(spaceObject);
				if(size == "Small")
				{
					m_EnemySmallShips.Remove(spaceObject);
				}
				else if(size == "Large")
				{
					m_EnemyLargeShips.Remove(spaceObject);
				}
				else
				{
					Debug.LogError("invalid argument");
				}
			}
			else
			{
				Debug.LogError("invalid argument");
			}
		}
		else
		{
			Debug.LogError("invalid argument");
		}
	}

	




}
