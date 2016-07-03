using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lines : MonoBehaviour {

	private GameObject gameSystem;
	private Sprint sprint;
	// Use this for initialization
	void Start () {
		gameSystem = GameObject.Find ("GameSystem");
		sprint = gameSystem.GetComponent<Sprint> ();

		GetComponent<Text> ().text = "" + sprint.linesObjective;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
