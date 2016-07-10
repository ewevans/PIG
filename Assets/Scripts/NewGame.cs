using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGame : MonoBehaviour {


	public void StartNewGame (int SceneToChangeTo) 
	{
		SceneManager.LoadScene ("SprintBacklogSelection");
	}
	

}
