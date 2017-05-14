using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	
	public GameObject m_ObjectOnTile;
	public bool m_IsFree; // la case est inoccupée (vaisseau, obstacle, autre)
	public bool m_CanShootThrough; // un tir peut passer au travers de la case 
	protected bool m_CanMoveThrough; // déplacement possible sur et au travers de la case 

	public List<Tile> m_TilesNeighboors;
	public int m_Clearance;
	protected int m_ThreatValue;
	protected int m_GridRow;
	protected int m_GridColumn;

	protected Color ColorFree;
	protected Color ColorOccupied;
	protected Color ColorMoveRange;
	protected Color ColorPathFinding;
	protected Color ColorFiringRange;
	protected Color ColorEnemyBracket;
	protected Color ColorPlayerBracket;
	protected Color ColorMouseOverBracket;
	protected Color ColorMouseSelectBracket;
	protected bool[] TileState;
	protected bool[] BracketState;
	protected bool m_ActiveBracket;

	protected SpriteRenderer SpriteFull;
	protected SpriteRenderer SpriteBracket;
	protected SpriteRenderer SpriteBorder;

	//Bracket
	protected bool isBelowMouse;
	protected bool IsSelected;

	protected bool isPathfindindTile;
	protected bool isMoveRangeTile;
	protected bool isAttackRangeTile;
	protected bool isFreeTile;

	//Color Bracket
	protected Color BracketColorMouseOver;
	protected Color BracketColorSelection;

	/*****************************************************************************************************/
	//Cluster variables
	public int m_ClusterRow;
	public int m_ClusterColumn;

	/*private void OnClusterSpawn()
	{
		SettingClusterVariables ();
		SettingGridVariables ();
	}

	private void SettingClusterVariables()
	{
		transform.parent.parent.GetComponent<Cluster> ().AddTile (this.gameObject);
	}

	private void SettingGridVariables()
	{
		if(m_IsFree == true)
			Grid.Instance.AddFreeTile(this.gameObject);
	}*/

	public int GetClusterRow()
	{
		return m_ClusterRow;
	}

	public int GetClusterColumn()
	{
		return m_ClusterColumn;
	}

	/*****************************************************************************************************/
	protected class TileBox
	{
		public GameObject m_Tile;
		public int m_distanceH;
		public int m_distanceV;

		public TileBox(GameObject tile, int distanceH, int distanceV)
		{
			m_Tile = tile;
			m_distanceV = distanceV;
			m_distanceH = distanceH;
		}
	}
	

	void Awake()
	{
		InitVar ();
		InitTagLayer ();
		DefineColor ();
		SetFullColor (ColorFree);
		SetBracketColor (ColorFree);
		//OnClusterSpawn ();
	}

	void Start()
	{
		DebugFreeWithColor ();
		//DebugClearanceWithColor ();
	}

	void InitVar()
	{
		//m_IsFree = true;
		m_CanShootThrough = true;
		m_CanMoveThrough = true;
		//m_TilesNeighboors = new List<Tile>();
		TileState = new bool[4];
		BracketState = new bool[3];
		SpriteFull = transform.Find ("SpriteFull").GetComponent<SpriteRenderer> ();
		SpriteBorder = transform.Find ("SpriteBorder").GetComponent<SpriteRenderer> ();
		SpriteBracket = transform.Find ("SpriteBracket").GetComponent<SpriteRenderer> ();
	}

	void InitTagLayer()
	{
		transform.tag = "Tile";
		gameObject.layer = LayerMask.NameToLayer ("Tile");
		transform.Find("NavTrigger").tag = "Nav";
		transform.Find("NavTrigger").gameObject.layer = LayerMask.NameToLayer ("Nav");
	}

	public void DefineColor()
	{
		ColorFree = new Color (0, 0, 0, 0); // empty
		ColorOccupied = new Color (255,0,0,0.35f); // occupied
		ColorMoveRange = new Color (0,0,255,0.35f);
		ColorPathFinding = new Color (0,255,0,0.35f);
		ColorFiringRange = new Color (255,255,0,0.35f);
		ColorEnemyBracket = new Color (255, 0, 0, 1);
		ColorPlayerBracket = new Color (0, 255, 0, 1);
		ColorMouseOverBracket = new Color (255, 255, 0, 1);
		ColorMouseSelectBracket = new Color (125, 255, 0, 1);

		BracketColorMouseOver = new Color (0, 150, 255, 1);
		BracketColorSelection = new Color (255, 255, 150, 1);
	}

	void BracketManager()
	{
		if (IsSelected == true) {
			SetBracketColor (BracketColorSelection);
		} else if (isBelowMouse == true) {
			SetBracketColor (BracketColorMouseOver);
		} else {
			SetBracketColor (ColorFree);
		}
	}

	void SpriteFullManager()
	{
		if(isPathfindindTile == true)
		{
			SetFullColor(ColorPathFinding);
		}
		else if (isMoveRangeTile == true)
		{
			SetFullColor(ColorMoveRange);
		}
		else if (isAttackRangeTile == true)
		{
			SetFullColor(ColorFiringRange);
		}
		else
		{
			SetFullColor(ColorFree);
		}
	}

	public void AddNeighboors(Tile NewTile)
	{
		if(m_TilesNeighboors.Contains(NewTile) == false)
		{
			m_TilesNeighboors.Add(NewTile);
		}
	}

	/*public void ComputeMyClearance()
	{
		for (int clearanceLevel = 1; clearanceLevel <=3; clearanceLevel++) 
		{
			List<GameObject> clearanceBox = Utilities.RecoverClearanceBox(this.gameObject,clearanceLevel);
			m_Clearance = clearanceLevel;

			if(clearanceBox == null)
			{
				m_Clearance--;
				return;
			}

			foreach (GameObject tile in clearanceBox) 
			{
				if (tile.GetComponent<Tile> ().GetFree () == false) 
				{
					m_Clearance--;
					return;
				}
			}
		}
	}

	public void ComputeNeighboorsClearance()
	{
		List<GameObject> clearanceNeighboors = Utilities.RecoverClearanceNeighboors(this.gameObject,3);
		foreach (GameObject tile in clearanceNeighboors) 
		{
			tile.GetComponent<Tile>().ComputeMyClearance();
		}
	}*/




	//********* GET ***********//
	public SpaceObject GetSpaceObjectOnTile()
	{
		if (m_ObjectOnTile != null && m_ObjectOnTile.GetComponent<SpaceObject> () != null)
			return m_ObjectOnTile.GetComponent<SpaceObject> ();
		else
			return null;
	}
	public bool GetFree()
	{
		return m_IsFree;
	}
	public bool GetMoveThrough()
	{
		return m_CanMoveThrough;
	}
	public bool GetShootThrough()
	{
		return m_CanShootThrough;
	}

	public bool GetTileState(string arg)
	{
		if(arg == "Free")
			return TileState[0];
		else if(arg == "MoveRange")
			return TileState[1];
		else if(arg == "PathFinding")
			return TileState[2];
		else if(arg == "FiringRange")
			return TileState[3];
		else
		{
			Debug.Log("Invalid argument");
			return false;
		}
	}
	
	//********* SET ***********//
	public void SetTileState(string arg, bool arg2)
	{
		if(arg == "Free")
			TileState[0] = arg2;
		else if(arg == "MoveRange")
			TileState[1] = arg2;
		else if(arg == "PathFinding")
			TileState[2] = arg2;
		else if(arg == "FiringRange")
			TileState[3] = arg2;
		else
			Debug.Log("Invalid argument");
	}

	public void SetBracketState(string arg, bool arg2)
	{
		if(arg == "Free")
			BracketState[0] = arg2;
		else if(arg == "MouseSelect")
			BracketState[1] = arg2;
		else if(arg == "MouseOver")
			BracketState[2] = arg2;
		else
			Debug.Log("Invalid argument");
	}

	public void ResetPathfindingTile()
	{
		TileState[2] = false;
	}

	public void SetFree(bool arg, GameObject objectOnTile =null)
	{
		m_IsFree = arg;
		SetTileState("Free",arg);
		SetBracketState("Free",arg);
		if(arg == true)
		{
			m_ObjectOnTile = null;
			m_CanShootThrough = true;
			//ComputeNeighboorsClearance();
			//Grid.Instance.GetFreeTiles().Add(gameObject);
			//Grid.Instance.GetThreatTiles(m_ThreatValue).Add(this.gameObject);

		}
		if(arg == false)
		{
			m_ObjectOnTile = objectOnTile;
			SetCanShootThrough();
			//ComputeNeighboorsClearance();
			//Grid.Instance.GetFreeTiles().Remove(gameObject);
			//Grid.Instance.GetThreatTiles(m_ThreatValue).Remove(this.gameObject);
		}
	}


	public void SetBracketColor(Color color)
	{
		SpriteRenderer SpriteBracket = transform.Find ("SpriteBracket").GetComponent<SpriteRenderer> ();
		SpriteBracket.color = color;
	}
	public void SetBracketColor(Color color, float sizeMultiplicator)
	{
		transform.Find ("SpriteBracket").localScale = Vector3.one*sizeMultiplicator;
		SpriteRenderer SpriteBracket = transform.Find ("SpriteBracket").GetComponent<SpriteRenderer> ();
		SpriteBracket.color = color;
	}

	public void SetFullColor(Color color)
	{
		SpriteFull.color = color;
	}

	public List<Tile> GetNeighboors()
	{
		return m_TilesNeighboors;
	}

	public List<Tile> GetNeighboors(int clearanceLevel)
	{
		List<Tile> clearanceNeighboors = new List<Tile> ();
		//Debug.Log ("tile = " + this.gameObject.name);
		//Debug.Log ("m_TilesNeighboors.Count = " + m_TilesNeighboors.Count);
		foreach (Tile tile in m_TilesNeighboors)
			if (tile.GetClearance () >= clearanceLevel)
				clearanceNeighboors.Add (tile);
		return clearanceNeighboors ;
	}

	public void ActiveBracket(bool arg)
	{
		m_ActiveBracket = arg;
	}

	//********* EXITE & ENTER ***********//
	/*public void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag == "Decor")
		{
			SetFree(false,collider.gameObject);
			collider.gameObject.SendMessage("SetTiles",gameObject,SendMessageOptions.DontRequireReceiver);
		}
	}*/

	public void SetIsBelowMouse(bool arg)
	{
		isBelowMouse = arg;
		BracketManager ();
	}

	public void SetIsSelected(bool arg)
	{
		IsSelected = arg;
		BracketManager ();
	}

	public void SetIsMoveRangeTile(bool active)
	{
		isMoveRangeTile = active;
		SpriteFullManager ();
	}

	public void SetIsAttackRangeTile(bool active)
	{
		isAttackRangeTile = active;
		SpriteFullManager ();
	}

	public void SetIsPathFindingTile(bool active)
	{
		isPathfindindTile = active;
		SpriteFullManager ();
	}

	private void SetCanShootThrough()
	{
		string objectSize = m_ObjectOnTile.GetComponent<SpaceObject> ().GetSize();
		if (objectSize == "Large")
		{
			m_CanShootThrough = false;
		}
		else
			m_CanShootThrough = true;
	}

	protected void RenderTile()
	{
		if(SpriteFull.enabled == false)
		{
			SpriteFull.enabled = true;
			if(SpriteFull.isVisible == false)
				SpriteFull.enabled = false;
		}
	}

	public void SetClearance(int value)
	{
		m_Clearance = value;
	}

	public int GetClearance()
	{
		return m_Clearance;
	}

	public void DebugClearanceWithColor()
	{
		if (m_Clearance == 0)
			SetFullColor (new Color (255, 0, 0, 1));
		else if (m_Clearance == 1)
			SetFullColor (new Color (255, 255, 0, 1));
		else if (m_Clearance == 2)
			SetFullColor (new Color (0, 255, 0, 1));
		else if (m_Clearance == 3)
			SetFullColor (new Color (0, 0, 255, 1));
	}

	public void DebugFreeWithColor()
	{
		if (m_IsFree == false)
			SetFullColor (new Color (255, 0, 0, 0.3f));
		else
			SetFullColor (ColorFree);

	}

	public void SetGridPosition(int rowValue, int colValue)
	{
		m_GridRow = rowValue;
		m_GridColumn = colValue;
	}

	public int GetGridRow()
	{
		return m_GridRow;
	}
	public int GetGridColumn()
	{
		return m_GridColumn ;
	}

}
