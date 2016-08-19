using UnityEngine;
using System.Collections;

public class NoEventText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
