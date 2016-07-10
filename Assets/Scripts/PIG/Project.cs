using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Project {

	public Task[] tasks;
	// Use this for initialization
	void Start () {
	
	}
	void initTestingTasks(){
		tasks = new Task[3];

		//Tasks for example sprint in Project 2
		//Task 1 is Transmission Control Unit with 800 LOC
		tasks[0] = new Task(800, 15, "Transmission Control Unit");
		//Task 3 is Human-machine Interface with 400 LOC
		tasks[1] = new Task(400, 10, "Human-machine Interface");
		//Task 3 is Seat Control System with 100 LOC
		tasks[2] = new Task(100, 5, "Seat Control System");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
