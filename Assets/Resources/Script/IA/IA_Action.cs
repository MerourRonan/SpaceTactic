using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IA_Action {

	public Tile m_Tile;
	public SpaceObject m_SpaceObjectSelected;
	public int m_WeaponActionNumber;
	public List<WeaponAction> m_WeaponActions;
	public int[] m_Combination;
	public int m_CombinationComplexity;
	public float m_CombinationScore;

	/**** Constructor ****/
	public IA_Action(Tile tile=null,SpaceObject spaceObjectAttacking=null)
	{
		m_Tile = tile;
		m_SpaceObjectSelected = spaceObjectAttacking;
		m_WeaponActionNumber=0;
		m_WeaponActions = new List<WeaponAction>();
		m_CombinationComplexity = 0;
		m_CombinationScore = 0;
	}
	
	public IA_Action (IA_Action actionToCopy)
	{
		m_WeaponActions = new List<WeaponAction>();
		m_Combination = new int[actionToCopy.m_CombinationComplexity];
		
		m_Tile = actionToCopy.m_Tile;
		m_SpaceObjectSelected = actionToCopy.m_SpaceObjectSelected;
		m_WeaponActionNumber=actionToCopy.m_WeaponActionNumber;
		m_WeaponActions = actionToCopy.m_WeaponActions;
		m_Combination = actionToCopy.m_Combination;
		m_CombinationComplexity = actionToCopy.m_CombinationComplexity;
		m_CombinationScore = actionToCopy.m_CombinationScore;
	}
	
	public void AddWeaponAction(WeaponAction actionWeapon)
	{
		if(actionWeapon != null)
		{
			//Debug.Log("actionWeapon action not null");
			m_WeaponActionNumber++;
			m_WeaponActions.Add (actionWeapon);
		}
	}
	public void ComputeBestCombination()
	{
		List<int[]> possibleCombinations = ComputeAllPossibleCombination ();
		RecoverBestCombination (possibleCombinations);
	}

	public List<int[]> ComputeAllPossibleCombination()
	{
		List<int[]> storedCombinations = new List<int[]> ();
		int[] arr = new int[m_WeaponActionNumber];
		for (int iter=0; iter< m_WeaponActionNumber; iter++)
			arr [iter] = iter;
		
		for(int iter = 0 ; iter<m_WeaponActionNumber;iter++)
			Utilities.Combinations(arr,iter+1,0,new int[iter+1],storedCombinations);

		return storedCombinations;
	}

	public void RecoverBestCombination(List<int[]> possibleCombinations)
	{
		foreach(int[] currentCombination in possibleCombinations)
		{
			//Debug.Log ("currentCombination.length = "+currentCombination.Length);
			int energyLimit = m_SpaceObjectSelected.GetEnergyPoints("Current");
			int energyUse =0;
			bool validCombination = true;
			float currentCombinationScore = 0;
			
			string debugCombination="";
			
			foreach(int weaponNumber in currentCombination)
			{
				debugCombination+=weaponNumber.ToString()+"/";
				//Debug.Log("m_WeaponActions[weaponNumber].m_Target = "+m_WeaponActions[weaponNumber].m_Target);
				energyUse+=m_WeaponActions[weaponNumber].m_Weapon.GetEnergyDrain();
				currentCombinationScore+= m_WeaponActions[weaponNumber].m_Score;
				//Debug.Log("m_WeaponActions[weaponNumber].m_Score = "+m_WeaponActions[weaponNumber].m_Score);
				if(energyUse > energyLimit)
				{
					validCombination = false;
					break;
				}
			}
			
			currentCombinationScore= Mathf.Ceil(currentCombinationScore/currentCombination.Length);
			Debug.Log ("validCombination = " + validCombination + " / combination = " + debugCombination + " / combination score = " + currentCombinationScore);
			if(validCombination == true 
			   && (currentCombinationScore > m_CombinationScore || (currentCombinationScore == m_CombinationScore && currentCombination.Length > m_CombinationComplexity)))
			{
				m_CombinationScore = currentCombinationScore;
				m_CombinationComplexity = currentCombination.Length;
				m_Combination = new int[m_CombinationComplexity];
				currentCombination.CopyTo(m_Combination,0);
			}
		}
	}
}
