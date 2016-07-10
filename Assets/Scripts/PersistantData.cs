using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour {
	public static PersistantData persistantData;

	public Project[] projects = {
		new Project()
	};
	public Task[] sprintTasks = null;
	public int projectIndex = 0;
	void Awake(){
		if (persistantData == null) 
		{
			DontDestroyOnLoad (gameObject);
			persistantData = this;
		} 
		else if (persistantData != this)
			Destroy (gameObject);

	}
	void Start(){
		if (persistantData == null) 
		{
			DontDestroyOnLoad (gameObject);
			persistantData = this;
		} 
		else if (persistantData != this)
			Destroy (gameObject);

	}
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	
	}
}
