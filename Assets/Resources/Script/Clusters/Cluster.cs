using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {

	public Tile[,] m_ClusterTiles;
	public int m_ClusterDimension;
	public int m_GridRow;
	public int m_GridColumn;

	public int m_SpawnChance;
	public int m_Fullness;

	void Awake()
	{
		m_ClusterDimension = 6;
	}

	public int GetFullness()
	{
		return m_Fullness;
	}

	public void AddChildTiles()
	{
		m_ClusterTiles = new Tile[m_ClusterDimension,m_ClusterDimension];
		foreach(Transform tile in transform.Find("Tiles"))
		{
			int tileRow = Mathf.RoundToInt(Mathf.Abs(tile.position.x-tile.parent.position.x)) ;
			int tileColumn = Mathf.RoundToInt(Mathf.Abs(tile.position.z-tile.parent.position.z)) ;
			m_ClusterTiles[tileRow,tileColumn]=tile.GetComponent<Tile>();
		}
	}

	public Tile[,] GetClusterTiles()
	{
		return m_ClusterTiles;
	}

	public int GetClusterDimension()
	{
		return m_ClusterDimension;
	}

	public void SetGridRow(int gridRow)
	{
		m_GridRow = gridRow;
	}

	public void SetGridColumn(int gridColumn)
	{
		m_GridColumn = gridColumn;
	}

	public int GetSpawnChance()
	{
		return m_SpawnChance;
	}
	
}
