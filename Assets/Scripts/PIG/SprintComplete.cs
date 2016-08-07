using UnityEngine;
using System.Collections;

public class SprintComplete : MonoBehaviour {
	public GameObject gameSystem;
	public GameObject autoplay;
	public GameObject manualplay;

	private Selection selected = Selection.NONE;

	enum Selection{
		NONE,
		AUTO,
		MANUAL
	};

	public void checkAuto(){
		selected = Selection.AUTO;
		manualplay.transform.GetChild (0).localScale = new Vector3 (0, 0, 0);
		autoplay.transform.GetChild (0).localScale = new Vector3 (1, 1, 1);
	}
	public void checkManual(){
		selected = Selection.MANUAL;
		autoplay.transform.GetChild (0).localScale = new Vector3 (0, 0, 0);
		manualplay.transform.GetChild (0).localScale = new Vector3 (1, 1, 1);
	}
	public void submit(){
		switch (selected) {
		case Selection.NONE:
			break;
		case Selection.AUTO:
			gameSystem.GetComponent<GameSystem> ().autoPlaySelected ();
			closeWindow ();
			break;
		case Selection.MANUAL:
			gameSystem.GetComponent<GameSystem> ().manualPlaySelected ();
			closeWindow ();
			break;
		}
		
	}
	public void closeWindow(){
		transform.localScale = new Vector3 (0, 0, 0);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
