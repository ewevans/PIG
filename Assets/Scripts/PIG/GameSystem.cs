using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameSystem : MonoBehaviour {
	public int score = 0;
	public int coders;
	public int debuggers;
	public int testers;

	public int coderMod = 0;

	public int linesModifier = 0;
	public int defectModifier = 0;
	public int debugDefectModifier = 0;

	private bool skipCoding = false;
	private Dictionary<int, List<Event> > effectsToCancel = new Dictionary<int, List<Event> >();

	private Sprint sprint;
	public NewGame newgame;


	public enum Category
	{
		NONE,
		DATA,
		COMM,
		EXEC,
		DEFECT,
		REQ
	};

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
	public GameObject dialogBox;
	public GameObject dayIndicator;
	public GameObject dayIndicatorText;
	public GameObject budgetDisplay;
	public GameObject scoreText;
    
    public GameObject eventSlot;
	public GameObject lasting1;
	public GameObject lasting2;
	public GameObject summaryslot;

	public GameObject effectLight;
	public GameObject devLight;

	private State state = State.NONE_PLAYED;

	private int eventTriggerdAlloc = 0;


	private string[] cards = {
		"Coding30",
		"Coding40",
		"Coding50",
		"Coding75-10",
		"Coding75-0"
	};

	public void linesPerCoder(int lines){
		int currentLines = sprint.updateLinesDone (Mathf.Max((lines + linesModifier) * (coders + coderMod), 0));
		Debug.Log (currentLines + " Lines");
		Debug.Log (lines + " Lines attempted");
		refreshLines ();
	}
	public void flatLines(int lines){
		int currentLines = sprint.updateLinesDone (lines);
		refreshLines ();
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
		budgetDisplay.GetComponent<Text> ().text = "$" + sprint.budget;
	}
	public void flatLinesObjective(int change){
		sprint.linesObjective += change;
		linesProgress.GetComponent<Image> ().fillAmount = (float)sprint.linesDone / (float)sprint.linesObjective;
		refreshLines ();
	}
	public void eventDevelopers(int change){
		if (change < 0) {
			int numToRemove = -change;
			while (coders > 0 && numToRemove > 0) {
				coders -= 1;
				numToRemove -= 1;
			}
			while (debuggers > 0 && numToRemove > 0) {
				debuggers -= 1;
				numToRemove -= 1;
			}
			while (testers > 0 && numToRemove > 0) {
				testers -= 1;
				numToRemove -= 1;
			}
		} else
			coders += change;
		eventTriggerdAlloc += change;
	}
	public void flatDevelopers(int change){
		if (change < 0) {
			int numToRemove = -change;
			while (coders > 0 && numToRemove > 0) {
				coders -= 1;
				numToRemove -= 1;
			}
			while (debuggers > 0 && numToRemove > 0) {
				debuggers -= 1;
				numToRemove -= 1;
			}
			while (testers > 0 && numToRemove > 0) {
				testers -= 1;
				numToRemove -= 1;
			}
		} else
			coders += change;
		if (change != 0) {
			changeRoles (Mathf.Abs (change));
		}
	}
	public void flatCoders(int change){
		coderMod += change;
		if (coders < 0)
			coders = 0;
	}
	public void flatDays(int change){
		sprint.updateSprintDuration (change);daysText.GetComponent<Text> ().text = "Day " + sprint.currentDay + " of " + sprint.sprintDuration;
		dayIndicatorText.GetComponent<Text> ().text = "" + sprint.currentDay;
		float location = linesProgress.GetComponent<RectTransform> ().rect.width * ((float)sprint.currentDay - 1f) / (float)sprint.sprintDuration + linesProgress.GetComponent<RectTransform>().rect.position.x;
		dayIndicator.transform.localPosition = new Vector3 (location, dayIndicator.transform.localPosition.y, dayIndicator.transform.localPosition.z);
	}
	public void changeDefectModifier(int mod){
		defectModifier += mod;
	}
	public void changeLinesModifier(int mod){
		linesModifier += mod;
	}
	public void defectsPerDebugger(int removed){
		int currentDefects = sprint.updateDefects (Mathf.Min((debugDefectModifier + removed) * debuggers, 0));
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	private void setState(State newState){
		state = newState;
		switch (state) {
		case State.NONE_PLAYED:
			effectLight.GetComponent<Image> ().color = Color.green;
			devLight.GetComponent<Image> ().color = Color.green;
			break;
		case State.EFFECT_PLAYED:
			effectLight.GetComponent<Image> ().color = Color.red;
			devLight.GetComponent<Image> ().color = Color.green;
			break;
		case State.DEV_PLAYED:
			effectLight.GetComponent<Image> ().color = Color.red;
			devLight.GetComponent<Image> ().color = Color.red;
			break;
		default:
			effectLight.GetComponent<Image> ().color = Color.green;
			devLight.GetComponent<Image> ().color = Color.green;
			break;
		}
		refreshLines ();
	}
	public void loseCoding(){
		skipCoding = true;
		devLight.GetComponent<Image> ().color = Color.red;
		if (state == State.EFFECT_PLAYED) {
			setState (State.DEV_PLAYED);
		}
	}
	public void loseEffect(){
		setState (State.EFFECT_PLAYED);
		if (skipCoding) {
			setState (State.DEV_PLAYED);
		}
	}
	public void loseRandomLasting(){
		//	pick one at random
		//	call its deactivate
		//	destroy it

		GameObject lastingSlot1 = GameObject.Find ("Lasting1");
		GameObject lastingSlot2 = GameObject.Find ("Lasting2");
		int which = (Random.Range (1, 2));
		switch (which) {
		case 1:
			if (lastingSlot1.transform.childCount > 0) {
				GameObject card = lastingSlot1.transform.GetChild (0).gameObject;
				card.GetComponent<Card> ().Deactivate ();
				Destroy (card);
			} else if (lastingSlot2.transform.childCount > 0) {
				GameObject card = lastingSlot2.transform.GetChild (0).gameObject;
				card.GetComponent<Card> ().Deactivate ();
				Destroy (card);
			}
			break;
		case 2:
			if (lastingSlot2.transform.childCount > 0) {
				GameObject card = lastingSlot2.transform.GetChild (0).gameObject;
				card.GetComponent<Card> ().Deactivate ();
				Destroy (card);
			} else if (lastingSlot1.transform.childCount > 0) {
				GameObject card = lastingSlot1.transform.GetChild (0).gameObject;
				card.GetComponent<Card> ().Deactivate ();
				Destroy (card);
			}
			break;
		default:
			break;
		}

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
		float location = linesProgress.GetComponent<RectTransform> ().rect.width * ((float)currentDay - 1f) / (float)sprint.sprintDuration + linesProgress.GetComponent<RectTransform>().rect.position.x;
		dayIndicator.transform.localPosition = new Vector3 (location, dayIndicator.transform.localPosition.y, dayIndicator.transform.localPosition.z);
		flatBudget (-100 * (coders + testers + debuggers));
		return currentDay <= sprint.sprintDuration;
	}
	public bool playCard(Card.CardType type){
		if (type == Card.CardType.DEVELOPMENT) {
			if (!skipCoding && (state == State.NONE_PLAYED || state == State.EFFECT_PLAYED)) {
				setState (State.DEV_PLAYED);
				return true;

			} else{
				return false;
			}
		} else if (type == Card.CardType.INSTANT_EFFECT || type == Card.CardType.LASTING_EFFECT) {
			if (state == State.NONE_PLAYED) {
				setState (State.EFFECT_PLAYED);
				if (skipCoding) {
					setState (State.DEV_PLAYED);
				}
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	//Role Allocation HUD Display Update
	public void RoleAllocHudUpdate (){
		codersText.GetComponent<Text>().text = "x " + coders;
		testersText.GetComponent<Text> ().text = "x " + testers;
		debuggersText.GetComponent<Text> ().text = "x " + debuggers;
	}

	//Discarding Entire Player Hand
	public void discardHand(){
		//updateDialogBox ("This is the Title!", "This is my body. I'm testing the space used. This is my body. I'm testing the space used. This is my body. I'm testing the space used. This is my body. I'm testing the space used. This is my body. I'm testing the space used.");
		if (state == State.NONE_PLAYED) {
			//Deleting all existing cards in hand
			foreach (Transform child in hand.transform) {
				GameObject.Destroy (child.gameObject);
			}
			//Create new hand of cards
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
		TurnUpdate ();

		if (sprint.currentDay >= sprint.sprintDuration) {
		//if (sprint.currentDay == 4) {
			//Debug.Log ("Budget = ", sprint.budget);
//			PlayerPrefs.SetInt ("Lines Done", sprint.linesDone);
//			PlayerPrefs.SetInt ("Lines Objective", sprint.linesObjective);
//			PlayerPrefs.SetInt ("Defects Generated", sprint.defects);
//			PlayerPrefs.SetInt ("Defects Max", sprint.defectLimit);
//			PlayerPrefs.SetInt ("Budget", sprint.budget);
			int linesDone = sprint.linesDone;
			bool foundInData = false;
			PersistantData data = PersistantData.persistantData;
			for (int index = 0; index < sprint.tasks.Length; ++index) {
				if (linesDone == 0) {
					
				} else if (linesDone > sprint.tasks [index].lines) {
					sprint.tasks [index].linesDone = sprint.tasks [index].lines;
					linesDone -= sprint.tasks [index].lines;
				} else {
					sprint.tasks [index].linesDone = linesDone;
					linesDone = 0;
				}
				Debug.Log ("Len = " + data.projects [data.projectIndex].tasks.Length);
				foundInData = false;
				for (int pIndex = 0; !foundInData && pIndex < data.projects [data.projectIndex].tasks.Length; ++pIndex) {
					Debug.Log (" pIndex = " + pIndex + "              index = " + index);
					if (data.projects [data.projectIndex].tasks [pIndex].name == sprint.tasks [index].name) {
						data.projects [data.projectIndex].tasks [pIndex].linesDone = sprint.tasks [index].linesDone;
						foundInData = true;
					}
				}
			}
			GameStats.gamestats.linesdone = sprint.linesDone;
			GameStats.gamestats.linesobjective = sprint.linesObjective;
			GameStats.gamestats.defects = sprint.defects;
			GameStats.gamestats.defectsmax = sprint.defectLimit;
			GameStats.gamestats.budget = sprint.budget;
			GameStats.gamestats.score = score;
			RankManager rm = new RankManager();
			SceneManager.LoadScene ("SprintSummary");

			GameStats.gamestats.Save ();
			rm.initiateRanks ();
			GameStats.gamestats.rank = rm.currentrank;
			GameStats.gamestats.nextrank = rm.currentnextrank;
			GameStats.gamestats.nextrankscore = rm.currentnextrankscore;
		}
	
		skipCoding = false;

		if (nextDay ()) {
			eventTriggerdAlloc = 0;
			if (effectsToCancel.ContainsKey(sprint.currentDay) && effectsToCancel [sprint.currentDay] != null) {
				foreach (Event canceller in effectsToCancel[sprint.currentDay]) {
					canceller.Deactivate ();
				}
			}
			drawCards ();
			setState (State.NONE_PLAYED);

			//clean up any existing event dialog box
			foreach (Transform child in eventSlot.transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			//Randomize probability of an event occuring
			if (Random.value > .90 && sprint.currentDay != sprint.sprintDuration) {
				startEvent (eventDeck.ChooseEvent ());
			}
			if (eventTriggerdAlloc != 0) {
				changeRoles(Mathf.Abs(eventTriggerdAlloc));
			}
		} 

		else {
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
		bool activate = true;
        //destroy previous event
        foreach (Transform child in eventSlot.transform) {
            GameObject.Destroy(child.gameObject);
        }

        //Make new event object from parameter string name
        GameObject eventObj = Instantiate(Resources.Load("Events/" + eventStarted, typeof(GameObject))) as GameObject;
        eventObj.transform.SetParent(eventSlot.transform);
        eventObj.transform.localScale = new Vector3(0, 0, 0);

		//if event category doesn't match either lasting pile then activate
		if (lasting1.transform.childCount > 0) {
			if (lasting1.GetComponentInChildren<Card>().category == eventObj.GetComponent<Event>().category) {
				Debug.Log (lasting1.GetComponentInChildren<Card> ().name + " blocked " + eventObj.GetComponent<Event> ().name);
				activate = false;
			}
		}
		if (lasting2.transform.childCount > 0) {
			if (lasting2.GetComponentInChildren<Card>().category == eventObj.GetComponent<Event>().category) {
				Debug.Log (lasting2.GetComponentInChildren<Card> ().name + " blocked " + eventObj.GetComponent<Event> ().name);
				activate = false;
			}
		}

		if (activate) {
			eventObj.GetComponent<Event> ().Activate ();
			eventObj.transform.localScale = new Vector3(1, 1, 1);
			Event ev = eventObj.GetComponent<Event> ();
			if (ev.eventDuration > 0) {
				if (!effectsToCancel.ContainsKey(sprint.currentDay + ev.eventDuration) || effectsToCancel [sprint.currentDay + ev.eventDuration] == null) {
					effectsToCancel [sprint.currentDay + ev.eventDuration] = new List<Event> ();
				}
				effectsToCancel [sprint.currentDay + ev.eventDuration].Add (ev);
			}
		}
		refreshLines ();
    }

	public void changeRoles(int allowed){
		RoleAllocation allocate = roleAllocation.GetComponent<RoleAllocation> ();
		allocate.allotment = allowed;
		allocate.init ();
	}

	public void updateDialogBox(string title, string body){
		DialogBox dialog = dialogBox.GetComponent<DialogBox> ();
		dialog.init(title, body);
	}

	public void TurnUpdate(){
		
		Turn turn = new Turn();
		turn.turnDay = sprint.currentDay;
		turn.turnBudget = sprint.budget;
		turn.turnDefects = sprint.defects;
		turn.turnLinesComnpleted = sprint.linesDone;
		sprint.TurnList.Add (turn);

		print ("TurnDay =" + turn.turnDay);
		print ("TurnBudget =" + turn.turnBudget);
		print ("TurnDefects =" + turn.turnDefects);
		print ("List Count " + sprint.TurnList.Count);
	}

	// Score Update Method checks for type to determine value - DE
	public void ScoreUpdate(int cardscore, Card.CardType type){
		if (type == Card.CardType.DEVELOPMENT) {
			score += cardscore;
			}

		else if (type == Card.CardType.INSTANT_EFFECT) {
			score += 50;
			}

		else if (type == Card.CardType.LASTING_EFFECT) {
			score += 150;
			}

		DisplayScore (score);
	}
		
	// Display Score to SCORE HUD - DE
	public void DisplayScore(int score){
		scoreText.GetComponent<Text> ().text = score.ToString ();
		}

	// Use this for initialization
	void Start () {
		sprint = GetComponent<Sprint> ();
		defectBar.GetComponent<DefectBar> ().setMax (sprint.defectLimit);
        eventSlot.GetComponent < GameObject> ();

		budgetDisplay.GetComponent<Text> ().text = "$" + sprint.budget;

		drawCardsStart ();
		RoleAllocHudUpdate ();
		refreshLines ();

	}
	void refreshLines(){
		linesNumber.GetComponent<Text> ().text = "" + sprint.linesDone + " / " + sprint.linesObjective;
		linesProgress.GetComponent<Image> ().fillAmount = (float)sprint.linesDone / (float)sprint.linesObjective;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Hide Summary Window until final day of Sprint is completed - DE
//		if (sprint.currentDay != sprint.sprintDuration + 1) {
//			//if (sprint.currentDay != 4) {
//			summaryslot.SetActive (false);
//		} 
//		else {
//			summaryslot.SetActive (true);
//			GameObject eventObj = Instantiate (Resources.Load ("Events/SummaryWindow")) as GameObject;
//			eventObj.transform.SetParent (summaryslot.transform);
//			eventObj.transform.localScale = new Vector3 (1, 1, 1);
//		}
	}
}
