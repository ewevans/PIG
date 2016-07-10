using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SBSProduct : MonoBehaviour {

	public Project project;
	public GameObject sprintBacklog;
	// Use this for initialization
	void Start () {
		project = PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex];
		Debug.Log ("Pre loop");
		for (int index = 0; index < project.tasks.Length; ++index) {
			makeEntry (project.tasks [index], index);
			Debug.Log ("In Loop");
		}
		Debug.Log ("After Loop");
	}
	private void makeEntry(Task task, int index){
		GameObject entry = (GameObject)Instantiate (Resources.Load ("UIElements/ProductBacklogEntry"));

		entry.transform.SetParent (transform);
		entry.transform.localScale = new Vector3 (1, 1, 1);

		entry.GetComponentInChildren<Text> ().text = task.name + " - " + task.linesDone + "/" + task.lines;
		entry.GetComponent<SBSProductEntry> ().index = index;

		if (task.linesDone == task.lines) {
			entry.GetComponentInChildren<Text> ().text = task.name + " - COMPLETED!";
			entry.GetComponentInChildren<Button> ().enabled = false;
			entry.transform.GetChild (0).GetComponent<Image> ().color = new Color (0, 0, 0);
		}
	}
	public void moveToSprint(int index){
		sprintBacklog.GetComponent<SBSSprint>().addTask (project.tasks [index]);
	}
	public void removeFromSprint(int index){
		sprintBacklog.GetComponent<SBSSprint>().removeTask (project.tasks [index]);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
