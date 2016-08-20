using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SBSProduct : MonoBehaviour {

	public DialogBox dialogBox;

	public Project project;
	public GameObject sprintBacklog;
	public GameObject sprintsRemainingDisplay;
	public GameObject projectName;

	// Use this for initialization
	void Start () {
		//dialog box
		if (PersistantData.persistantData.projectIndex == 0 )
			updateDialogBox ("Each Project starts with making a Sprint Backlog", "At the beginning of the sprint, the team selects"
							+ " tasks to complete for the next 20 days. Make sure to:\n   Plan enough work for each sprint to complete "
							+ "the project on time.\n   Consider defect limits, or how many defects are "
							+ "allowed for that task.\n   Don't forget the customer's priority, which is color-coded in the text of the task.\n\n"
							+ "For the tutorial project, completing 1000 Lines of Code each sprint is suggested.");

		project = PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex];
		projectName.GetComponent<Text>().text = project.name;

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
