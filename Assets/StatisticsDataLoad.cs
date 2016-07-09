using UnityEngine;
using System.Collections;

public class StatisticsDataLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RankManager rm = new RankManager ();
		GameStats.gamestats.Load ();
		rm.initiateRanks ();
		GameStats.gamestats.rank = rm.currentrank;
		GameStats.gamestats.nextrank = rm.currentnextrank;
		GameStats.gamestats.nextrankscore = rm.currentnextrankscore;
	
	}

}
