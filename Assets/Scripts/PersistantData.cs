using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour {
	public static PersistantData persistantData;

	public Project[] projects = {
		new Project("Videogame Interface"),
		new Project("Smart Car Systems"),
		new Project("Rocket Systems")
	};
	public void sprintsDisplay(){
		Debug.Log ("TotalSprints: " + projects [projectIndex].totalSprints);
		Debug.Log ("SprintsDone: " + projects [projectIndex].sprintsDone);
	}
	public Task[] sprintTasks = null;
	public int projectIndex = 1;

	public int totalSprintTasks = 0;
	public int completedSprintTasks = 0;
	public int remainingBudget = 0;
	public int runningDefects = 0;

	//added parameter to selectively delete only one project's tasks
	public void resetTasks(int which){
		totalSprintTasks = 0;
		completedSprintTasks = 0;
		remainingBudget = 0;
		runningDefects = 0;
		projects [which].sprintsDone = 0;

		/*
		foreach (Project project in projects){
			project.sprintsDone = 0;
			foreach (Task task in project.tasks) {
				task.linesDone = 0;
			}
		}*/
		//Ethan 8/20
		//only deleting started project's tasks due to messing up how checking proj progress
		foreach (Task task in projects[which].tasks) {
			task.linesDone = 0;
		}

	}
	void initProjects(){
		//	Project 1
		projects [0].addTask (new Task (500, 10, "Menu System", Task.Priority.MEDIUM));
		projects [0].addTask (new Task (500, 10, "Game Preferences", Task.Priority.LOW));
		projects [0].addTask (new Task (1000, 15, "Player Input System", Task.Priority.HIGH));

		projects [0].setTotalSprints (2);
		projects [0].setSprintsDone (0);
		//	Project 2
		projects[1].addTask(new Task(1000, 15,   "Transmission Control Unit", Task.Priority.HIGH));

		projects[1].addTask(new Task(550, 10,   "Human-machine Interface", Task.Priority.MEDIUM));

		projects[1].addTask(new Task(100, 5,    "Seat Control System", Task.Priority.LOW));

		projects[1].addTask(new Task (250, 5,  "Battery Management System", Task.Priority.HIGH));

		projects[1].addTask(new Task (200, 10, "Speed Control Unit", Task.Priority.MEDIUM));

		projects[1].addTask(new Task (450, 10, "Global Positioning System", Task.Priority.LOW));

		projects[1].addTask(new Task (200, 10, "Door Control Unit", Task.Priority.MEDIUM));

		projects[1].addTask( new Task (550, 5, "Engine Control System", Task.Priority.HIGH));


		projects [1].setTotalSprints (3);
		projects [1].setSprintsDone (0);
		//	Project 3

		projects [2].addTask (new Task (1050, 10, "Structural Systems", Task.Priority.HIGH));
		projects [2].addTask (new Task (575, 10, "Payload Systems", Task.Priority.LOW));
		projects [2].addTask (new Task (725, 15, "Guidance Systems", Task.Priority.MEDIUM));
		projects [2].addTask (new Task (900, 15, "Propulsion Systems", Task.Priority.HIGH));
		projects [2].addTask (new Task (625, 5, "Life Support Systems", Task.Priority.HIGH));
		projects [2].addTask (new Task (925, 15, "Communications Systems", Task.Priority.MEDIUM));

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
