using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneManager : MonoBehaviour {

	public static ZoneManager Instance;

	protected LineRenderer m_ActionRangeMaxLine;
	protected LineRenderer m_ActionRangeMinLine;
	protected LineRenderer m_ActionRangeSightLine;

	void Awake()
	{
		Instance = this;
		m_ActionRangeMaxLine = transform.Find ("ActionRangeMax").GetComponent<LineRenderer> ();
		m_ActionRangeMinLine = transform.Find ("ActionRangeMin").GetComponent<LineRenderer> ();
		m_ActionRangeSightLine = transform.Find ("ActionRangeSight").GetComponent<LineRenderer> ();
	}

	public void DisplaySightBorderLine(List<Tile> borderTiles)
	{
		foreach(Tile border in borderTiles)
		{
			border.GetComponent<Tile>().SetFullColor(new Color(0,255,0,1));
		}


		HashSet<Tile> openBorderTiles = new HashSet<Tile> (borderTiles);
		HashSet<Tile> closeBorderTiles = new HashSet<Tile> ();
		int borderTilesCount = openBorderTiles.Count;
		int iterPosition = 0;
		Tile currentTile = borderTiles [0];

		m_ActionRangeSightLine.enabled = true;
		m_ActionRangeSightLine.SetVertexCount (borderTilesCount + 1);
		m_ActionRangeSightLine.SetPosition(iterPosition, currentTile.transform.position);

		Debug.Log ("borderTilesCount = " + borderTilesCount);
		for(int iter=1; iter<=borderTilesCount;iter++)
		{
			Debug.Log("iter line = " + iter);
			openBorderTiles.Remove(currentTile);
			//closeBorderTiles.Add(currentTile);
			foreach(Tile neighboor in currentTile.GetNeighboors())
			{
				if(openBorderTiles.Contains(neighboor))
				{
					Debug.Log("next border tile found");
					iterPosition++;
					borderTilesCount--;
					currentTile = neighboor;
					m_ActionRangeSightLine.SetPosition(iter,currentTile.transform.position);
					break;
				}
			}
		}
		m_ActionRangeSightLine.SetPosition(iterPosition,borderTiles [0].transform.position); // rebouclage sur le dernier tile
	}
}
