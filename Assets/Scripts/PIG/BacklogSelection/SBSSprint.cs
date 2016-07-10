using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SBSSprint : MonoBehaviour {

	private List<Task> tasks;
	// Use this for initialization
	void Start () {
		tasks = new List<Task> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void refreshList(){
		while (transform.childCount > 0) {
			DestroyImmediate (transform.GetChild (0).gameObject);
		}
		for (int index = 0; index < tasks.Count; ++index) {
			makeEntry (tasks [index], index);
		}
	}
	private void makeEntry(Task task, int index){
		GameObject entry = (GameObject)Instantiate (Resources.Load ("UIElements/SprintBacklogEntry"));

		entry.transform.SetParent (transform);
		entry.transform.localScale = new Vector3 (1, 1, 1);

		entry.GetComponentInChildren<Text> ().text = task.name + " - " + task.linesDone + "/" + task.lines;
		entry.GetComponent<SBSSprintEntry> ().index = index;
	}
	public void addTask(Task task){
		tasks.Add (task);
		refreshList ();
	}
	public void removeTask(Task task){
		tasks.Remove (task);
		refreshList ();
	}
	public void moveUp(int index){
		if (index > 0) {
			Task temp = tasks [index - 1];
			tasks[index - 1] = tasks[index];
			tasks [index] = temp;
			refreshList ();
		}
	}
	public void moveDown(int index){
		if (index < tasks.Count - 1) {
			Task temp = tasks [index + 1];
			tasks [index + 1] = tasks [index];
			tasks [index] = temp;
			refreshList ();
		}
	}
	public void confirm(){
		PersistantData.persistantData.sprintTasks = tasks.ToArray ();
		SceneManager.LoadScene ("Sprint");
	}

}
