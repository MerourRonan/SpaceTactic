using UnityEngine;
using System.Collections;

public class SelectCamController : CameraManager {

	public static SelectCamController Instance;
	
	public void Awake()
	{
		base.Awake ();
		Instance=this;
	}

	public void ActiveSelectCam(GameObject target1, GameObject target2)
	{

	}
}
