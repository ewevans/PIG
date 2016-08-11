using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Project {

	public string name;
	public int totalSprints;
	public int sprintsDone = 0;
	public Project(){
		name = "NONE";
	}
	public Project(string name){
		this.name = name;
		//initTestingTasks ();
	}
	public List<Task> tasks = new List<Task>();
	// Use this for initialization
	void Start () {
	}
	public void setTotalSprints(int sprints){
		totalSprints = sprints;
	}
	public void setSprintsDone(int sprints){
		sprintsDone = sprints;
	}
	public void addTask(Task task){
		tasks.Add (task);
	}
	void initTestingTasks(){

		//Tasks for example sprint in Project 2
		//Task 1 is Transmission Control Unit with 800 LOC
		tasks.Add(new Task(800, 15,   "Transmission Control Unit", Task.Priority.HIGH));
		//Task 3 is Human-machine Interface with 400 LOC
		tasks.Add(new Task(400, 10,   "Human-machine Interface", Task.Priority.LOW));
		//Task 3 is Seat Control System with 100 LOC
		tasks.Add(new Task(100, 5,    "Seat Control System", Task.Priority.LOW));

		tasks.Add(new Task (250, 5,  "Battery Management System", Task.Priority.HIGH));

		tasks.Add(new Task (200, 10, "Speed Control Unit", Task.Priority.MEDIUM));

		tasks.Add(new Task (300, 10, "Global Positioning System", Task.Priority.LOW));

		tasks.Add(new Task (200, 10, "Door Control Unit", Task.Priority.MEDIUM));

		tasks.Add( new Task (550, 15, "Engine Control System", Task.Priority.HIGH));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
