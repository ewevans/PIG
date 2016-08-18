using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DefectsSummary : MonoBehaviour {

	public int defectsnumber;
	public int defectsmax;

	// Use this for initialization
	void Start ()
	{
		//defectsnumber = PlayerPrefs.GetInt ("Defects Generated");
		//defectsmax = PlayerPrefs.GetInt ("Defects Max");
		defectsnumber = GameStats.gamestats.defects;
		defectsmax = GameStats.gamestats.defectsmax;


		if (defectsnumber >= defectsmax) {
			GetComponent<Text> ().color = Color.red;
			GetComponent<Text> ().text = " " + defectsnumber + " with a limit of " + defectsmax + " (-150)";
		} else if (defectsnumber < defectsmax) {
			GetComponent<Text> ().color = Color.green;
			GetComponent<Text> ().text = " " + defectsnumber + " with a limit of " + defectsmax + " (+150)";
		}
	}
}

