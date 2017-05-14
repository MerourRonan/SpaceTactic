using UnityEngine;
using System.Collections;

public class FactionTest : Faction {

	public FactionTest() : base()
	{
		InitEasySquadrons ();
	}

	protected void InitEasySquadrons()
	{
		Squadron squadron1 = new Squadron ("LargeEnemyShipTest", null, null, null);
		Squadron squadron2 = new Squadron ("LargeEnemyShipTest", "SmallEnemyShipTest", null, null);
		Squadron squadron3 = new Squadron ("LargeEnemyShipTest", "SmallEnemyShipTest", "SmallEnemyShipTest", null);
		Squadron squadron4 = new Squadron ("LargeEnemyShipTest", "SmallEnemyShipTest", "SmallEnemyShipTest", "SmallEnemyShipTest");
		Squadron squadron5 = new Squadron (null, "SmallEnemyShipTest", "SmallEnemyShipTest", "SmallEnemyShipTest");
		Squadron squadron6 = new Squadron (null, "SmallEnemyShipTest", "SmallEnemyShipTest", null);

		m_EasySquadrons.Add (squadron1);
		m_EasySquadrons.Add (squadron2);
		m_EasySquadrons.Add (squadron3);
		m_EasySquadrons.Add (squadron4);
		m_EasySquadrons.Add (squadron5);
		m_EasySquadrons.Add (squadron6);
	}
}
