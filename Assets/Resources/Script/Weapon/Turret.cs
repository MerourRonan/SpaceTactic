using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
    GameObject m_SpawnPoint;
	bool AttackRun;
	Weapon m_Weapon;

	public void Awake()
	{
		m_SpawnPoint = transform.Find("Cube/Capsule/ProjectileSpawn").gameObject;
		AwakeCheck ();
	}

	public IEnumerator AligningTurret(GameObject target)
	{
		Vector3 dir = target.transform.position - transform.position;
		while(transform.rotation != Quaternion.LookRotation(dir))
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(dir),Time.fixedDeltaTime*200);
			yield return new WaitForEndOfFrame();
		}
		yield return true;
	}
	public IEnumerator TurretAttack(GameObject target, int numberOfProj, float rateOfFire,GameObject projectilePrefab)
	{
        for(int shotNumber=0; shotNumber<numberOfProj;shotNumber++)
        {
			Vector3 destination = Random.onUnitSphere*0.1f+target.transform.position;
			GameObject projectileInstance = Instantiate(projectilePrefab, m_SpawnPoint.transform.position, Quaternion.LookRotation (destination - m_SpawnPoint.transform.position)) as GameObject;
			projectileInstance.GetComponentInChildren<Projectile>().SetIgnoreTag(m_Weapon.GetSpaceObject().GetFaction());
			yield return new WaitForSeconds(rateOfFire);
        }
		yield return true;
	}

	private void AwakeCheck()
	{
		if (m_SpawnPoint == null)
			Debug.LogError("spawnPoint not found");
	}

	public void SetWeapon(Weapon weapon)
	{
		m_Weapon = weapon;
	}

	/*public bool Attacking()
	{
		return AttackRun;
	}*/
}
