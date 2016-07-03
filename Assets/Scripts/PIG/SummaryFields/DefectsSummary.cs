using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DefectsSummary : MonoBehaviour {

	private GameObject gameSystem;
	private Sprint sprint;

	public int defectsnumber;
	public int defectsmax;

	// Use this for initialization
	void Start ()
	{
		gameSystem = GameObject.Find ("GameSystem");
		//sprint = gameSystem.GetComponent<Sprint> ();
		defectsnumber = PlayerPrefs.GetInt ("Defects Generated");
		defectsmax = PlayerPrefs.GetInt ("Defects Max");
		if (defectsnumber >= defectsmax) {
			GetComponent<Text> ().color = Color.red;
			GetComponent<Text> ().text = " " + defectsnumber + " with a maximum allowable of " + defectsmax;
		} else if (defectsnumber < defectsmax) {
			GetComponent<Text> ().color = Color.green;
			GetComponent<Text> ().text = " " + defectsnumber + " with a maximum allowable of " + defectsmax;
		}
	}
}

