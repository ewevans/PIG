using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour {

	public int coders = 2;
	public int debuggers = 2;
	public int testers = 0;

	public int coderMod = 0;

	public int linesModifier = 0;
	public int defectModifier = 0;
	public int debugDefectModifier = 0;

	private bool skipCoding = false;

	private Sprint sprint;

	private enum State{
		NONE_PLAYED,
		EFFECT_PLAYED,
		DEV_PLAYED
	};
    public Deck deck = new Deck();
    public EventDeck eventDeck = new EventDeck();

	public GameObject codersText;
	public GameObject testersText;
	public GameObject debuggersText;
	public GameObject defectsDisplay;
	public GameObject linesNumber;
	public GameObject linesProgress;
	public GameObject daysText;
	public GameObject hand;
	public GameObject defectBar;
	public GameObject roleAllocation;
	public GameObject dayIndicator;
	public GameObject dayIndicatorText;

    
    public GameObject eventSlot;

	private State state = State.NONE_PLAYED;


	private string[] cards = {
		"Coding30",
		"Coding40",
		"Coding50",
		"Coding75-10",
		"Coding75-0"
	};
	public void linesPerCoder(int lines){
		int currentLines = sprint.updateLinesDone (Mathf.Max((lines + linesModifier) * (coders + coderMod), 0));
		linesNumber.GetComponent<Text> ().text = "" + currentLines;
		linesProgress.GetComponent<Image> ().fillAmount = (float)currentLines / (float)sprint.linesObjective;
	}
	public void flatLines(int lines){
		int currentLines = sprint.updateLinesDone (lines);
		linesNumber.GetComponent<Text> ().text = "" + currentLines;
		linesProgress.GetComponent<Image> ().fillAmount = (float)currentLines / (float)sprint.linesObjective;
	}
	public void defectsPerCoder(int defects){
		int currentDefects = sprint.updateDefects (Mathf.Max((defects + defectModifier) * (coders + coderMod), 0));
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public void flatDefects(int defects){
		int currentDefects = sprint.updateDefects (defects);
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public void flatBudget(int amount){
		sprint.budget += amount;
	}
	public void flatLinesObjective(int change){
		sprint.linesObjective += change;
		linesProgress.GetComponent<Image> ().fillAmount = (float)sprint.linesDone / (float)sprint.linesObjective;
	}
	public void flatDevelopers(int change){
		// ?
	}
	public void flatCoders(int change){
		// ?
	}
	public void flatDays(int change){
		sprint.updateSprintDuration (change);
	}
	public void changeDefectModifier(int mod){
		defectModifier += mod;
	}
	public void changeLinesModifier(int mod){
		linesModifier += mod;
	}
	public void defectsPerDebugger(int removed){
		int currentDefects = sprint.updateDefects (Mathf.Max((debugDefectModifier + removed) * debuggers, 0));
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public void loseCoding(){
		skipCoding = true;
		if (state == State.EFFECT_PLAYED) {
			state = State.DEV_PLAYED;
		}
	}
	public void loseEffect(){
		state = State.EFFECT_PLAYED;
		if (skipCoding) {
			state = State.DEV_PLAYED;
		}
	}
	public void loseRandomLasting(){
		//	pick one at random
		//	call its deactivate
		//	destroy it
	}
	public int percentDefects(double percent){
		int defectsRemoved = (int)((double)sprint.defects * percent);
		int currentDefects = sprint.updateDefects (-defectsRemoved);
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
		return defectsRemoved;
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
				if (skipCoding) {
					state = State.DEV_PLAYED;
				}
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	public void RoleAllocHudUpdate (){
		codersText.GetComponent<Text>().text = "x " + coders;
		testersText.GetComponent<Text> ().text = "x " + testers;
		debuggersText.GetComponent<Text> ().text = "x " + debuggers;
	}

	public void discardHand(){
		if (state == State.NONE_PLAYED) {
			foreach (Transform child in hand.transform) {
				GameObject.Destroy (child.gameObject);
			}

			for (int i = 0; i < 5; i++) {
				GameObject card = Instantiate (Resources.Load (deck.DealCard (), typeof(GameObject))) as GameObject;
				card.transform.SetParent (hand.transform);
				card.transform.localScale = new Vector3 (1, 1, 1);
			}

			endTurn ();
		}
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
	private void drawCardsStart(){
		while (hand.transform.childCount < 5) {
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
            startEvent(eventDeck.ChooseEvent());
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

    public void startEvent(string eventStarted)
    {
        //destroy previous event
        foreach (Transform child in eventSlot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Make new event object from parameter string name
        GameObject eventObj = Instantiate(Resources.Load("Events/" + eventStarted, typeof(GameObject))) as GameObject;
        eventObj.transform.SetParent(eventSlot.transform);
        eventObj.transform.localScale = new Vector3(1, 1, 1);
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
        eventSlot.GetComponent < GameObject> ();

		drawCardsStart ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
