using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TasksComplete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PersistantData data = PersistantData.persistantData;

		GetComponent<Text> ().text = "Tasks Completed: " + data.completedSprintTasks + " of " + data.totalSprintTasks;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
