using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDMarkerManager : MonoBehaviour {

	public static HUDMarkerManager Instance;

	void Awake()
	{
		Instance = this;
	}

	public void DisplayTargetMarkers(List<GameObject> targets)
	{

	}

	public void SpawnTargetMarker(SpaceObject ship)
	{
		string spaceObjectFaction = ship.GetFaction();
		GameObject markerPrefab = Resources.Load<GameObject>("Prefab/HUD/TargetMarker");
		GameObject markerInstance = Instantiate(markerPrefab,ship.GetBody().transform.position,Quaternion.identity) as GameObject;
		markerInstance.GetComponent<Marker>().SetSpaceObject(ship);
		markerInstance.transform.SetParent(GameObject.Find("HUD/Markers/TargetMarkers").transform);
	}

	public void DestroyTargetMarkers()
	{
		Transform targetMarkersParent = transform.Find ("Markers/TargetMarkers");
		foreach (Transform marker in targetMarkersParent)
			if(marker != null)
				Destroy (marker.gameObject);
	}
}
