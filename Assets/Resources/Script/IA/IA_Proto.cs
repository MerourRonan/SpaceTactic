using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IA_Proto : MonoBehaviour {

	protected SpaceObject m_SpaceObjectSelected;
	protected string[] m_PriorityOrder;
	protected string m_IABehavior; // Attack, Defence

	void Start()
	{
		m_SpaceObjectSelected = transform.GetComponent<SpaceObject> ();
		IAAction ();
	}

	public void IAAction()
	{

		/*HashSet<Tile> moveRangeTiles = Utilities.ComputeTileInRange(m_SpaceObjectSelected,true);
		List<IA_Action> IAActionList = ComputeIAActions (moveRangeTiles);
		Debug.Log ("IAActionList.count = " + IAActionList.Count);
		if(IAActionList.Count == 0)
		{

		}
		else
		{
			IA_Action bestAction = RecoverBestIAAction (IAActionList);
		}



		string combine="";
		foreach(int val in bestAction.m_Combination)
			combine+=val.ToString()+"/";
		Debug.Log("combination = "+combine);
		Debug.Log("score = "+bestAction.m_CombinationScore);
		Debug.Log("target = "+bestAction.m_WeaponActions[0].m_Target.gameObject.name);
		Debug.Log("weapon 0 = "+bestAction.m_WeaponActions[0].m_Weapon.GetName());*/
	}

	public void ComputePathToBestTarget()
	{
		// Recuperation des cibles de type prioritaires
		List<SpaceObject> targets = new List<SpaceObject> ();
		foreach(string priority in m_PriorityOrder)
		{
			targets = FleetManager.Instance.GetShipList("Player",priority);
			if(targets.Count >0)
				break;
		}

		// recuperation de la cible la plus proche
		float closestDistance=Mathf.Infinity;
		SpaceObject closestTarget=null;
		foreach(SpaceObject target in targets)
		{
			float currentDistance = Vector3.Distance(transform.position,target.GetBody().transform.position);
			if(currentDistance< closestDistance)
			{
				closestDistance = currentDistance;
				closestTarget = target;
			}
		}

		// calcul du tile appartenant a moverange et le plus proche de la cible
	}

	public IA_Action RecoverBestIAAction(List<IA_Action> IAActionList)
	{

		//Debug.Log ("IAActionList.count = " + IAActionList.Count);
		IA_Action bestIA_Action = new IA_Action ();

		foreach(IA_Action currentIA_Action in IAActionList)
		{
			//Debug.Log("currentIA_Action target = " + currentIA_Action.m_SpaceObject.gameObject.name +" / score = " + currentIA_Action.m_CombinationScore);
			if(currentIA_Action.m_CombinationScore > bestIA_Action.m_CombinationScore 
			   ||(currentIA_Action.m_CombinationScore == bestIA_Action.m_CombinationScore && currentIA_Action.m_CombinationComplexity > bestIA_Action.m_CombinationComplexity))
			{
				//Debug.Log("new best action found");
				bestIA_Action = new IA_Action(currentIA_Action);
			}
		}
		return bestIA_Action;
	}

	public List<IA_Action> ComputeIAActions(HashSet<Tile> moveRangeTiles)
	{
		//Debug.Log ("moveRangeTiles.count = " + moveRangeTiles.Count);
		List<IA_Action> IAActionList = new List<IA_Action> ();
		foreach(Tile currentTile in moveRangeTiles)
		{
			IA_Action currentIA_Action = new IA_Action(currentTile,m_SpaceObjectSelected);
			foreach(Weapon currentWeapon in m_SpaceObjectSelected.GetWeaponController().GetEquipments())
			{
				//Debug.Log("current tile = " + currentTile.gameObject.name);
				HashSet<Tile> attackRangeTiles = currentWeapon.ComputeAttackRange(currentTile);
				/*Debug.Log ("attackRangeTiles.count = " + attackRangeTiles.Count);
				if(attackRangeTiles.Contains(GameObject.Find("Tile(2;6)").GetComponent<Tile>()))
					Debug.Log ("OK");*/
				List<SpaceObject> targets = currentWeapon.ComputeTargetInRange(attackRangeTiles);
				//Debug.Log ("targets.count = " + targets.Count);
				WeaponAction currentWeaponAction = ComputeWeaponAction(targets,currentWeapon);
				//Debug.Log("currentWeaponAction target = " + currentWeaponAction.m_Target.gameObject.name + " / current score = " + currentWeaponAction.m_Score);
				currentIA_Action.AddWeaponAction(currentWeaponAction);
			}
			//Debug.Log("intersection = " + currentIA_Action.m_WeaponIntersection);
			if(currentIA_Action.m_WeaponActionNumber >0)
			{
				//Debug.Log("currentWeaponAction target = " + currentIA_Action.m_WeaponActions.m_Target.gameObject.name + " / current score = " + currentWeaponAction.m_Score);
				currentIA_Action.ComputeBestCombination();
				IAActionList.Add(currentIA_Action);
			}
		}
		//Debug.Log("IAActionList.count = " + IAActionList.Count);
		return IAActionList;

	}

	public WeaponAction ComputeWeaponAction(List<SpaceObject> targets, Weapon weapon)
	{
		if (targets == null || targets.Count == 0)
			return null;

		WeaponAction actionWeapon = new WeaponAction (weapon);
		foreach(SpaceObject currentTarget in targets)
		{
			float currentScore = weapon.ComputeScore(currentTarget);

			if(currentScore > actionWeapon.m_Score)
			{
			
				SpaceObject newTarget = currentTarget;
				actionWeapon.m_Target = newTarget;
				actionWeapon.m_Score = currentScore;
				//Debug.Log("current target = " + newTarget.gameObject.name + " / current score = " + currentScore);
			}
		}

		return actionWeapon;
	}

}
