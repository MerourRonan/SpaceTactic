using UnityEngine;
using System.Collections;

public class MoveCamController : CameraManager {

	public static MoveCamController Instance;

	public void Awake()
	{
		base.Awake ();
		Instance=this;
	}


	public IEnumerator PlayerControlCamera()
	{
		while(true)
		{
			if(GameManager.Instance.GetRunning() == false)
			{
				MoveCam();
				ZoomCam ();
				CameraClampPosition ();
				CameraRotate();
			}
			yield return null;
		}
	}
	
	void MoveCam()
	{
		Vector3 moveDir = new Vector3(0,0,0);
		float speedFactor = transform.position.y/CameraHeightMax;
		bool newPos = false;
		if (Input.mousePosition.x > Screen.width - boundary)
		{
			moveDir+=transform.right;
			newPos = true;
		}
		
		if (Input.mousePosition.x < 0 + boundary)
		{
			moveDir+=-transform.right;
			newPos = true;
		}
		
		if (Input.mousePosition.y > Screen.height - boundary)
		{
			moveDir+=transform.up;
			newPos = true;
		}
		
		if (Input.mousePosition.y < 0 + boundary)
		{
			moveDir+=-transform.up;
			newPos = true;
		}
		
		if (newPos == true) 
		{
			moveDir.y = 0;
			transform.position += moveDir * speedFactor;
			UpdateMarkersPosition();
		}
		
		
		
	}
	
	void ZoomCam()
	{
		
		if (transform.position.y > CameraHeightMax)
		{
			transform.position = new Vector3 (transform.position.x, CameraHeightMax, transform.position.z);
		}
		if (transform.position.y < CameraHeightMin) 
		{
			transform.position = new Vector3 (transform.position.x, CameraHeightMin, transform.position.z);
		}
		if((transform.position.y == CameraHeightMax && Input.GetAxis("Mouse ScrollWheel")>0)
		   || (transform.position.y == CameraHeightMin && Input.GetAxis("Mouse ScrollWheel")<0)
		   || (transform.position.y > CameraHeightMin && transform.position.y < CameraHeightMax))
		{
			transform.position +=(transform.forward*Input.GetAxis("Mouse ScrollWheel")*5);
			UpdateMarkersPosition();
		}
	}
	
	void CameraClampPosition()
	{
		float X = Mathf.Clamp (transform.position.x, 0, MapSize [0]);
		float Z = Mathf.Clamp (transform.position.z, 0, MapSize [1]);
		if(X != transform.position.x || Z != transform.position.z)
		{
			transform.position = new Vector3 (X, transform.position.y,Z);
			UpdateMarkersPosition();
		}
	}
	
	void CameraRotate()
	{
		Vector3 camDir = transform.forward;
		Vector3 camPos = transform.position;
		Vector3 rotationCenter = new Vector3 (-camDir.x / camDir.y * camPos.y + camPos.x, 0, -camDir.z / camDir.y * camPos.y + camPos.z);
		if (Input.GetKeyDown (KeyCode.A) && CamRotating == false) 
		{
			StartCoroutine(Rotating(rotationCenter,Vector3.up,1));
			
		}
		else if (Input.GetKeyDown (KeyCode.Z) && CamRotating == false) 
		{
			StartCoroutine(Rotating(rotationCenter,Vector3.up,-1));
		}
	}
	
	IEnumerator Rotating(Vector3 center, Vector3 axis, int sens)
	{
		CamRotating = true;
		float angle = 0;
		while(angle < 90)
		{
			
			transform.RotateAround(center,axis,sens*90f*Time.deltaTime);
			angle+=90f*Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log("angle = " + angle);
		CamRotating = false;
		UpdateMarkersPosition();
		yield return true;
	}

	public void UpdateMarkersPosition()
	{
		foreach (Transform marker in m_HUDMarkers)
			marker.GetComponent<Marker> ().SetPosition ();
	}
}
