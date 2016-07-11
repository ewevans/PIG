using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SBSProductEntry : MonoBehaviour {

	public int index = 0;



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
