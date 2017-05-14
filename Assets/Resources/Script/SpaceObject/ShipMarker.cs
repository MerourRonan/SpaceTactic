using UnityEngine;
using System.Collections;

public class ShipMarker : MonoBehaviour {

	CameraManager CameraScript;
	Color MarkerColor;

	// Use this for initialization
	void Start () {
		CameraScript = Camera.main.GetComponent<CameraManager> ();
		ConfigureColor ();
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.LookRotation (-Camera.main.gameObject.transform.forward);
		transform.localScale = 0.4f * Vector3.one * (CameraScript.GetCurrentHeight() / CameraScript.GetMaxHeight());
		Fade ();
	
	}

	private void ConfigureColor()
	{
		string faction = transform.GetComponentInParent<Ship> ().GetFaction ();
		if (faction == "Player")
			MarkerColor = new Color (0, 255, 0, 1);
		if (faction == "Enemy")
			MarkerColor = new Color (255, 0, 0, 1);
		if (faction == "Neutral")
			MarkerColor = new Color (255, 255, 0, 1);
	}

	private void Fade()
	{
		float fadeFactor = (2*CameraScript.GetCurrentHeight()-CameraScript.GetMaxHeight())/CameraScript.GetMaxHeight();
		MarkerColor.a = fadeFactor;
		transform.GetComponent<SpriteRenderer> ().color = MarkerColor;
	}
}
