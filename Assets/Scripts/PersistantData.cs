using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour {
	public static PersistantData persistantData;

	public Project[] projects = {
		new Project("TO FILL IN"),
		new Project("Car Systems"),
		new Project("TO FILL IN")
	};
	public Task[] sprintTasks = null;
	public int projectIndex = 1;
	void resetTasks(){
		foreach (Project project in projects){
			foreach (Task task in project.tasks) {
				task.linesDone = 0;
			}
		}
	}
	void initProjects(){
		//	Project 1
		//	Project 2
		projects[1].addTask(new Task(800, 15,   "Transmission Control Unit", Task.Priority.HIGH));

		projects[1].addTask(new Task(400, 10,   "Human-machine Interface", Task.Priority.LOW));

		projects[1].addTask(new Task(100, 5,    "Seat Control System", Task.Priority.LOW));

		projects[1].addTask(new Task (250, 5,  "Battery Management System", Task.Priority.HIGH));

		projects[1].addTask(new Task (200, 10, "Speed Control Unit", Task.Priority.MEDIUM));

		projects[1].addTask(new Task (300, 10, "Global Positioning System", Task.Priority.LOW));

		projects[1].addTask(new Task (200, 10, "Door Control Unit", Task.Priority.MEDIUM));

		projects[1].addTask( new Task (550, 15, "Engine Control System", Task.Priority.HIGH));
		//	Project 3
	}
	void Awake(){
		if (persistantData == null) 
		{
			DontDestroyOnLoad (gameObject);
			persistantData = this;
			persistantData.initProjects ();
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
