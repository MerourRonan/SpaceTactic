using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponAnimation : MonoBehaviour {

	protected Weapon m_Weapon;

	/**** Animation Properties ****/
	protected List<GameObject> m_Turrets;
	protected GameObject m_ProjectilePrefab;
	protected float m_RateOfFire;
	protected int m_NumberOfProj;
	protected string m_Mount; // Gimbaled,Fixed,Other

	/**** Unity Function ****/
	public void Awake()
	{

	}

	public void StartAnimations(bool active, GameObject target)
	{
		if(active)
		{
			GameManager.Instance.SetRunning (true);
			//HUD.Instance.DisplayHideAll ("StartAttack");
			Grid2.Instance.ActiveTile (false);
			CameraManager.Instance.AttackCamera (true, m_Weapon.GetSpaceObject().transform.position, target.transform.position);
		}
		else
		{
			CameraManager.Instance.AttackCamera (false);
			Grid2.Instance.ActiveTile(true);
			//HUD.Instance.DisplayHideAll ("EndAttack");
			GameManager.Instance.SetRunning(false);
		}
	}

	public virtual IEnumerator AligningAnimation(GameObject target)
	{
		if(m_Mount == "Fixed")
		{
			yield return StartCoroutine(AligningShipBody(target));
		}
		else if(m_Mount == "Gimbaled")
		{
			List<Turret> firingTurrets = GetFiringTurret(target);
			yield return StartCoroutine(AligningTurrets(target,firingTurrets));
		}
		else
			yield return true;
	}
	public virtual IEnumerator AttackAnimation(GameObject target)
	{
		if(m_Mount == "Fixed")
		{
			yield return StartCoroutine(FixedAttack(target));
		}
		else if(m_Mount == "Gimbaled")
		{
			List<Turret> firingTurrets = GetFiringTurret(target);
			yield return StartCoroutine(GimbaledAttack(target,firingTurrets));
		}
		else
			yield return true;
	}
	protected IEnumerator AligningShipBody(GameObject target)
	{
		Vector3 dirToTarget = (target.transform.position-m_Weapon.GetSpaceObject().transform.position);
		while(m_Weapon.GetSpaceObject().GetBody().transform.rotation != Quaternion.LookRotation(dirToTarget))
		{
			m_Weapon.GetSpaceObject().GetBody().transform.rotation = Quaternion.RotateTowards(m_Weapon.GetSpaceObject().GetBody().transform.rotation,Quaternion.LookRotation(dirToTarget),Time.fixedDeltaTime*500);
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds (0.5f);
	}
	protected virtual IEnumerator FixedAttack(GameObject target)
	{
		for(int shotNumber=0; shotNumber<m_NumberOfProj;shotNumber++)
		{
			foreach(Transform SpawnPoint in transform)
			{
				Vector3 destination = Random.onUnitSphere*0.1f+target.transform.position;
				GameObject projectileInstance = Instantiate(m_ProjectilePrefab, SpawnPoint.position, Quaternion.LookRotation (destination - SpawnPoint.transform.position)) as GameObject;
				Debug.Log ("projectile instance= " + projectileInstance.name);
				Debug.Log("spawning ignore tag = "+ m_Weapon.GetSpaceObject().GetFaction());
				projectileInstance.GetComponentInChildren<Projectile>().SetIgnoreTag(m_Weapon.GetSpaceObject().GetFaction());
			}
			yield return new WaitForSeconds(m_RateOfFire);
		}
		yield return new WaitForSeconds (1f);
	}
	protected List<Turret> GetFiringTurret(GameObject target)
	{
		List<Turret> turretsList = new List<Turret> ();
		GameObject shipBody = m_Weapon.GetSpaceObject ().GetBody ();
		Vector3 dirToTarget = target.transform.position-shipBody.transform.position;
		foreach (GameObject turret in m_Turrets)
		{
			Vector3 dirToTurret = turret.transform.position-shipBody.transform.position;
			if (Mathf.Sign(Vector3.Cross (shipBody.transform.forward, dirToTurret).y) == Mathf.Sign(Vector3.Cross (shipBody.transform.forward, dirToTarget).y))
			{
				turretsList.Add(turret.GetComponent<Turret>());
			}
		}
		Debug.Log ("turrets attacking count = " + turretsList.Count);
		return turretsList;
	}
	protected IEnumerator AligningTurrets(GameObject target, List<Turret> turretsList)
	{
		int turretsNumber = turretsList.Count;
		Coroutine[] turretCoroutinesArray = new Coroutine[turretsNumber];
		int iter = 0;
		foreach(Turret turret in turretsList)
		{
			turretCoroutinesArray[iter] = StartCoroutine(turret.AligningTurret(target));
			iter++;
		}
		foreach(Coroutine turretCoroutine in turretCoroutinesArray)
		{
			yield return turretCoroutine;
		}
		yield return new WaitForSeconds (0.5f);
	}
	protected virtual IEnumerator GimbaledAttack(GameObject target, List<Turret> turretsList)
	{
		int turretsNumber = turretsList.Count;
		Coroutine[] turretCoroutinesArray = new Coroutine[turretsNumber];
		int iter = 0;
		foreach(Turret turret in turretsList)
		{
			turretCoroutinesArray[iter] = StartCoroutine(turret.TurretAttack(target,m_NumberOfProj,m_RateOfFire,m_ProjectilePrefab));
			iter++;
		}
		foreach(Coroutine turretCoroutine in turretCoroutinesArray)
		{
			yield return turretCoroutine;
		}
		yield return new WaitForSeconds (0.5f);
	}


	/*** Set ***/
	public void SetWeapon(Weapon weapon)
	{
		m_Weapon = weapon;
		SetTurrets ();
	}
	public void SetNumberOfProj(int number)
	{
		m_NumberOfProj = number;
	}
	public void SetRateOfFire(float value)
	{
		m_RateOfFire = value;
	}
	public void SetProjectilePrefab(GameObject prefab)
	{
		m_ProjectilePrefab=prefab;
	}
	public void SetMount(string mount)
	{
		m_Mount = mount;
	}
	protected void SetTurrets()
	{
		if(m_Weapon.GetSize()!= "Small")
		{
			m_Turrets = new List<GameObject>();
			foreach(Transform turret in transform)
			{
				m_Turrets.Add (turret.gameObject);
				turret.GetComponent<Turret>().SetWeapon(m_Weapon);
			}
		}
	}

	/*** Get ***/
	public int GetNumberOfProj()
	{
		return m_NumberOfProj;
	}
	public float GetRateOfFire()
	{
		return m_RateOfFire;
	}
	public GameObject GetProjectilePrefab()
	{
		return m_ProjectilePrefab;
	}
	public string GetMount()
	{
		return m_Mount;
	}
}
