using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamagePanel : MonoBehaviour {

	protected RectTransform MyRectTransform;
	protected Vector3 m_TargetPosition;

	// Use this for initialization
	void Awake () {
		MyRectTransform = transform.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition ();
	}

	public IEnumerator ActiveDamagePanel(SpaceObject target, float damage)
	{
		m_TargetPosition = target.transform.position;
		SetPosition ();
		transform.GetComponentInChildren<Text> ().text = damage.ToString ();
		yield return new WaitForSeconds(2.5f);
		gameObject.SetActive (false);
	}

	public void SetPosition()
	{
	//	if(m_Target != null)
		{
			Vector3 panelPosition =  RectTransformUtility.WorldToScreenPoint(Camera.main,m_TargetPosition );
			transform.position = panelPosition+ new Vector3(0,1,0)*MyRectTransform.sizeDelta.y;
		}
	}
}
