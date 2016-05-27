using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour {

	public int coders = 2;
	public int debuggers = 2;
	public int testers = 0;

	private Sprint sprint;

	private enum State{
		NONE_PLAYED,
		EFFECT_PLAYED,
		DEV_PLAYED
	};
    public Deck deck = new Deck();

	public GameObject defectsDisplay;
	public GameObject linesNumber;
	public GameObject linesProgress;
	public GameObject daysText;
	public GameObject hand;
	public GameObject defectBar;
	public GameObject roleAllocation;
	public GameObject dayIndicator;
	public GameObject dayIndicatorText;

	private State state = State.NONE_PLAYED;


	private string[] cards = {
		"Coding30",
		"Coding40",
		"Coding50",
		"Coding75-10",
		"Coding75-0"
	};
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
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public void flatDefects(int defects){
		int currentDefects = sprint.updateDefects (defects);
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public bool nextDay(){
		int currentDay = sprint.updateCurrentDay (1);
		daysText.GetComponent<Text> ().text = "Day " + currentDay + " of " + sprint.sprintDuration;
		dayIndicatorText.GetComponent<Text> ().text = "" + currentDay;
		float location = linesProgress.GetComponent<RectTransform> ().rect.width * (float)currentDay / (float)sprint.sprintDuration + linesProgress.GetComponent<RectTransform>().rect.position.x;
		dayIndicator.transform.localPosition = new Vector3 (location, dayIndicator.transform.localPosition.y, dayIndicator.transform.localPosition.z);
		return currentDay < sprint.sprintDuration;
	}
	public bool playCard(Card.CardType type){
		if (type == Card.CardType.DEVELOPMENT) {
			if (state == State.NONE_PLAYED || state == State.EFFECT_PLAYED) {
				state = State.DEV_PLAYED;
				return true;
			} else{
				return false;
			}
		} else if (type == Card.CardType.INSTANT_EFFECT || type == Card.CardType.LASTING_EFFECT) {
			if (state == State.NONE_PLAYED) {
				state = State.EFFECT_PLAYED;
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	public void discardHand(){
		foreach (Transform child in hand.transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < 5; i++) {
			GameObject card = Instantiate (Resources.Load (deck.DealCard (), typeof(GameObject))) as GameObject;
			card.transform.SetParent (hand.transform);
			card.transform.localScale = new Vector3 (1, 1, 1);
		}

		endTurn ();
			
	}
	private void drawCards(){
		while (hand.transform.childCount < 6) {
            if (deck == null)
                    Debug.Log("deck null");
			GameObject card = Instantiate (Resources.Load (deck.DealCard(), typeof(GameObject))) as GameObject;
			card.transform.SetParent (hand.transform);
			card.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
	public bool endTurn(){
		Debug.Log ("End Turn");
		if (nextDay ()) {
			drawCards ();
			state = State.NONE_PLAYED;
		} else {
		}
		return false;
		/* 
		 * Advance day
		 * If day >= duration
		 * 		end sprint (restart for demo?)
		 * else
		 * 		display new day
		 * 		change state to none played
	   /**/
	}
	public void changeRoles(int allowed){
		RoleAllocation allocate = roleAllocation.GetComponent<RoleAllocation> ();
		allocate.allotment = allowed;
		allocate.init ();
	}
	// Use this for initialization
	void Start () {
		sprint = GetComponent<Sprint> ();
		defectBar.GetComponent<DefectBar> ().setMax (sprint.defectLimit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
