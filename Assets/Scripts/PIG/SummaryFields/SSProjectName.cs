using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SSProjectName : MonoBehaviour {
	public GameObject gameSystem;

	// Use this for initialization
	void Start () {
		Project project = PersistantData.persistantData.projects [PersistantData.persistantData.projectIndex];
		GetComponent<Text>().text = project.name + " Sprint " + (project.sprintsDone + 1) + " Summary:";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
