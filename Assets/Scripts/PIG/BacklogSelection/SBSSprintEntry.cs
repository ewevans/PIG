using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SBSSprintEntry : MonoBehaviour {

	public int index = 0;
	public GameObject name;
	public GameObject lines;

	public void setName(string name){
		this.name.GetComponent<Text>().text = name;
	}
	public void setLines(string lines){
		this.lines.GetComponent<Text>().text = lines;
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void upArrow(){
		index = transform.GetSiblingIndex ();
		transform.parent.GetComponent<SBSSprint> ().moveUp (index);
	}
	public void downArrow(){
		index = transform.GetSiblingIndex ();
		transform.parent.GetComponent<SBSSprint> ().moveDown (index);
	}
}
