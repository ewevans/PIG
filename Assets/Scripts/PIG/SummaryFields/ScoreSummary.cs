using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreSummary : MonoBehaviour {

	public int score;

	// Use this for initialization
	void Start () {
		score = GameStats.gamestats.score;
		GetComponent<Text> ().text = "Score: " + score;

	}


}
