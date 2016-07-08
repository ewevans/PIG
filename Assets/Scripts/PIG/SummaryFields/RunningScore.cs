using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RunningScore : MonoBehaviour {

	public int cumulativescore;

	// Use this for initialization
	void Start () {
		
		cumulativescore = GameStats.gamestats.cumulativescore;
		GetComponent<Text> ().text = "Cumulative Score: " + cumulativescore;

	}


}
