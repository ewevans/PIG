using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BudgetSummary : MonoBehaviour {

	private GameObject gameSystem;
	private Sprint sprint;

	public int budgetnumber;

	// Use this for initialization
	void Start () {
		gameSystem = GameObject.Find ("GameSystem");
		budgetnumber = PlayerPrefs.GetInt ("Budget");
		Debug.Log ("Budget is" + budgetnumber);
		if (budgetnumber < 0) {
			GetComponent<Text> ().color = Color.red;
			GetComponent<Text> ().text = "-$ " + budgetnumber + " you went over budget!!";
		} else {
			GetComponent<Text> ().color = Color.green;
			GetComponent<Text> ().text = "+$" + budgetnumber + " you stayed within budget!! Good Job";
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
