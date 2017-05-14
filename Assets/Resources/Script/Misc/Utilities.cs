using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utilities  {

	public static HashSet<Tile> ComputeTileInRange(Tile startTile, int rangeMax, bool blockingTile)
	{
		if(rangeMax >=0)
		{
			HashSet<Tile> TileInRange = new HashSet<Tile> ();
			HashSet<Tile> OpenTiles = new HashSet<Tile> ();
			OpenTiles.Add (startTile);
			for(int range=0;range<=rangeMax;range++)
			{
				HashSet<Tile> OpenTilesTemp = new HashSet<Tile> (OpenTiles);
				foreach(Tile Tile in OpenTilesTemp)
				{
					OpenTiles.Remove(Tile);
					TileInRange.Add(Tile);
					//foreach(GameObject TileNeighboor in Tile.GetComponent<Tile>().GetNeighboors(startTile.GetComponent<Tile>().GetClearance()))
					foreach(Tile TileNeighboor in Tile.GetNeighboors())
					{
						if(TileNeighboor.GetFree() == true || blockingTile == false)
						{
							OpenTiles.Add(TileNeighboor);
						}
					}
				}
			}
			TileInRange.Remove(startTile);
			return TileInRange;
		}
		else
		{
			Debug.LogError("invalid argument");
			return null;
		}
	}
	
	public static HashSet<Tile> ComputeTileInRange(SpaceObject ship, bool blockingTile)
	{
		Tile startTile = ship.GetReferenceTile ();
		List<Tile> shipTiles = ship.GetTiles ();

		if (startTile.GetComponent<Tile> () == null || ship.GetMoveRange () < 0)
		{
			Debug.LogError("invalid argument");
			return null;
		}

		HashSet<Tile> TilesInRange = new HashSet<Tile> ();
		HashSet<Tile> TilesInRangeTemp = new HashSet<Tile> ();
		HashSet<Tile> OpenTiles = new HashSet<Tile> ();

		/*foreach (GameObject shipTile in shipTiles)
			OpenTiles.Add (shipTile);*/
		OpenTiles.Add (startTile);
			
		for(int range=0;range<=ship.GetMoveRange ();range++)
		{
			HashSet<Tile> OpenTilesTemp = new HashSet<Tile> (OpenTiles);
			foreach(Tile currentOpenTile in OpenTilesTemp)
			{
				List<Tile> currentOpenTileNeighboors = currentOpenTile.GetNeighboors(ship.GetClearance());
				OpenTiles.Remove(currentOpenTile);
				TilesInRange.Add(currentOpenTile);
				//Debug.Log("TilesInRange.count = " +TilesInRange.Count);
				//Debug.Log("currentOpenTileNeighboors.count = " +currentOpenTileNeighboors.Count);
				foreach(Tile TileNeighboor in currentOpenTileNeighboors)
				{
					if((TileNeighboor.GetFree() == true || TileNeighboor.GetSpaceObjectOnTile() == ship || blockingTile == false) && OpenTiles.Contains(TileNeighboor)==false)
						OpenTiles.Add(TileNeighboor);
				}
			}
		}
		TilesInRangeTemp = new HashSet<Tile> (TilesInRange);
		foreach(Tile rangeTile in TilesInRangeTemp)
		{
			if(ship.GetClearance() > 1)
			{
				foreach(Tile boxTile in Utilities.RecoverBox(rangeTile,ship.GetClearance()))
					if(TilesInRange.Contains(boxTile) == false &&( boxTile.GetFree() == true || blockingTile == false))
						TilesInRange.Add(boxTile);
			}
			else
			{
				TilesInRange.Add(rangeTile);
			}
		}
		return TilesInRange;
	}

	public static HashSet<Tile> ComputeTileInInterval(Tile startTile,int rangeMin, int rangeMax)
	{
		HashSet<Tile> TileInRangeMin = ComputeTileInRange(startTile,rangeMin,false);
		HashSet<Tile> TileInRangeMax = ComputeTileInRange(startTile,rangeMax,false);
		HashSet<Tile> TileInInterval = new HashSet<Tile> ();

		foreach(Tile Tile in TileInRangeMax)
		{
			if(TileInRangeMin.Contains(Tile) == false || rangeMin == rangeMax )
			{
				TileInInterval.Add(Tile);
			}
		}
		TileInInterval.Remove (startTile);
		return TileInInterval;
	}

	public static HashSet<Tile> ComputeTileInInterval(List<Tile> startTiles,int rangeMin, int rangeMax)
	{
		HashSet<Tile> TileInRangeList = new HashSet<Tile> ();
		HashSet<Tile> OpenTilesList = new HashSet<Tile> (startTiles);
		HashSet<Tile> CloseTilesList = new HashSet<Tile> (startTiles);
		int currentRange = 1;

		while(currentRange <=rangeMax)
		{
			HashSet<Tile> OpenTilesTempList = new HashSet<Tile> (OpenTilesList);
			foreach (Tile opentile in OpenTilesTempList) 
			{
				OpenTilesList.Remove(opentile);
				CloseTilesList.Add(opentile);
				List<Tile> neighboorTilesList = opentile.GetNeighboors();
				foreach (Tile neighboorTile in neighboorTilesList) 
				{
					if(CloseTilesList.Contains(neighboorTile)==false)
					{
						OpenTilesList.Add(neighboorTile);
						if(currentRange<=rangeMax)
							TileInRangeList.Add(neighboorTile);
					}
				}
			}
			currentRange++;
		}
		Debug.Log ("tile in range count = " + TileInRangeList.Count);
		return TileInRangeList;
	}

	public static HashSet<Tile> ComputeTileInDistance(List<Tile> startTile, Vector3 startPosition,int rangeMin, int rangeMax, bool noObstacle=true)
	{
		HashSet<Tile> TileInRangeList = new HashSet<Tile> ();
		HashSet<Tile> OpenTilesList = new HashSet<Tile> (startTile);
		HashSet<Tile> CloseTilesList = new HashSet<Tile> (startTile);
		int currentRange = 1;
		
		while(OpenTilesList.Count != 0)
		{
			HashSet<Tile> OpenTilesTempList = new HashSet<Tile> (OpenTilesList);
			foreach (Tile opentile in OpenTilesTempList) 
			{
				OpenTilesList.Remove(opentile);
				CloseTilesList.Add(opentile);
				List<Tile> neighboorTilesList = opentile.GetNeighboors();
				foreach (Tile neighboorTile in neighboorTilesList) 
				{
					if(CloseTilesList.Contains(neighboorTile)==false 
					   && Vector3.Distance(startPosition,neighboorTile.transform.position)<=rangeMax)
					{
						OpenTilesList.Add(neighboorTile);

						/*RaycastHit hit;
						Vector3 dir = neighboorTile.transform.position-startPosition;
						Physics.Raycast(startPosition, dir,out hit, dir.magnitude,(1<<LayerMask.NameToLayer("SpaceObject")));*/
						/*if(Vector3.Distance(startPosition,neighboorTile.transform.position)>=rangeMin
						   && Vector3.Distance(startPosition,neighboorTile.transform.position)<=rangeMax
						   && hit.collider == null)*/
						if(Vector3.Distance(startPosition,neighboorTile.transform.position)>=rangeMin
						   && Vector3.Distance(startPosition,neighboorTile.transform.position)<=rangeMax)
							TileInRangeList.Add(neighboorTile);
					}
				}
			}
		}
		return TileInRangeList;
	}

	public static List<Tile> ComputeBorderTile(HashSet<Tile> tiles)
	{
		List<Tile> borderTiles = new List<Tile> ();
		foreach(Tile tile in tiles)
		{
			int neighboorCount =0;
			foreach(Tile neighboor in tile.GetNeighboors())
			{
				if(tiles.Contains(neighboor))
					neighboorCount++;
			}
			if(neighboorCount<4)
				borderTiles.Add(tile);
		}
		return borderTiles;
	}


	public static int ConvertDifficultyToInt(string difficulty)
	{
		if (difficulty == "Easy") {
			return 0;
		} else if (difficulty == "Normal") {
			return 1;
		} else if (difficulty == "Hard") {
			return 2;
		} else if (difficulty == "Extreme") {
			return 3;
		} else {
			Debug.LogError("invalid argument difficulty");
			return -1;
		}
	}

	public static int ConvertThreatZoneToInt(string zone)
	{
		if (zone == "Low") {
			return 0;
		} else if (zone == "Medium") {
			return 1;
		} else if (zone == "High") {
			return 2;
		} else {
			Debug.LogError("invalid argument difficulty");
			return -1;
		}
	}

	public static string ConvertThreatZoneToString(int zone)
	{
		if (zone == 0) {
			return "Low";
		} else if (zone == 1) {
			return "Medium";
		} else if (zone == 2) {
			return "High";
		} else {
			Debug.LogError("invalid argument difficulty");
			return "";
		}
	}

	public static Weapon[] FactoryEquipments (string[] equipmentName)
	{
		
		if (equipmentName != null) 
		{
			Weapon[] equipments = new Weapon[equipmentName.Length];
			for(int number = 0; number < equipmentName.Length; number++)
			{
				if(equipmentName[number] == "SmallLaserTest")
				{
					equipments[number] = new SmallLaserTest();
				}
				else if(equipmentName[number] == "LargeLaserTest")
				{
					equipments[number] = new SmallLaserTest();
				}
				else if(equipmentName[number] == "Overcharge")
				{
					Debug.Log("mount overcharge on ship");
					equipments[number] = new Overcharge();
				}
				else if(equipmentName[number] != null)
				{
					Debug.LogError("invalid weapon name");
				}
			}
			return equipments;
		}
		else 
			return null;
	}

	public static Passives[] FactoryPassives (string[] passiveName)
	{
		if (passiveName != null) 
		{
			Passives[] passives = new Passives[passiveName.Length];
			for (int number = 0; number < passiveName.Length; number++) {
				if (passiveName [number] == "ReactiveArmor") {
					passives [number] = new ReactiveArmor ();
				}
				else if(passiveName[number] != null)
				{
					Debug.LogError("invalid passive name");
				}
			}
			return passives;
		} else 
			return null;
	}

	public static List<Tile> RecoverBox(Tile tileUpLeft, int boxDimension)
	{
		List<Tile> boxTiles = new List<Tile> ();
		if (tileUpLeft == null || boxDimension == 0)
		{
			Debug.Log("tileUpLeft == null || boxDimension == 0");
			return boxTiles;
		}

		int refRow = tileUpLeft.GetGridRow ();
		int refColumn = tileUpLeft.GetGridColumn ();


		if (refRow + boxDimension >= Grid2.Instance.GetMapSize () [0] || refColumn + boxDimension >= Grid2.Instance.GetMapSize () [1])
		{
			Debug.Log("refRow - boxDimension + 1 < 0 || refColumn + boxDimension - 1 >= Grid.Instance.GetMapSize () [1]");
			return boxTiles;
		}

		for(int iterRow = refRow;iterRow<refRow + boxDimension;iterRow++)
		{
			for(int iterColumn = refColumn;iterColumn<refColumn + boxDimension;iterColumn++)
			{
				if(Grid2.Instance.GetTilesArray()[iterRow,iterColumn]==null)
				{
					Debug.Log("tile("+iterRow+";"+iterColumn+") is null");
				}

				boxTiles.Add(Grid2.Instance.GetTilesArray()[iterRow,iterColumn]);
			}
		}
		return boxTiles;
	}

	public static List<Tile> RecoverClearanceBox(Tile tileUpLeft, int boxDimension)
	{
	
		List<Tile> boxTiles = new List<Tile> ();
		if (tileUpLeft == null || boxDimension == 0)
		{
			return null;
		}
		int refRow = tileUpLeft.GetGridRow ();
		int refColumn = tileUpLeft.GetGridColumn ();
		/*if(refRow == 0 && refColumn == 0)
		{
			Debug.Log ("box dimension = " + boxDimension);
			Debug.Log("refRow + boxDimension - 1 = " +(refRow + boxDimension - 1));
			Debug.Log("refColumn + boxDimension - 1 = " +(refColumn + boxDimension - 1));
		}*/
		
		if (refRow + boxDimension - 1 >= Grid2.Instance.GetMapSize () [0] || refColumn + boxDimension - 1 >= Grid2.Instance.GetMapSize () [1])
		{
			return null;
		}
		
		for(int iterRow = refRow;iterRow<refRow + boxDimension;iterRow++)
		{
			for(int iterColumn = refColumn;iterColumn<refColumn + boxDimension;iterColumn++)
			{
				/*if(refRow == 0 && refColumn == 0)
				{
					Debug.Log("tile up left  = " +tileUpLeft.name);
					Debug.Log("tile in box  = " +Grid2.Instance.GetTilesArray()[iterRow,iterColumn].name);
				}*/
				boxTiles.Add(Grid2.Instance.GetTilesArray()[iterRow,iterColumn]);
			}
		}
		return boxTiles;
	}

	public static List<Tile> RecoverClearanceNeighboors(Tile tileUpLeft, int boxDimension)
	{
		List<Tile> clearanceNeighboors = new List<Tile> ();
		int refRow = tileUpLeft.GetGridRow ();
		int refColumn = tileUpLeft.GetGridColumn ();

		for(int iterRow = refRow-2 ; iterRow <= refRow ; iterRow++)
		{
			for(int iterColumn = refColumn -2 ;iterColumn<=refColumn ; iterColumn++)
			{
				if (iterColumn >=0 && iterColumn < Grid2.Instance.GetMapSize () [1] && iterRow >= 0 && iterRow < Grid2.Instance.GetMapSize () [0] )
				{
					clearanceNeighboors.Add(Grid2.Instance.GetTilesArray()[iterRow,iterColumn]);
				}
			}
		}
		return clearanceNeighboors;
	}

	public static void Combinations(int[] arr, int len, int startPosition, int[] result, List<int[]> storedResult=null){
		if (len == 0)
		{
			int[] resultostore = new int[result.Length];
			result.CopyTo(resultostore,0);
			storedResult.Add(resultostore);
			//Debug.Log("["+result[0]+","+result[1]+","+result[2]+"]");
			return;
		}       
		for (int i = startPosition; i <= arr.Length-len; i++)
		{
			result[result.Length - len] = arr[i];
			Combinations(arr, len-1, i+1, result,storedResult);
		}
	} 

	public static void combinations2(string[] arr, int len, int startPosition, string[] result){
		if (len == 0){
			Debug.Log("["+result[0]+","+result[1]+","+result[2]+"]");
			return;
		}       
		for (int i = startPosition; i <= arr.Length-len; i++){
			result[result.Length - len] = arr[i];
			combinations2(arr, len-1, i+1, result);
		}
	}

}
