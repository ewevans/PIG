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


	private bool isTutorial(){
		return PersistantData.persistantData.projectIndex == 0 && PersistantData.persistantData.projects[PersistantData.persistantData.projectIndex].sprintsDone == 0;
	}
	private void tutorialMessages(){
		
	}
	public enum Category
	{
		NONE,
		DATA,
		COMM,
		EXEC,
		DEFECT,
		REQ
	};
	public bool canDevelop(){
		return sprint.defects < sprint.defectLimit;
	}

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
	public GameObject NoEventText;

	public GameObject effectLight;
	public GameObject devLight;

	public GameObject dailySpend;

	public GameObject earlyCompletion;

	private State state = State.NONE_PLAYED;

	private int eventTriggerdAlloc = 0;

	private bool dialogStandup = false;

	private string[] cards = {
		"Coding30",
		"Coding40",
		"Coding50",
		"Coding75-10",
		"Coding75-0"
	};

	private bool endEarlyOffered = false;
	public void showEarlyBox(){
		earlyCompletion.transform.localScale = new Vector3 (1, 1, 1);
	}
	public void autoPlaySelected(){
		flatDefects (sprint.currentDay - sprint.sprintDuration);
		sprint.currentDay = sprint.sprintDuration;
		endTurn ();
	}
	public void manualPlaySelected(){
		//	nothing special happens?
	}
	public void doneEarlyCheck(){
		if (sprint.linesDone >= sprint.linesObjective && !endEarlyOffered) {
			sprint.linesDone = sprint.linesObjective;
			endEarlyOffered = true;
			showEarlyBox ();
		}
		refreshLines ();
	}
	public void linesPerCoder(int lines){
		int currentLines = sprint.updateLinesDone (Mathf.Max((lines + linesModifier) * (coders + coderMod), 0));
		Debug.Log (currentLines + " Lines");
		Debug.Log (lines + " Lines attempted");
		doneEarlyCheck ();
	}
	public void flatLines(int lines){
		int currentLines = sprint.updateLinesDone (lines);
		doneEarlyCheck ();
	}
	public void defectsPerCoder(int defects){
		int currentDefects = sprint.updateDefects (Mathf.Max((defects + defectModifier) * (coders + coderMod), 0));
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects + "/" + sprint.defectLimit;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public void flatDefects(int defects){
		int currentDefects = sprint.updateDefects (defects);
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects + "/" + sprint.defectLimit;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
	}
	public void flatBudget(int amount){
		sprint.budget += amount;
		budgetDisplay.GetComponent<Text> ().text = "$" + sprint.budget;
	}
	public void flatLinesObjective(int change){
		sprint.updateLinesObjective (change);
		linesProgress.GetComponent<Image> ().fillAmount = (float)sprint.linesDone / (float)sprint.linesObjective;
		doneEarlyCheck ();
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
	public void flatPermCoders(int change){
		coders += change;
		if (coders < 0)
			coders = 0;
		RoleAllocHudUpdate ();
	}
	public void flatPermDebuggers(int change){
		debuggers += change; //coderMod += change;
		if (debuggers < 0)
			debuggers = 0;
		RoleAllocHudUpdate ();
	}
	public void flatPermTesters(int change){
		testers += change;//coderMod += change;
		if (testers < 0)
			testers = 0;
		RoleAllocHudUpdate ();
	}
	public void flatDays(int change){
		sprint.updateSprintDuration (change);daysText.GetComponent<Text> ().text = "Day " + sprint.currentDay + " of " + sprint.sprintDuration;
		dayIndicatorText.GetComponent<Text> ().text = "" + sprint.currentDay;
		//ethan testing if day indictator off by 1 day
		//float location = linesProgress.GetComponent<RectTransform> ().rect.width * ((float)sprint.currentDay - 1f) / (float)sprint.sprintDuration + linesProgress.GetComponent<RectTransform>().rect.position.x;
		float location = linesProgress.GetComponent<RectTransform> ().rect.width * ((float)sprint.currentDay - 1f) / ((float)sprint.sprintDuration) + linesProgress.GetComponent<RectTransform>().rect.position.x;
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
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects + "/" + sprint.defectLimit;
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
		defectsDisplay.GetComponent<Text> ().text = "" + currentDefects + sprint.defectLimit;
		defectBar.GetComponent<DefectBar> ().reportDefects (currentDefects);
		//multiply by 5 to add 5 times as much LOC as Defects removed
		return defectsRemoved*5;
	}

	public bool nextDay(){
		int currentDay = sprint.updateCurrentDay (1);
		daysText.GetComponent<Text> ().text = "Day " + currentDay + " of " + sprint.sprintDuration;
		dayIndicatorText.GetComponent<Text> ().text = "" + currentDay;
		//ethan testing 1f to 0f
		float location = linesProgress.GetComponent<RectTransform> ().rect.width * ((float)currentDay - 1f) / ((float)sprint.sprintDuration) + linesProgress.GetComponent<RectTransform>().rect.position.x;
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
		updateSpend ();
	}

	//Discarding Entire Player Hand
	public void discardHand(){
		//updateDialogBox ("This is the Title!", "This is my body. I'm testing the space used. This is my body. I'm testing the space used. This is my body. I'm testing the space used. This is my body. I'm testing the space used. This is my body. I'm testing the space used.");
		if (state == State.NONE_PLAYED) {
			//Deleting all existing cards in hand
			foreach (Transform child in hand.transform) {
				
				GameObject.Destroy (child.gameObject);
			}

			//ETHAN: in order to be able to draw tester cards, removing below in future
			//need to fix bug that only draws 1 card half the time 
			//Create new hand of cards
			/*for (int i = 0; i < 5; i++) {
				GameObject card = Instantiate (Resources.Load (deck.DealCard (), typeof(GameObject))) as GameObject;
				card.transform.SetParent (hand.transform);
				card.transform.localScale = new Vector3 (1, 1, 1);
			}*/
			bool drewTester = false;
			for (int i = 0; i < 5; i++) {
				if (deck == null)
					Debug.Log("deck null");
				Debug.Log (testers);
				//If they haven't drawn a tester this turn AND they have testers AND it rolls a tester card
				if (!drewTester && testers > 0 && (Random.value < (.1f * testers))) {
					//Added 10% extra chance per tester
					GameObject card = Instantiate (Resources.Load (deck.DealTesterCard(), typeof(GameObject))) as GameObject;
					card.transform.SetParent (hand.transform);
					card.transform.localScale = new Vector3 (1, 1, 1);
					drewTester = true;
					Debug.Log ("tester drawn");
				}
				else {
					GameObject card = Instantiate (Resources.Load (deck.DealCard(), typeof(GameObject))) as GameObject;
					card.transform.SetParent (hand.transform);
					card.transform.localScale = new Vector3 (1, 1, 1);
				}
			}

			endTurn ();
		}
	}

	private void drawCards(){
		//if the player has drawn a tester this round
		bool drewTester = false;
		Debug.Log ("drew tester is " + drewTester.ToString());
		while (hand.transform.childCount < 6) {
            if (deck == null)
                    Debug.Log("deck null");
			Debug.Log (testers);
			//If they haven't drawn a tester this turn AND they have testers AND it rolls a tester card
			if (!drewTester && testers > 0 && (Random.value < (.1f * testers))) {
				//Added 10% extra chance per tester
				GameObject card = Instantiate (Resources.Load (deck.DealTesterCard(), typeof(GameObject))) as GameObject;
				card.transform.SetParent (hand.transform);
				card.transform.localScale = new Vector3 (1, 1, 1);
				drewTester = true;
				Debug.Log ("tester drawn");
			}
			else {
				GameObject card = Instantiate (Resources.Load (deck.DealCard(), typeof(GameObject))) as GameObject;
				card.transform.SetParent (hand.transform);
				card.transform.localScale = new Vector3 (1, 1, 1);
			}
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
			//Scoring Bonuses and Subtractions - DE
			if (sprint.linesDone >= sprint.linesObjective)
				score += 200;
			else if(sprint.linesDone < sprint.linesObjective)
					score -= 150;
				
			if (sprint.defects <= sprint.defectLimit)
				score += 200;
			else if (sprint.defects > sprint.defectLimit)
				score -= 150;
			
			if (sprint.budget >= 0)
				score += 200;
			else if (sprint.budget < 0)
				score -= 150;

			int linesDone = sprint.linesDone;
			bool foundInData = false;
			PersistantData data = PersistantData.persistantData;
			data.totalSprintTasks = sprint.tasks.Length;
			data.completedSprintTasks = 0;

			data.runningDefects += sprint.defects;
			data.remainingBudget += sprint.budget;

			//if all tasks were complete
			if (linesDone >= sprint.linesObjective) {
				data.completedSprintTasks += sprint.tasks.Length;

				for (int index = 0; index < sprint.tasks.Length; index++) {
					sprint.tasks [index].linesDone = sprint.tasks [index].lines;
					foundInData = false;
					for (int pIndex = 0; !foundInData && pIndex < data.projects [data.projectIndex].tasks.Count; ++pIndex) {
						Debug.Log (" pIndex = " + pIndex + "              index = " + index);
						if (data.projects [data.projectIndex].tasks [pIndex].name == sprint.tasks [index].name) {
							data.projects [data.projectIndex].tasks [pIndex].linesDone = sprint.tasks [index].linesDone;
							foundInData = true;
						}
					}
				}
			}
			else{
				
				for (int index = 0; index < sprint.tasks.Length; ++index) {
					if (linesDone == 0) {

						// made > into >= because the last task wasn't counted
					} else if (linesDone >= sprint.tasks [index].lines) {
						sprint.tasks [index].linesDone = sprint.tasks [index].lines;
						linesDone -= sprint.tasks [index].lines;
						data.completedSprintTasks++;
					} else {
						sprint.tasks [index].linesDone = linesDone;
						linesDone = 0;
						//removed this line, since it was adding task complete that wasnt
						//data.completedSprintTasks++;
					}
					Debug.Log ("Len = " + data.projects [data.projectIndex].tasks.Count);
					foundInData = false;
					for (int pIndex = 0; !foundInData && pIndex < data.projects [data.projectIndex].tasks.Count; ++pIndex) {
						Debug.Log (" pIndex = " + pIndex + "              index = " + index);
						if (data.projects [data.projectIndex].tasks [pIndex].name == sprint.tasks [index].name) {
							data.projects [data.projectIndex].tasks [pIndex].linesDone = sprint.tasks [index].linesDone;
							foundInData = true;
						}
					}
				}//end for loop

			}


		
			//Transfer of player game stats to persistent object for use in summary scene
			GameStats.gamestats.linesdone = sprint.linesDone;
			GameStats.gamestats.linesobjective = sprint.linesObjective;
			GameStats.gamestats.defects = sprint.defects;
			GameStats.gamestats.defectsmax = sprint.defectLimit;
			GameStats.gamestats.budget = sprint.budget;
			GameStats.gamestats.score = score;
			RankManager rm = new RankManager(); // Ranking instance
			SceneManager.LoadScene ("SprintSummary");

			GameStats.gamestats.Save ();

			//Establish player ranking - DE
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
			string[] tutMessage = TutorialMessages.getMessage(sprint.currentDay - 1);
			if (isTutorial () && tutMessage != null) {
				startEvent ("NoEvent");
				updateDialogBox (tutMessage [0], tutMessage [1]);
			} else {
				if (Random.value > .90 && sprint.currentDay != sprint.sprintDuration) {
					startEvent (eventDeck.ChooseEvent ());
				} else {//give them a daily stand up without event

					startEvent ("NoEvent");
				}
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
		//removed 8/18 to implement better dialog boxes for tutorial
		/*if (!dialogStandup){
			updateDialogBox ("Note on Daily Standups!",
				"     In Scrum methodology, a quick morning meeting takes place before the team gets to work. In this 10-15 " +
				"minute standing meeting, the Scrum Master asks the team what they did yesterday, what they're doing today, " +
				"and if they have any impediments concerning their work.");
			dialogStandup = true;
		}*/

		
		bool activate = true;
        //destroy previous event
        foreach (Transform child in eventSlot.transform) {
            GameObject.Destroy(child.gameObject);
        }
		NoEventText.GetComponent<Text> ().text = "";

        //Make new event object from parameter string name
        GameObject eventObj = Instantiate(Resources.Load("Events/" + eventStarted, typeof(GameObject))) as GameObject;
        eventObj.transform.SetParent(eventSlot.transform);
        eventObj.transform.localScale = new Vector3(0, 0, 0);

		if (eventStarted != "NoEvent") {
			//if event category doesn't match either lasting pile then activate
			if (lasting1.transform.childCount > 0) {
				if (lasting1.GetComponentInChildren<Card> ().category == eventObj.GetComponent<Event> ().category) {
					Debug.Log (lasting1.GetComponentInChildren<Card> ().name + " blocked " + eventObj.GetComponent<Event> ().name);
					activate = false;
				}
			}
			if (lasting2.transform.childCount > 0) {
				if (lasting2.GetComponentInChildren<Card> ().category == eventObj.GetComponent<Event> ().category) {
					Debug.Log (lasting2.GetComponentInChildren<Card> ().name + " blocked " + eventObj.GetComponent<Event> ().name);
					activate = false;
				}
			}

			if (activate) {
				eventObj.GetComponent<Event> ().Activate ();
				eventObj.transform.localScale = new Vector3 (1, 1, 1);
				Event ev = eventObj.GetComponent<Event> ();
				if (ev.eventDuration > 0) {
					if (!effectsToCancel.ContainsKey (sprint.currentDay + ev.eventDuration) || effectsToCancel [sprint.currentDay + ev.eventDuration] == null) {
						effectsToCancel [sprint.currentDay + ev.eventDuration] = new List<Event> ();
					}
					effectsToCancel [sprint.currentDay + ev.eventDuration].Add (ev);
				}
			}
			refreshLines ();
		} else {//NoEvent
			eventObj.transform.localScale = new Vector3 (1, 1, 1);
			//if first day of the sprint
			if (sprint.currentDay == 1) {
				if (Random.value > .5) {
					NoEventText.GetComponent<Text> ().text = "The team is ready to get started on this sprint's tasks!";
				} else {
					NoEventText.GetComponent<Text> ().text = "This first daily stand-up focused on possible impediments for the new tasks.";
				}
			}//if defects are in red
			else if (sprint.defects > sprint.defectLimit) {
				if (Random.value > .5) {
					NoEventText.GetComponent<Text> ().text = "The buggy state of the code is causing impediments to your team! Try hiring a tester to find better cards.";
				} else {
					NoEventText.GetComponent<Text> ().text = "The Product Owner complains the glaring defects in latest release.";
				}
			}//if the LOC Progression FAR BEHIND
			else if ((sprint.currentDay > 8) && (((float)sprint.linesDone / (float)(sprint.linesObjective - 400f)) < ((float)sprint.currentDay / (float)sprint.sprintDuration))) {
				NoEventText.GetComponent<Text> ().text = "LOC far behind";
				if (Random.value > .5) {
					NoEventText.GetComponent<Text> ().text = "After checking the burndown chart, you can see the team is VERY far behind.";
				} else {
					NoEventText.GetComponent<Text> ().text = "During the stand-up, the team discusses why progress is so badly behind.";
				}
			}//if the budget is below 0
			else if (sprint.budget < 0) {
				NoEventText.GetComponent<Text> ().text = "The Product Owner is upset with the budget situation. He hopes the extra money is worth it.";
			}//if LOC Prog behind
			else if ((sprint.currentDay > 4) && (((float)sprint.linesDone / (float)(sprint.linesObjective - 200f)) < ((float)sprint.currentDay / (float)sprint.sprintDuration))) {
				NoEventText.GetComponent<Text> ().text = "The team discusses the problems that have occurred in the last day and why progress has slowed.";
				if (Random.value > .5) {
					NoEventText.GetComponent<Text> ().text = "The team discusses the problems that have occurred in the last day and why progress has slowed.";
				} else {
					NoEventText.GetComponent<Text> ().text = "The burndown chart indicates that the team is falling behind.";
				}
			}//if defects are in yellow
			else if (sprint.defects > (sprint.defectLimit / 2)) {
				NoEventText.GetComponent<Text> ().text = "As a team, you all notice that the current state of the code is quite sloppy.";
			}//if team has more than 5 members
			else if (coders + testers + debuggers > 5) {
				NoEventText.GetComponent<Text> ().text = "The Product Owner is worried that your team is growing too large. Try to reassign some developers.";
			}//if budget is set to run out
			else if (sprint.budget < ((sprint.sprintDuration - sprint.currentDay) * 100 * (coders + testers + debuggers))) {
				NoEventText.GetComponent<Text> ().text = "By the math of the Product Owner, you're set to run out of money! Make cuts!";
			} else {
				float random = Random.value;
				if (random < .1) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n Always make sure that discussion in daily stand-ups are only concerned with the sprint.";
				} else if (random < .2) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n If a team member says a task is too big, remember to discuss creating smaller tasks in Retrospective. ";
				} else if (random < .3) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n Actually standing for the daily stand-up is recommended by many experts.";
				} else if (random < .4) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n To help keep the atmosphere light, some teams pass an item or ball when answering the big three questions.";
				} else if (random < .5) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n One main cause of failure of Scrum implementation is the team not correctly updating task status on the Scrum board.";
				} else if (random < .6) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n As a Scrum Master, ask the team how comfortable they are completing a task on time. The answer may lead to a possible impediment.";
				} else if (random < .7) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n Make sure the daily stand-up lasts no longer than 15 minutes. Otherwise, the point is moot.";
				} else if (random < .8) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n Leave problem-solving until after the stand-up. The purpose of the 15 minute meeting is organization.";
				} else if (random < .9) {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n Always discuss the team's definition of done concerning different tasks. This will prevent unfinished work.";
				} else {
					NoEventText.GetComponent<Text> ().text = "Note on Agile Scrum:\n As a Scrum Master, don't direct the team. The team is a self-organzing force.";
				}

			}



				


			//NoEventText.GetComponent<Text> ().text = "The Product Owner is upset with the current budget situation.";

		}
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
		defectsDisplay.GetComponent<Text> ().text = "0/" + sprint.defectLimit;
        eventSlot.GetComponent < GameObject> ();

		budgetDisplay.GetComponent<Text> ().text = "$" + sprint.budget;

		Debug.Log ("First day " + sprint.currentDay);
		if (isTutorial()) {
			string[] message = TutorialMessages.getMessage (0);
			if (message != null) {
				updateDialogBox (message [0], message [1]);
			}
		}
		drawCardsStart ();
		RoleAllocHudUpdate ();
		refreshLines ();
		startEvent ("NoEvent");

	}
	void updateSpend(){
		dailySpend.GetComponent<Text> ().text = "-$" + (100 * (coders + debuggers + testers)) + " / Day";
	}
	void refreshLines(){
		linesNumber.GetComponent<Text> ().text = "" + sprint.linesDone + " / " + sprint.linesObjective;
		linesProgress.GetComponent<Image> ().fillAmount = (float)sprint.linesDone / (float)sprint.linesObjective;
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
