using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Marker : MonoBehaviour {

	protected SpaceObject m_SpaceObject;
	private RectTransform MyRectTransform;
	private Renderer m_MyShipRenderer;
	private Image m_MarkerImage;

	private bool m_IsTargeted;


	// Use this for initialization
	void Awake () {
		MyRectTransform = transform.GetComponent<RectTransform> ();
		m_MarkerImage = transform.GetComponent<Image> ();
	}

	void Start () {
		//SetPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_SpaceObject == null)
			Destroy (gameObject);
		SetPosition ();
	}

	public void SetSpaceObject(SpaceObject spaceObject)
	{
		m_SpaceObject = spaceObject;
		m_SpaceObject.SetTargetMarker (this);
		m_MyShipRenderer = m_SpaceObject.gameObject.GetComponentInChildren<MeshRenderer> ();
		ConfigureColor ();

	}

	public void SetPosition()
	{
		if(m_MyShipRenderer != null)
		{
			if(m_MyShipRenderer.isVisible == true)
				m_MarkerImage.enabled = true;
			else
				m_MarkerImage.enabled = false;

			if(m_MarkerImage.enabled == true)
			{
				MyRectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, m_SpaceObject.GetBody().transform.position);
			}
		}
		
	}

	private void ConfigureColor()
	{
		Color markerColor = new Color ();
		string faction = m_SpaceObject.GetFaction ();
		if (faction == "Player")
			markerColor = new Color (0, 255, 0, 1);
		else if (faction == "Enemy")
			markerColor = new Color (255, 0, 0, 1);
		else if (faction == "Neutral")
			markerColor = new Color (255, 255, 0, 1);
		else
			markerColor = new Color (255, 0, 0, 1);

		transform.GetComponent<Image> ().color = markerColor;
	}

	public void IsTargeted(bool active)
	{
		if (active == true)
			transform.GetComponent<Image> ().color = new Color (255, 255, 0, 1);
		else
			ConfigureColor ();
	}
}
