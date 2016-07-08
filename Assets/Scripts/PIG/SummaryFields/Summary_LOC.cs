using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Summary_LOC : MonoBehaviour {
	
	public int LinesDone;
	public int LinesObjective;

	// Use this for initialization
	void Start () {
		//LinesDone = PlayerPrefs.GetInt ("Lines Done");
		//LinesObjective = PlayerPrefs.GetInt ("Lines Objective");
		LinesDone = GameStats.gamestats.linesdone;
		LinesObjective = GameStats.gamestats.linesobjective;

		if (LinesDone >= LinesObjective) {
			GetComponent<Text> ().color = Color.green;
			GetComponent<Text> ().text = " " + LinesDone + " of " + LinesObjective;
		} else {
			GetComponent<Text> ().color = Color.red;
			GetComponent<Text> ().text = " " + LinesDone + " of " + LinesObjective;
		}
			
	}


}