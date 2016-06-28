using UnityEngine;
using System.Collections;

public class RoleAllocation : MonoBehaviour {
	public int allotment;
	private int movesMade;


	public GameObject coderZone;
	public GameObject debuggerZone;
	public GameObject testerZone;
	public GameObject gameSystem;


	private int numMoved;
	private int previousCoders = 0;
	private int previousDebuggers = 0;
	private int previousTesters = 0;

	private int[] previousValues = new int[3];
	// Use this for initialization
	void Start () {
	}
	public void init(){

		transform.parent.localScale = new Vector3 (1, 1, 1);

		GameSystem system = gameSystem.GetComponent<GameSystem> ();
		previousValues[0] = system.coders;
		previousValues[1] = system.debuggers;
		previousValues[2] = system.testers;

		GameObject obj;
		for (int index = 0; index < system.coders; ++index) {
			obj = (GameObject)Instantiate (Resources.Load ("UIElements/Developer"));
			obj.transform.SetParent (coderZone.transform);
			obj.transform.localScale = new Vector3 (1, 1, 1);
		}for (int index = 0; index < system.debuggers; ++index) {
			obj = (GameObject)Instantiate (Resources.Load ("UIElements/Developer"));
			obj.transform.SetParent (debuggerZone.transform);
			obj.transform.localScale = new Vector3 (1, 1, 1);
		}for (int index = 0; index < system.testers; ++index) {
			obj = (GameObject)Instantiate (Resources.Load ("UIElements/Developer"));
			obj.transform.SetParent (testerZone.transform);
			obj.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
	public void checkMoves(){
		movesMade = (int)Mathf.Max (previousValues [0] - coderZone.transform.childCount, 0) +
		(int)Mathf.Max (previousValues [1] - debuggerZone.transform.childCount, 0) +
		(int)Mathf.Max (previousValues [2] - testerZone.transform.childCount, 0);
		Debug.Log ("Moves Made: " + movesMade);
	}
	public bool allowDrop(DevDrop location){
		if (location.transform.childCount < previousValues [(int)location.type]) {
			--movesMade;
			return true;
		} else if (movesMade < allotment && location.transform.childCount >= previousValues [(int)location.type]) {
			++movesMade;
			return true;
		}
		return false;
	}
	public void apply(){
		GameSystem gs = gameSystem.GetComponent<GameSystem> ();
		gs.coders = coderZone.transform.childCount;
		gs.debuggers = debuggerZone.transform.childCount;
		gs.testers = testerZone.transform.childCount;
		gs.RoleAllocHudUpdate ();
		transform.parent.localScale = new Vector3 (0, 0, 0);
		int numChildren = coderZone.transform.childCount;
		for (int index = 0; index < numChildren; ++index) {
			DestroyImmediate(coderZone.transform.GetChild(0).gameObject);
		}
		numChildren = debuggerZone.transform.childCount;
		for (int index = 0; index < numChildren; ++index) {
			DestroyImmediate(debuggerZone.transform.GetChild(0).gameObject);
		}
		numChildren = testerZone.transform.childCount;
		for (int index = 0; index < numChildren; ++index) {
			DestroyImmediate(testerZone.transform.GetChild(0).gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
