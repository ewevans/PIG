using UnityEngine;
using System.Collections;

public class SBSSprintEntry : MonoBehaviour {

	public int index = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void upArrow(){
		transform.parent.GetComponent<SBSSprint> ().moveUp (index);
	}
	public void downArrow(){
		transform.parent.GetComponent<SBSSprint> ().moveDown (index);
	}
}
