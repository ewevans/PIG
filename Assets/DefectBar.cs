using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefectBar : MonoBehaviour {
	public GameObject greenBar;
	public GameObject yellowBar;
	public GameObject redBar;

	public int maxDefects;
	private int greenDefects;
	private int yellowDefects;
	private int redDefects;


	// Use this for initialization
	void Start () {

	}
	public void setMax(int max){
		maxDefects = max;
		greenDefects = max / 2;
		yellowDefects = (max - greenDefects) / 2;
		redDefects = max - yellowDefects;
	}
	public void reportDefects(int current){
		greenBar.GetComponent<Image> ().fillAmount = Mathf.Min (1, (float)current / (float)greenDefects);
		int remaining = 0;
		if (current > greenDefects) {
			remaining = current - greenDefects;
			yellowBar.GetComponent<Image> ().fillAmount = Mathf.Min (1, (float)remaining / (float)yellowDefects);
		} else {
			yellowBar.GetComponent<Image> ().fillAmount = 0;
		}
		if (current > yellowDefects + greenDefects) {
			remaining = current - yellowDefects;
			redBar.GetComponent<Image> ().fillAmount = Mathf.Min (1, (float)remaining / (float)redDefects);
		} else {
			redBar.GetComponent<Image> ().fillAmount = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
