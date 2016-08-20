using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProductSummaryTaskList : MonoBehaviour {

	public GameObject linesDone;
	public GameObject defects;
	public GameObject budget;
	public GameObject tasks;
	public GameObject title;

	// Use this for initialization
	void Start () {
		PersistantData data = PersistantData.persistantData;
		Project project = data.projects [data.projectIndex];

		title.GetComponent<Text> ().text = "Project: " + project.name;

		int totalLines = 0;
		int linesDone = 0;
		int defects = 0;
		int completedTasks = 0;
		foreach (Task task in project.tasks) {
			makeEntry (task);
			totalLines += task.lines;
			linesDone += task.linesDone;
			defects += task.allowedDefects;
			if (task.linesDone >= task.lines) {
				completedTasks++;
			}
			Debug.Log ("Adding Task " + task.name);
			Debug.Log ("Lines: " + task.linesDone);
			Debug.Log ("Goal: " + task.lines);
			Debug.Log ("Total: " + totalLines);
		}
		tasks.GetComponent<Text> ().text = "Tasks Completed: " + completedTasks + " of " + project.tasks.Count;
		this.linesDone.GetComponent<Text> ().text = "" + linesDone + " of " + totalLines;
		this.defects.GetComponent<Text> ().text = "" + data.runningDefects + " of " + defects + " allowed";
		this.budget.GetComponent<Text> ().text = "$" + data.remainingBudget;

		//Ethan: add 8/20 for project progression tracking
		if (completedTasks == project.tasks.Count) {
			if (PlayerPrefs.HasKey ("projectProgress")) {
				if (PlayerPrefs.GetInt ("projectProgress") < (data.projectIndex+1)) {
					PlayerPrefs.SetInt ("projectProgress", (data.projectIndex + 1));
				}
			} else {
				PlayerPrefs.SetInt ("projectProgress", (data.projectIndex + 1));
			}		
		}

	}
	public void close(){
		SceneManager.LoadScene ("Main Menu");
	}
	void makeEntry(Task task){
		GameObject entry = (GameObject)Instantiate (Resources.Load ("UIElements/ProjectSummaryTask"));

		entry.GetComponent<ProductSummaryTask> ().setName (task.name);
		entry.GetComponent<ProductSummaryTask> ().setLines ("" + task.linesDone + " / " + task.lines);

		entry.transform.SetParent (transform);
		entry.transform.localScale = new Vector3 (1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
