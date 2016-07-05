using UnityEngine;
using System.Collections;

public class ScrumBoard : MonoBehaviour {
	public GameObject productBacklog;
	public GameObject sprintBacklog;
	public GameObject chart;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void refreshBoard(){
		productBacklog.GetComponent<ProductBacklog> ().updateBacklog ();
		sprintBacklog.GetComponent<SprintBacklog> ().updateBacklog ();
		chart.GetComponent<GraphingScript> ().openChart ();
	}
	public void openBoard(){
		transform.localScale = new Vector3 (1, 1, 1);
		refreshBoard ();
	}
	public void closeBoard(){
		chart.GetComponent<GraphingScript> ().closeChart ();
		transform.localScale = new Vector3 (0, 0, 0);
	}
}
