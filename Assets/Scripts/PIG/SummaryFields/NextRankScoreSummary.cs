using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextRankScoreSummary : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<Text> ().text = "Next Rank Score: " + GameStats.gamestats.nextrankscore;

	}


}
