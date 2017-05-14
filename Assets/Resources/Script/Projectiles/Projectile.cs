using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected string m_IgnoreTag;
	protected float speed;
	
	// Use this for initialization
	public void Start () {
		transform.GetComponent<Collider>().isTrigger = true;
		Destroy (this.transform.root.gameObject,10);
	}
	
	void FixedUpdate () {
		transform.GetComponent<Rigidbody> ().velocity = transform.parent.forward * speed;
	}
	
	public void SetIgnoreTag(string tag)
	{
		m_IgnoreTag = tag;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag != m_IgnoreTag)
		{
			Debug.Log ("collider tag = " + collider.transform.tag + " / ignore tag = " + m_IgnoreTag);
			Destroy(transform.parent.gameObject);
		}
	}
}
