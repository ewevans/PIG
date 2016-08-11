using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SBSProduct : MonoBehaviour {

	public DialogBox dialogBox;

	public Project project;
	public GameObject sprintBacklog;
	public GameObject sprintsRemainingDisplay;

	// Use this for initialization
	void Start () {
		//dialog box
		updateDialogBox ("Organizing a Project into Sprints", "Each project is broken down into tasks that make up the product backlog. During " +
			"a sprint planning meeting, the team takes tasks from the the product backlog and moves them to the sprint backlog, which is a set " +
			"of work for the team to complete over a designated, fixed time period. Each sprint should carry a set deliverable that is the goal " +
			"of the team for that sprint. In industry, if a sprint goal becomes obsolete, a sprint may be cancelled.");

		project = PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex];
		sprintsRemainingDisplay.GetComponent<Text> ().text = "" + (project.totalSprints - project.sprintsDone);
		//Debug.Log ("Pre loop");
		for (int index = 0; index < project.tasks.Count; ++index) {
			makeEntry (project.tasks [index], index);
			//Debug.Log ("In Loop");
		}
		//Debug.Log ("After Loop");
	}
	private void makeEntry(Task task, int index){
		GameObject entry = (GameObject)Instantiate (Resources.Load ("UIElements/ProductBacklogEntry"));

		entry.transform.SetParent (transform);
		entry.transform.localScale = new Vector3 (1, 1, 1);

		entry.GetComponent<SBSProductEntry> ().setPriority (task.priority);
		entry.GetComponent<SBSProductEntry> ().setName (task.name);
		entry.GetComponent<SBSProductEntry> ().setDefects ("" + task.allowedDefects);
		entry.GetComponent<SBSProductEntry> ().index = transform.childCount - 1;
		if (task.linesDone == task.lines) {
			entry.GetComponent<SBSProductEntry> ().setLines ("COMPLETED!");
			entry.GetComponentInChildren<Button> ().enabled = false;
			entry.transform.GetChild (0).GetComponent<Image> ().color = new Color (0, 0, 0);
		} else {
			entry.GetComponent<SBSProductEntry> ().setLines ("" + task.linesDone + "/" + task.lines);
		}
	}
	public void moveToSprint(int index){
		sprintBacklog.GetComponent<SBSSprint>().addTask (project.tasks [index]);
	}
	public void removeFromSprint(int index){
		sprintBacklog.GetComponent<SBSSprint>().removeTask (project.tasks [index]);
	}

	public void updateDialogBox(string title, string body){
		DialogBox dialog = dialogBox.GetComponent<DialogBox> ();
		dialog.init(title, body);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
