using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Project {

	public Project(){
		initTestingTasks ();
	}
	public Task[] tasks;
	// Use this for initialization
	void Start () {
	
	}
	void initTestingTasks(){
		tasks = new Task[8];

		//Tasks for example sprint in Project 2
		//Task 1 is Transmission Control Unit with 800 LOC
		tasks[0] = new Task(800, 15, "Transmission Control Unit");
		//Task 3 is Human-machine Interface with 400 LOC
		tasks[1] = new Task(400, 10, "Human-machine Interface");
		//Task 3 is Seat Control System with 100 LOC
		tasks[2] = new Task(100, 5, "Seat Control System");

		tasks [3] = new Task (250, 5, "Battery Management System");

		tasks [4] = new Task (200, 10, "Speed Control Unit");

		tasks [5] = new Task (300, 10, "Global Positioning System");

		tasks [6] = new Task (200, 10, "Door Control Unit");

		tasks [7] = new Task (550, 15, "Engine Control System");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
