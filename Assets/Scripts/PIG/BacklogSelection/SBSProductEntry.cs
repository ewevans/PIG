using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SBSProductEntry : MonoBehaviour {

	public int index = 0;
	public GameObject name;
	public GameObject lines;

	public void setName(string name){
		this.name.GetComponent<Text> ().text = name;
	}
	public void setLines(string lines){
		this.lines.GetComponent<Text> ().text = lines;
	}
	public void setPriority(Task.Priority priority){
		switch (priority) {
		case Task.Priority.HIGH:
			name.GetComponent<Text> ().color = new Color (223f/255f, 0, 0);
			lines.GetComponent<Text> ().color = new Color (223f/255f, 0, 0);
			break;
		case Task.Priority.MEDIUM:
			name.GetComponent<Text> ().color = new Color (144f/255f, 140f/255f, 0);
			lines.GetComponent<Text> ().color = new Color (144f/255f, 140f/255f, 0);
			break;
		case Task.Priority.LOW:
			name.GetComponent<Text> ().color = new Color (6f/255f, 176f/255f, 0);
			lines.GetComponent<Text> ().color = new Color (6f/255f, 176f/255f, 0);
			break;
		default:
			break;
		}

	}


	private bool inSprint = false;
	void Start(){


	}
	// Update is called once per frame
	void Update () {
	
	}
	public void checkBox(){
		if (!inSprint) {
			transform.parent.GetComponent<SBSProduct> ().moveToSprint (index);
			transform.GetChild (0).GetChild (0).GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		} else {
			transform.parent.GetComponent<SBSProduct> ().removeFromSprint (index);
			transform.GetChild (0).GetChild (0).GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		}
		inSprint = !inSprint;
	}


}
