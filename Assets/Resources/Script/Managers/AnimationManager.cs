using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	public static AnimationManager Instance;

	void Awake()
	{
		Instance = this;
	}

	public IEnumerator LaunchAnimations(Weapon weaponAttacking, GameObject target)
	{
		weaponAttacking.GetWeaponAnimation ().StartAnimations (true, target);
		yield return StartCoroutine (weaponAttacking.GetWeaponAnimation ().AligningAnimation (target));
		Coroutine attackCoroutine = StartCoroutine (weaponAttacking.GetWeaponAnimation ().AttackAnimation (target));
		yield return new WaitForSeconds (0.5f);
		AttackManager.Instance.ApplyEffect (weaponAttacking);
		yield return attackCoroutine;
		weaponAttacking.GetWeaponAnimation ().StartAnimations (false, target);
		ActionManager.Instance.ResetOnButtonSelection ();
	}
}
