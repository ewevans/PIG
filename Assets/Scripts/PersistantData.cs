using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour {
	public static PersistantData persistantData;

	public Project[] projects = {
		new Project("TO FILL IN"),
		new Project("Car Systems"),
		new Project("TO FILL IN")
	};
	public void sprintsDisplay(){
		Debug.Log ("TotalSprints: " + projects [projectIndex].totalSprints);
		Debug.Log ("SprintsDone: " + projects [projectIndex].sprintsDone);
	}
	public Task[] sprintTasks = null;
	public int projectIndex = 1;
	public void resetTasks(){
		foreach (Project project in projects){
			project.sprintsDone = 0;
			foreach (Task task in project.tasks) {
				task.linesDone = 0;
			}
		}
	}
	void initProjects(){
		//	Project 1
		projects [0].addTask (new Task (200, 5, "Placeholder task 1.1", Task.Priority.LOW));
		projects [0].addTask (new Task (250, 10, "Placeholder task 1.2", Task.Priority.MEDIUM));
		projects [0].addTask (new Task (300, 15, "Placeholder task 1.3", Task.Priority.HIGH));
		projects [0].addTask (new Task (250, 10, "Placeholder task 1.4", Task.Priority.MEDIUM));
		projects [0].addTask (new Task (200, 5, "Placeholder task 1.5", Task.Priority.LOW));

		projects [0].setTotalSprints (2);
		projects [0].setSprintsDone (0);
		//	Project 2
		projects[1].addTask(new Task(800, 15,   "Transmission Control Unit", Task.Priority.HIGH));

		projects[1].addTask(new Task(400, 10,   "Human-machine Interface", Task.Priority.LOW));

		projects[1].addTask(new Task(100, 5,    "Seat Control System", Task.Priority.LOW));

		projects[1].addTask(new Task (250, 5,  "Battery Management System", Task.Priority.HIGH));

		projects[1].addTask(new Task (200, 10, "Speed Control Unit", Task.Priority.MEDIUM));

		projects[1].addTask(new Task (300, 10, "Global Positioning System", Task.Priority.LOW));

		projects[1].addTask(new Task (200, 10, "Door Control Unit", Task.Priority.MEDIUM));

		projects[1].addTask( new Task (550, 15, "Engine Control System", Task.Priority.HIGH));


		projects [1].setTotalSprints (3);
		projects [1].setSprintsDone (0);
		//	Project 3

		projects [2].addTask (new Task (200, 5, "Placeholder task 3.1", Task.Priority.LOW));
		projects [2].addTask (new Task (250, 10, "Placeholder task 3.2", Task.Priority.MEDIUM));
		projects [2].addTask (new Task (300, 15, "Placeholder task 3.3", Task.Priority.HIGH));
		projects [2].addTask (new Task (250, 10, "Placeholder task 3.4", Task.Priority.MEDIUM));
		projects [2].addTask (new Task (200, 5, "Placeholder task 3.5", Task.Priority.LOW));

		projects [2].setTotalSprints (4);
		projects [2].setSprintsDone (0);
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
