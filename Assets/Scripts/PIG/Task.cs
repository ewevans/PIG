using UnityEngine;
using System.Collections;

public class Task {

	public int lines = 500;
	public int allowedDefects = 25;
	public string name = "name";
	public Task(int lines, int allowedDefects, string name){
		this.lines = lines;
		this.allowedDefects = allowedDefects;
		this.name = name;
	}
	public Task(){
		lines = 500;
		allowedDefects = 13;
		name = "name";
	}
	// Use this for initialization
}
