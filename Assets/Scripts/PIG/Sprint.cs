using UnityEngine;
using System.Collections;

public class Sprint : MonoBehaviour {

	public int linesObjective = 0;
	public int linesDone = 0;
	public int defectLimit = 0;
	public int defects = 0;
	public int sprintDuration = 0;
	public int currentDay = 0;

	public int updateLinesObjective(int change){
		linesObjective += change;
		return linesObjective;
	}
	public int updateLinesDone(int change){
		linesDone += change;
		return linesDone;
	}
	public int updateDefects(int change){
		defects += change;
		return defects;
	}
	public int updateDefectLimit(int change){
		defectLimit += change;
		return defectLimit;
	}
	public int updateSprintDuration(int change){
		sprintDuration += change;
		return sprintDuration;
	}
	public int updateCurrentDay(int change){
		currentDay += change;
		return currentDay;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
