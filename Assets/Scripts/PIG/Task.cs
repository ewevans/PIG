using UnityEngine;
using System.Collections;

public class Task {

	public enum Priority{
		LOW,
		MEDIUM,
		HIGH
	};
	public Priority priority;
	public int lines = 500;
	public int allowedDefects = 25;
	public string name = "name";
	public int linesDone = 0;
	public Task(int lines, int allowedDefects, string name, Priority priority){
		this.lines = lines;
		this.allowedDefects = allowedDefects;
		this.name = name;
		this.priority = priority;
	}
	public Task(){
		lines = 500;
		allowedDefects = 13;
		name = "name";
		priority = Priority.LOW;
	}
	// Use this for initialization
}
