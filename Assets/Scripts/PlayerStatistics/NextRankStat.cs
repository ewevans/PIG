using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextRankStat : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<Text> ().text = "Next Rank: " + PlayerStats.playerstats.nextrank;

	}


}
