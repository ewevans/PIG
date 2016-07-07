using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Sprint : MonoBehaviour {

	public int linesObjective = 1500;
	public int linesDone = 0;
	public int defectLimit = 0;
	public int defects = 0;
	public int sprintDuration = 0;
	public int currentDay = 0;
	public int budget = 5000;

	public List<Turn> TurnList = new List<Turn> ();

	public Task[] tasks;

	public int updateLinesObjective(int change){
		linesObjective += change;
		if (linesObjective < 0) {
			linesObjective = 0;
		}
		return linesObjective;
	}
	public int updateLinesDone(int change){
		linesDone += change;
		if (linesDone < 0) {
			linesDone = 0;
		}
		return linesDone;
	}
	public int updateDefects(int change){
		defects += change;
		if (defects < 0) {
			defects = 0;
		}
		return defects;
	}
	public int updateDefectLimit(int change){
		defectLimit += change;
		if (defectLimit < 0) {
			defectLimit = 0;
		}
		return defectLimit;
	}
	public int updateSprintDuration(int change){
		sprintDuration += change;
		if (sprintDuration < 0) {
			sprintDuration = 0;
		}
		return sprintDuration;
	}

	public int updateCurrentDay(int change){
		currentDay += change;
		if (currentDay < 0) {
			currentDay = 0;
		}
		return currentDay;
	}


	// Use this for initialization
	void Start () {
		tasks = new Task[3];

		GameObject taskHolder = GameObject.Find ("TaskHolder");
		float holderWidth = taskHolder.GetComponent<RectTransform> ().rect.width;

		/* 
		//	This first loop fills in 4 tasks for our test run, it should be removed eventually
		for (int index = 0; index < tasks.Length; ++index) {
			tasks [index] = new Task (500 + 100 * index, 13 + 3 * index, "Task " + (index + 1));
		}
		tasks [2].lines = 200;
		*/

		//Tasks for example sprint in Project 2
		//Task 1 is Transmission Control Unit with 800 LOC
		tasks[0] = new Task(800, 15, "Transmission Control Unit");
		//Task 3 is Human-machine Interface with 400 LOC
		tasks[1] = new Task(400, 10, "Human-machine Interface");
		//Task 3 is Seat Control System with 100 LOC
		tasks[2] = new Task(100, 5, "Seat Control System");

		//	This second loop figures out lines based on tasks and whatnot, also creates the task identifiers
		for (int index = 0; index < tasks.Length; ++index) {
			linesObjective += tasks [index].lines;
			defectLimit += tasks [index].allowedDefects;
		}

		float taskSizer = holderWidth / linesObjective;
		for (int index = 0; index < tasks.Length; ++index) {
			GameObject task = (GameObject)Instantiate (Resources.Load ("UIElements/Task"));
			task.transform.GetChild (0).GetComponent<Text> ().text = tasks [index].name;
			task.GetComponent<LayoutElement> ().preferredWidth = tasks [index].lines * taskSizer;
			task.transform.SetParent (taskHolder.transform);
			task.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
