using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LinesCompletedStat : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		GetComponent<Text> ().text = "Total Lines of Code Completed: " + PlayerStats.playerstats.totallinesdone + " of " + PlayerStats.playerstats.totallinesobjective;

		}


	}