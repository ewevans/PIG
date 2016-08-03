using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectSelection : MonoBehaviour {
	int project = -1;
	GameObject previous = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void setWhich(int which){
		project = which;
	}
	public void checkBox(GameObject which){
		if (previous != null) {
			previous.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
		}
		which.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		previous = which;
	}
	public void confirm(){
		if (project != -1) {
			PersistantData.persistantData.projectIndex = project;
			PersistantData.persistantData.resetTasks ();
			SceneManager.LoadScene ("SprintBacklogSelection");
		}
	}
}
