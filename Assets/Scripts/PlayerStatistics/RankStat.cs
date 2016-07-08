using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankStat : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<Text> ().text = "Player Rank: " + PlayerStats.playerstats.rank;

	}


}