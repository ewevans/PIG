using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Summary_LOC : MonoBehaviour {
	
	private GameObject gameSystem;
	private Sprint sprint;

	public int LinesDone;
	public int LinesObjective;

	// Use this for initialization
	void Start () {
		gameSystem = GameObject.Find ("GameSystem");
		//sprint = gameSystem.GetComponent<Sprint> ();
		LinesDone = PlayerPrefs.GetInt ("Lines Done");
		LinesObjective = PlayerPrefs.GetInt ("Lines Objective");
		if (LinesDone >= LinesObjective) {
			GetComponent<Text> ().color = Color.green;
			GetComponent<Text> ().text = " " + LinesDone + " of " + LinesObjective;
		} else {
			GetComponent<Text> ().color = Color.red;
			GetComponent<Text> ().text = " " + LinesDone + " of " + LinesObjective;
		}
			
	}

	// Update is called once per frame
	void Update () {

	}
}