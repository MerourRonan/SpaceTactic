using UnityEngine;
using System.Collections;

public class ShieldAnimation : WeaponAnimation {

	public override IEnumerator AttackAnimation(GameObject target)
	{
		yield return StartCoroutine(SpawnShield(target));
	}

	protected IEnumerator SpawnShield(GameObject target)
	{
		Debug.Log ("shield target name = " + target.transform.parent.name);
		GameObject shieldInstance = Instantiate(m_ProjectilePrefab, target.transform.position, Quaternion.identity) as GameObject;
		shieldInstance.transform.SetParent (transform.parent.parent);
		target.transform.parent.GetComponent<SpaceObject> ().SetShield (shieldInstance);
		yield return new WaitForSeconds (1f);
	}
}
