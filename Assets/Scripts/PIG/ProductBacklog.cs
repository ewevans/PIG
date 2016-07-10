using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductBacklog : MonoBehaviour {

	public GameObject gameSystem;

	// Use this for initialization
	void Start () {
	}
	public void updateBacklog(){
		while (transform.childCount > 0) {
			DestroyImmediate (transform.GetChild (0).gameObject);
		}
		int linesCompleted = gameSystem.GetComponent<Sprint> ().linesDone;
		Task[] tasks = gameSystem.GetComponent<Sprint> ().tasks;
		GameObject taskObj;
		foreach(Task task in tasks){
			taskObj = (GameObject)Instantiate (Resources.Load ("UIElements/ListedTask"));
			taskObj.transform.SetParent (transform);
			if (task.lines <= linesCompleted) {
				taskObj.GetComponent<Text> ().text = task.name + ": " + task.lines + "/" + task.lines;
				linesCompleted -= task.lines;
			}
			else{
				taskObj.GetComponent<Text> ().text = task.name + ": " + linesCompleted + "/" + task.lines;
				linesCompleted = 0;
			}
			taskObj.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
