using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {

	public static CameraManager Instance;

	protected static float mouseSpeed;
	protected static int boundary;

	protected static float smoothSpeed;
	protected static float CameraHeightMax;
	protected static float CameraHeightMin;
	protected static float CameraHeightCurrent;

	protected static float cameraAngleX;
	protected static float cameraAngleY;

	//bool CameraZoomOut;
	protected static int[] MapSize;
	/*protected static float limitLeft;
	protected static float limitRight;
	protected static float limitUp;
	protected static float limitDown;*/

	protected static bool CamRotating;

	protected static Vector3 m_SavedPosition;
	protected static Quaternion m_SavedRotation;

	protected static Transform m_HUDMarkers;

	protected static Vector3 m_CenterPointOnGrid;

	public static bool m_IsActiveSelectCam;

	public void Awake()
	{
		transform.position = new Vector3(0,transform.position.y,0);
		Instance = this;
		m_HUDMarkers = GameObject.Find ("HUD/Markers/IdentificationMarkers").transform;

		mouseSpeed = 10;
		boundary = 10;
		smoothSpeed = 4.0f;
		CameraHeightMax = 20;
		CameraHeightMin = 1;

		cameraAngleX = 70;
		cameraAngleY = 30;

		MapSize = new int[2];

		CamRotating = false;

	}

	void Update()
	{
		CameraHeightCurrent = transform.position.y;
	}

	void Start()
	{
		MapSize = Grid2.Instance.GetMapSize ();
		GetComponent<Camera> ().orthographic = false;
		transform.position = new Vector3 (0, 20, 0);
		transform.rotation = Quaternion.Euler (cameraAngleX, cameraAngleY, 0);
	}
	


	public void AttackCamera(bool active, Vector3 attackerPosition= new Vector3(),Vector3 defenderPosition= new Vector3())
	{
		if (active == true) 
		{
			m_SavedPosition = transform.position;
			m_SavedRotation = transform.rotation;
			transform.position = attackerPosition + new Vector3 (0, 1, 0);
			transform.rotation = Quaternion.LookRotation (defenderPosition - attackerPosition);
			transform.RotateAround (transform.position, Vector3.up, 40);
			transform.position = transform.position -1.5f * transform.forward;
			transform.rotation = Quaternion.LookRotation ((defenderPosition + attackerPosition)/2 - transform.position);
		}
		else
		{
			Debug.Log("reset attack cam");
			transform.position = m_SavedPosition;
			transform.rotation = m_SavedRotation;
		}

	}

	public float GetCurrentHeight()
	{
		return CameraHeightCurrent;
	}

	public float GetMaxHeight()
	{
		return CameraHeightMax;
	}


	
}
