using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectSelection : MonoBehaviour {
	int project = -1;
	GameObject previous = null;
	bool proj2available = false;
	bool proj3available = false;

	public GameObject select2;
	public GameObject select3;
	public Sprite X;
	// Use this for initialization
	void Start () {
	

		CheckProjectProgess ();



	}
	
	// Update is called once per frame
	void Update () {

	}
	public void closeSplash(GameObject splash){
		splash.transform.localScale = new Vector3 (0, 0, 0);
	}
	public void setWhich(int which){
		CheckProjectProgess ();
		
		if (which == 1) {
			if (proj2available) {
				project = which;
			}
		} else if (which == 2) {
			if (proj3available) {
				project = which;
			}
		} else {
			project = which;
		}



	}
	public void checkBox(GameObject which){
		if (which.name == "Select2 (1)") {
			if (proj2available) {
				if (previous != null) {
					previous.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
				}
				which.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
				previous = which;
			}
		} else if (which.name == "Select3 (1)") {
			if (proj3available) {
				if (previous != null) {
					previous.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
				}
				which.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
				previous = which;
			}
		} else if (which.name == "Select1 (1)") {

			if (previous != null) {
				previous.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
			}
			which.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			previous = which;

		}
	}
	public void confirm(){
		if (project != -1) {
			PersistantData.persistantData.projectIndex = project;
			PersistantData.persistantData.resetTasks (project);
			SceneManager.LoadScene ("SprintBacklogSelection");
		}
	}


	void CheckProjectProgess(){
		//check player progress and disallow projects that shouldnt be reached

		if (PlayerPrefs.HasKey("projectProgress")){

			if (PlayerPrefs.GetInt ("projectProgress") == 2) {
				proj2available = true;
				proj3available = true;
			} else if (PlayerPrefs.GetInt ("projectProgress") == 1) {
				proj2available = true;
				proj3available = false;
			} else {
				proj2available = false;
				proj3available = false;
			}
		}
		else{
			//PlayerPrefs.SetInt ("projectProgress") = 0;
				proj2available = false;
				proj3available = false;
		}



		/* Not doing PersistantData way
	PersistantData data = PersistantData.persistantData

		//project 1 complete?
		//assume project is complete, if any task has less than req LOC, then proj incomplete

		if (data.projectProgress == 2) {
			proj2available = true;
			proj3available = true;
		} else if (data.projectProgress == 1) {
			proj2available = true;
			proj3available = false;
		}
		//removed 8/20 because data.projectProgress is not updated in project summary

		/*bool projIncomplete = false;
		foreach (Task task in data.projects[0].tasks) {
			if (task.linesDone < task.lines)
				projIncomplete = true;
		}
		//set project 2 and 3 as blocked
		if (projIncomplete && data.projectProgress == 0) {
			proj2available = false;
			proj3available = false;
		}
		else {// project 1 is complete, test 2nd
			//project 2 complete?
			//assume project is complete, if any task has less than req LOC, then proj incomplete
			data.projectProgress = 1;
			projIncomplete = false;
			foreach (Task task in data.projects[1].tasks) {
				if (task.linesDone < task.lines)
					projIncomplete = true;
			}
			//set project 3 as blocked
			if (projIncomplete && data.projectP) {
				proj3available = false;
			} else {
				data.projectProgress = 2;
			}
		}*/

		//put in X's
		if (!proj2available) {
			select2.GetComponent<Image>().sprite = X;
			select2.GetComponent<Image> ().color = new Color (1, 1, 1, 1);

		}
		if (!proj3available) {
			select3.GetComponent<Image>().sprite = X;
			select3.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}


	}	

}