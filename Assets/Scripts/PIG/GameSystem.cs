using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour {

	public int coders = 2;
	public int debuggers = 2;
	public int testers = 0;

	private Sprint sprint;

	public GameObject defectsDisplay;
	public GameObject linesNumber;
	public GameObject linesProgress;


	public void linesPerCoder(int lines){
		int currentLines = sprint.updateLinesDone (lines * coders);
		linesNumber.GetComponent<Text> ().text = "" + currentLines;
		linesProgress.GetComponent<Image> ().fillAmount = (float)currentLines / (float)sprint.linesObjective;
	}
	public void flatLines(int lines){
		int currentLines = sprint.updateLinesDone (lines);
		linesNumber.GetComponent<Text> ().text = "" + currentLines;
		linesProgress.GetComponent<Image> ().fillAmount = (float)currentLines / (float)sprint.linesObjective;
	}
	public void defectsPerCoder(int defects){
		int currentDefects = sprint.updateDefects (defects * coders);
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
	}
	public void flatDefects(int defects){
		int currentDefects = sprint.updateDefects (defects);
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
	}
	// Use this for initialization
	void Start () {
		sprint = GetComponent<Sprint> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
