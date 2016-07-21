using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GraphingScript : MonoBehaviour {

	public int numDays;
	public int linesObjective;
	public GameObject gameSystem;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void refreshChart(){
		List<Turn> turns = gameSystem.GetComponent<Sprint> ().TurnList;
		linesObjective = gameSystem.GetComponent<Sprint> ().linesObjective;
		numDays = gameSystem.GetComponent<Sprint> ().sprintDuration;
		drawTicks ();
		for (int index = 0; index < turns.Count - 1; ++index) {
			drawDot (turns [index].turnDay, linesObjective - turns [index].turnLinesComnpleted);
			drawLine (turns [index].turnDay, linesObjective - turns [index].turnLinesComnpleted, turns [index + 1].turnDay, linesObjective - turns [index+1].turnLinesComnpleted);
		}
		if (turns.Count > 0) {
			drawDot (turns [turns.Count - 1].turnDay, linesObjective - turns [turns.Count - 1].turnLinesComnpleted);
			drawLine (turns [turns.Count - 1].turnDay, linesObjective - turns [turns.Count - 1].turnLinesComnpleted, gameSystem.GetComponent<Sprint> ().currentDay, linesObjective - gameSystem.GetComponent<Sprint> ().linesDone);
		}
		drawDot (gameSystem.GetComponent<Sprint> ().currentDay, linesObjective - gameSystem.GetComponent<Sprint> ().linesDone);
	}
	public void closeChart(){
		while (transform.childCount > 0) {
			DestroyImmediate (transform.GetChild (0).gameObject);
		}
	}
	public void openChart(){
		refreshChart ();
	}
	void drawTicks(){
		int linesPortion = linesObjective / 5;
		int daysPortion = numDays / 4;
		for (int index = 1; index * daysPortion < numDays; ++index) {
			drawTick (index * daysPortion, true);
		}
		for (int index = 1; index * linesPortion < linesObjective; ++index) {
			drawTick (index * linesPortion, false);
		}
	}
	void drawTick(int value, bool horizontal){
		/*
		GameObject tick = (GameObject)Instantiate (Resources.Load ("UIElements/LineSegment"));
		tick.transform.SetParent (transform);
		tick.transform.localScale = new Vector3 (10, 1, 1);
		*/
		if (horizontal) {
			drawLine (value, 0, value, linesObjective, new Color(.75f, .75f, .75f));
			/*
			tick.transform.localPosition = new Vector3 (value * GetComponent<RectTransform> ().rect.width / (float)numDays, 0, 0);
			tick.transform.localRotation = Quaternion.Euler (0, 0, 90);
*/
			GameObject number = (GameObject)Instantiate (Resources.Load ("UIElements/DaysAxis"));
			number.GetComponent<Text> ().text = "" + value;
			number.transform.SetParent (transform);
			number.transform.localPosition = new Vector3 (value * GetComponent<RectTransform> ().rect.width / (float)numDays, 0, 0);
			number.transform.localScale = new Vector3 (1, 1, 1);

		} else {
			drawLine (0, value, numDays, value, new Color(.75f, .75f, .75f));
			/*
			tick.transform.localPosition = new Vector3 (0, value * GetComponent<RectTransform> ().rect.height / (float)linesObjective, 0);
			tick.transform.localRotation = Quaternion.Euler (0, 0, 0);
*/
			GameObject number = (GameObject)Instantiate (Resources.Load ("UIElements/LinesAxis"));
			number.GetComponent<Text> ().text = "" + value;
			number.transform.SetParent (transform);
			number.transform.localPosition = new Vector3 (0, value * GetComponent<RectTransform> ().rect.height / (float)linesObjective, 0);
			number.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
	void drawDot(int x, int y){
		GameObject point = (GameObject)Instantiate (Resources.Load ("UIElements/Point"));

		x = (int)((float)x * (float)GetComponent<RectTransform> ().rect.width / (float)numDays);

		y = (int)((float)y * (float)GetComponent<RectTransform> ().rect.height / (float)linesObjective);

		point.transform.SetParent (transform);
		point.transform.localScale = new Vector3 (1, 1, 1);
		point.transform.localPosition = new Vector3 (x, y, 0);
	}
	void drawLine(int x1, int y1, int x2, int y2){
		drawLine (x1, y1, x2, y2, Color.black);
	}
	void drawLine(int x1, int y1, int x2, int y2, Color color){
		x1 = (int)((float)x1 * (float)GetComponent<RectTransform> ().rect.width / (float)numDays);
		x2 = (int)((float)x2 * (float)GetComponent<RectTransform> ().rect.width / (float)numDays);

		y1 = (int)((float)y1 * (float)GetComponent<RectTransform> ().rect.height / (float)linesObjective);
		y2 = (int)((float)y2 * (float)GetComponent<RectTransform> ().rect.height / (float)linesObjective);
		GameObject line = (GameObject)Instantiate(Resources.Load("UIElements/LineSegment"));
		line.GetComponent<Image> ().color = color;


		Transform l = line.GetComponent<Transform> ();
		l.SetParent (transform);

		int dy = y2 - y1;
		int dx = x2 - x1;
		float theta = Mathf.Rad2Deg * Mathf.Atan ((float)dy / (float)dx);
		float len = Mathf.Sqrt (dx * dx + dy * dy);

		l.localScale = new Vector3 (len, 1, 1);
		l.localRotation = Quaternion.Euler (0, 0, theta);
		l.localPosition = new Vector3 (x1, y1, 0);
	}

}
