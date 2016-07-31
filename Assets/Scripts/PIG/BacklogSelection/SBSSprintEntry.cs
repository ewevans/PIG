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
