﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BudgetSummary : MonoBehaviour {

	public int budgetnumber;

	// Use this for initialization
	void Start () {
		// budgetnumber = PlayerPrefs.GetInt ("Budget");
		budgetnumber = GameStats.gamestats.budget;

		if (budgetnumber < 0) {
			GetComponent<Text> ().color = Color.red;
			GetComponent<Text> ().text = "$" + budgetnumber + " you went over budget!!";
		} else {
			GetComponent<Text> ().color = Color.green;
			GetComponent<Text> ().text = "$" + budgetnumber + " you stayed within budget!! Good Job";
		}
	}


}
