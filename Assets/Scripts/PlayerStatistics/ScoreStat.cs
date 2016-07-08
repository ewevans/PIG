using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreStat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		GetComponent<Text> ().text = "Cumulative Score: " + PlayerStats.playerstats.totalscore;

	}


}
