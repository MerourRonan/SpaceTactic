using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveController : MonoBehaviour {

	protected SpaceObject m_MySpaceObject;

	/**** Pathfindind and Position ****/
	protected int m_Clearance;
	public Tile m_ReferenceTile;
	public List<Tile> m_Tiles;

	/**** Variables ****/
	protected float m_MoveSpeed;
	protected float m_RotationSpeed;

	/**** Control boolean ****/
	protected bool m_CanMove;
	protected bool m_HasMoved;

	void Awake()
	{
		m_MySpaceObject = transform.GetComponent<SpaceObject> ();
		m_Tiles = new List<Tile>();
		m_CanMove = true;
		m_HasMoved = false;

		m_MoveSpeed = 20;
		m_RotationSpeed = 20f;
	}

	public IEnumerator ShipMovement(List<Tile> Path)
	{
		GameManager.Instance.SetRunning (true);
		yield return StartCoroutine (Move (Path));
		GameManager.Instance.SetRunning (false);
	}
	
	IEnumerator Move(List<Tile> Path)
	{
		float currentMoveSpeed = 0;
		float currentRotationSpeed = 0;
		int waypointNumber = 0;
		Vector3 Destination = Path [Path.Count - 1].transform.position;
		Vector3 currentWaypoint = Path [waypointNumber].transform.position;
		
		
		while(Vector3.Distance(Destination,transform.position)>0)
		{
			Vector3 dir = currentWaypoint-m_MySpaceObject.GetBody().transform.position;
			currentMoveSpeed = Mathf.Lerp(currentMoveSpeed,m_MySpaceObject.GetMoveSpeed(),Time.fixedDeltaTime);
			m_MySpaceObject.GetBody().transform.rotation = Quaternion.Lerp(m_MySpaceObject.GetBody().transform.rotation,Quaternion.LookRotation(currentWaypoint-transform.position), m_MySpaceObject.GetRotationSpeed()*Time.fixedDeltaTime);
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, m_MySpaceObject.GetMoveSpeed()*Time.deltaTime);
			if(transform.position == currentWaypoint && currentWaypoint != Destination)
			{
				waypointNumber++;
				currentWaypoint = Path [waypointNumber].transform.position;
			}
			yield return new WaitForEndOfFrame();
		}
		yield return StartCoroutine (Align());
	}
	
	IEnumerator Align()
	{
		Vector3[] alignementVectors = new Vector3[4]{Vector3.forward,Vector3.left,-Vector3.back,Vector3.right};
		Vector3 minAlignementVector = Vector3.zero;
		float minAngle = 180;
		
		for(int number =0 ;number<4;number++)
		{
			float currentAngle = Vector3.Angle(m_MySpaceObject.GetBody().transform.forward,alignementVectors[number]);
			if(currentAngle<minAngle)
			{
				minAngle = currentAngle;
				minAlignementVector = alignementVectors[number];
			}
		}
		while(Vector3.Angle(m_MySpaceObject.GetBody().transform.forward,minAlignementVector) != 0)
		{
			m_MySpaceObject.GetBody().transform.rotation = Quaternion.RotateTowards(m_MySpaceObject.GetBody().transform.rotation,Quaternion.LookRotation(minAlignementVector),90*Time.fixedDeltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

	public virtual void SetTiles(Tile tile)
	{
		m_Tiles.Add (tile);
	}

	public void SetClearance(string size)
	{
		if(size == "Small")
		{
			m_Clearance = 1;
		}
		else if(size == "Medium")
		{
			m_Clearance = 2;
		}
		else if(size == "Large")
		{
			m_Clearance = 3;
		}
		else
		{
			Debug.LogError("invalid argument : " + size);
		}
		
	}

	public int GetClearance()
	{
		return m_Clearance;
	}

	public virtual List<Tile> GetTiles()
	{
		return m_Tiles;
	}

	public void SetReferenceTile(Tile referenceTile)
	{
		m_ReferenceTile = referenceTile;
	}
	
	public Tile GetReferenceTile()
	{
		return m_ReferenceTile;
	}

	public float GetMoveSpeed()
	{
		return m_MoveSpeed;
	}
	
	public float GetRotationSpeed()
	{
		return m_RotationSpeed;
	}

	public void SetHasMoved(bool arg)
	{
		m_HasMoved = arg;
	}

	public void SetCanMove(bool arg)
	{
		m_CanMove = arg;
	}

	public bool GetHasMoved()
	{
		return m_HasMoved;
	}

	public bool GetCanMove()
	{
		return m_CanMove;
	}
}
