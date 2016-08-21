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
	public GameObject successFailMessage;

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

		//Ethan: added 8/21 for SuccessFailMessage
		this.successFailMessage.GetComponent<Text> ().text = MakeSuccessFailMessage(completedTasks, data, defects);


		//Ethan: add 8/20 for project progression tracking
		if (completedTasks == project.tasks.Count && data.runningDefects < defects) {
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

	string MakeSuccessFailMessage(int tasksCompleted, PersistantData data, int defectsAllowed) {
		string message = "";
		Project project = data.projects [data.projectIndex];
		//success or fail
		if (tasksCompleted == project.tasks.Count && data.runningDefects < defectsAllowed) {
			message += "Success! You completed the ";
			if (project.sprintsDone == 2) {
				message += "Video Game Interface!\n";
			} else if (project.sprintsDone == 3) {
				message += "Smart Car System!\n";
			} else if (project.sprintsDone == 4) {
				message += "Rocket Ship!\n";
			}
		} else {
			message += "Project failed!\n";
		}
		//budget
		if (data.remainingBudget >= 0) {
			message += "The Product Owner is amazed you stayed on budget. ";
		} else {
			message += "The Product Owner is upset too much was spent on the project.";
		}

		//defects
		if (data.runningDefects > defectsAllowed) {
			message += "The project is too full of defects to complete the required functionality.";
		} else if (data.runningDefects > (defectsAllowed/2)){
			message += "The project fulfills all requirements, but has many defects still.";
		} else if (data.runningDefects == 0){
			message += "The project fulfills all requirements, and runs perfectly!";
		} else {
			message += "The project works, but has many defects still.";
		}
			
		return message;

	}

	// Update is called once per frame
	void Update () {
	
	}
}
