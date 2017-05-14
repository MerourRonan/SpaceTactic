using UnityEngine;
using System.Collections;

public class MoveManager : MonoBehaviour {

	public static MoveManager Instance;

	void Awake()
	{
		Instance = this;
	}


	public IEnumerator StartMove(Tile destinationTile, SpaceObject shipSelected)
	{
		if(GridPathFinder.Instance.GetMoveRangeTiles().Contains(destinationTile)==true)
		{
			shipSelected.GetHUDController().DisplayHUD(false);
			shipSelected.SetHasMoved(true);
			Grid2.Instance.ReleaseTile(shipSelected.GetReferenceTile(),shipSelected);
			Grid2.Instance.LockTile(destinationTile,shipSelected);
			yield return StartCoroutine(shipSelected.GetMoveController().ShipMovement(GridPathFinder.Instance.GetPathFindingTiles()));
			ActionManager.Instance.SelectTile(destinationTile);
		}
	}

}
