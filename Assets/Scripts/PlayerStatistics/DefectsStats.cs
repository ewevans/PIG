using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefectsStats : MonoBehaviour {

		// Use this for initialization
		void Start () {

			GetComponent<Text> ().text = "Total Defects Generated: " + PlayerStats.playerstats.totaldefects;

		}


	}