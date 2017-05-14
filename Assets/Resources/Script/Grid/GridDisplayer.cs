using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridDisplayer : Grid2 {

	public static GridDisplayer Instance;
	


	protected Color m_NoColor;
	protected Color m_AttackRangeColor;
	protected Color m_MoveRangeColor;
	protected Color m_DestinationColor;

	protected Color m_UnderMouseColor;
	protected Color m_SelectionColor;

	void Awake()
	{
		Instance = this;


		m_NoColor = new Color (0, 0, 0, 0);
		m_MoveRangeColor = new Color (0, 0, 255, 0.5f);
		m_DestinationColor = new Color (0, 255, 0, 0.5f);
		m_AttackRangeColor = new Color (255, 222, 0, 0.5f);

		m_UnderMouseColor = new Color (0, 0, 255, 0.5f);
		m_SelectionColor = new Color (0, 255, 0, 0.5f);
	}

	public void DisplayMoveRange(bool active)
	{
		if(active == false)
		{
			foreach (Tile tile in GridPathFinder.Instance.GetMoveRangeTiles())
				tile.SetFullColor(m_NoColor);
		}
		else
		{
			RenderMoveGrid();
		}
	}

	public void DisplayAttackRange(bool active)
	{
		if(active == false)
		{
			foreach (Tile tile in AttackManager.Instance.GetAttackRangeTiles())
				tile.SetFullColor(m_NoColor);
		}
		else
		{
			RenderAttackGrid();
		}
	}

	public void DisplayDestinationTiles(bool active)
	{
		if(active == false)
		{
			foreach (Tile tile in GridPathFinder.Instance.GetDestinationTiles())
				tile.SetFullColor(m_NoColor);
		}
		else
		{
			RenderMoveGrid ();
		}
	}

	public void DisplayPathFindingLine(bool active, SpaceObject objectSelected=null)
	{
		LinePath.enabled=false;
		if(active == true)
		{
			if(GridPathFinder.Instance.GetDestinationTiles().Count !=0)
			{
				Vector3 offsetVector = objectSelected.transform.Find("Body").position-objectSelected.GetReferenceTile().transform.position;
				LinePath.enabled=true;
				LinePath.SetVertexCount(GridPathFinder.Instance.GetPathFindingTiles().Count);
				for(int number=0;number<GridPathFinder.Instance.GetPathFindingTiles().Count;number++)
					LinePath.SetPosition(number,GridPathFinder.Instance.GetPathFindingTiles()[number].transform.position+offsetVector);
			}
		}
	}
	
	private void RenderMoveGrid()
	{
		foreach(Tile tile in GridPathFinder.Instance.GetMoveRangeTiles())
			tile.SetFullColor(m_MoveRangeColor);

		foreach(Tile tile in GridPathFinder.Instance.GetDestinationTiles())
			tile.SetFullColor(m_DestinationColor);
	}

	private void RenderAttackGrid()
	{
		foreach(Tile tile in AttackManager.Instance.GetAttackRangeTiles())
			tile.SetFullColor(m_AttackRangeColor);
	}

	public void DisplayUnderMouseBracket(bool active)
	{
		if(ActionManager.Instance.GetTileUnderMouse ()!=null)
		{
			if(active == false)
			{
				ActionManager.Instance.GetTileUnderMouse ().GetComponent<Tile> ().SetBracketColor(m_NoColor);
			}
			else
			{
				RenderBracket();
			}
		}
	}

	public void DisplaySelectionBracket(bool active)
	{
		if(ActionManager.Instance.GetTileSelected ()!=null)
		{
			if(active == false)
			{
				ActionManager.Instance.GetTileSelected ().GetComponent<Tile> ().SetBracketColor(m_NoColor);
			}
			else
			{
				RenderBracket();
			}
		}
	}

	private void RenderBracket()
	{
		if(ActionManager.Instance.GetTileUnderMouse ()!=null)
			ActionManager.Instance.GetTileUnderMouse ().GetComponent<Tile> ().SetBracketColor(m_UnderMouseColor);
		if(ActionManager.Instance.GetTileSelected ()!=null)
			ActionManager.Instance.GetTileSelected ().GetComponent<Tile> ().SetBracketColor(m_SelectionColor);
	}
}
