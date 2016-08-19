using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextRankSummary : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
		GetComponent<Text> ().text = "Next Rank: " + GameStats.gamestats.nextrank;

	}


}