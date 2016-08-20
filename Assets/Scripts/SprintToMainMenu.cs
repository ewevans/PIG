using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SprintToMainMenu : MonoBehaviour {
	Button button;

	// Use this for initialization
	void Awake () {
		button = GetComponent<Button> ();
		//data = GetComponent<PersistantData>();

		button.onClick.AddListener (() => {
			whenClicked ();
		});
	}

	void whenClicked(){

		//if X'd on second sprint, the next time the project was started, a sprint would be skipped.
		//should fix that

		PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex].sprintsDone = 0;
		Debug.Log ("project index" + PersistantData.persistantData.projectIndex);
		SceneManager.LoadScene (0);


	}

	// Update is called once per frame

}

