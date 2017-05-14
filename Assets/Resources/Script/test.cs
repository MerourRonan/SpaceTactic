using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {

		List<int[]> storedResults = new List<int[]> ();
		int[] arr = new int[]{1,2,3,4};

		for(int iter = 1 ; iter<=4;iter++)
		Utilities.Combinations(arr,iter,0,new int[iter],storedResults);

		Debug.Log ("data in list");
		foreach(int[] result in storedResults)
		{
			string combine="";
			foreach(int val in result)
				combine+=val.ToString();
			//Debug.Log("["+result[0]+","+result[1]+","+result[2]+","+result[3]+"]");
			Debug.Log(combine);
		}

		/*string[] arr = {"A","B","C","D","E","F"};
		Utilities.combinations2(arr, 3, 0, new string[3]);*/
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
