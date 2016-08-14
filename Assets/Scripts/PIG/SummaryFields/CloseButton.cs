using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CloseButton : MonoBehaviour {

	private int sprintsLeft = 0;
	public void LoadLevel(){
		PersistantData.persistantData.sprintsDisplay ();
		PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex].sprintsDone+= 1;
		sprintsLeft = PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex].totalSprints -
			PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex].sprintsDone;
		if (sprintsLeft > 0) {
			SceneManager.LoadScene ("SprintBacklogSelection");
		} else {
			SceneManager.LoadScene ("ProjectSummary");
		}
	}
	void Start(){
	}

}
