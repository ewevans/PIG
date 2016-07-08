using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextRankScoreStat : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<Text> ().text = "Next Rank Score: " + PlayerStats.playerstats.nextrankscore;

	}


}