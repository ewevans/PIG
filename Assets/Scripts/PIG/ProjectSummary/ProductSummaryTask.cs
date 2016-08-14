using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductSummaryTask : MonoBehaviour {

	public GameObject taskName;
	public GameObject taskLines;

	public void setName(string name){
		taskName.GetComponent<Text> ().text = name;
	}
	public void setLines(string lines){
		taskLines.GetComponent<Text> ().text = lines;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
