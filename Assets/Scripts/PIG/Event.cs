using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {
    public int eventDuration = 0;
    public int flatLines = 0;
    public int flatDefects = 0;
    public int flatBudget = 0;
    public int flatLinesObjective = 0;
    public int flatDevelopers = 0;
    public int flatCoders = 0;
    public int flatDays = 0;
    public int defectModifier = 0;
    public int linesModifier = 0;
    public int linesPerCoder = 0;
    public int defectsPerCoder = 0;
    public int defectsPerDebugger = 0;
    public bool loseCodingTurn = false;
    public bool loseEffectTurn = false;
    public bool roleChange = false;
    public bool loseRandomLastingEffect = false;
    public double percentDefects = 0.0;

    public enum Category
    {
        NONE,
        DATA,
        COMM,
        EXEC,
        DEFECT,
        REQ
    };

	public Category category = Category.NONE;
    public string title;
    public string description = null;
    public double probability = 0.0;
    public Effect activateEffect = null;
    public Effect deactivateEffect = null;

    private GameObject gameSystem;

    // Use this for initialization
	void Awake(){
		activateEffect = new Effect();
		activateEffect.flatLines = flatLines;
		activateEffect.flatDefects = flatDefects;
		activateEffect.flatBudget = flatBudget;
		activateEffect.flatLinesObjective = flatLinesObjective;
		activateEffect.flatDevelopers = flatDevelopers;
		activateEffect.flatCoders = flatCoders;
		activateEffect.flatDays = flatDays;
		activateEffect.defectModifier = defectModifier;
		activateEffect.linesModifier = linesModifier;
		activateEffect.linesPerCoder = linesPerCoder;
		activateEffect.defectsPerCoder = defectsPerCoder;
		activateEffect.defectsPerDebugger = defectsPerDebugger;
		activateEffect.loseCodingTurn = loseCodingTurn;
		activateEffect.loseEffectTurn = loseEffectTurn;
		activateEffect.roleChange = roleChange;
		activateEffect.loseRandomLastingEffect = loseRandomLastingEffect;
		activateEffect.percentDefects = percentDefects;

		gameSystem = GameObject.Find("GameSystem");
	}
    void Start () {
        activateEffect = new Effect();
        activateEffect.flatLines = flatLines;
        activateEffect.flatDefects = flatDefects;
        activateEffect.flatBudget = flatBudget;
        activateEffect.flatLinesObjective = flatLinesObjective;
        activateEffect.flatDevelopers = flatDevelopers;
        activateEffect.flatCoders = flatCoders;
        activateEffect.flatDays = flatDays;
        activateEffect.defectModifier = defectModifier;
        activateEffect.linesModifier = linesModifier;
        activateEffect.linesPerCoder = linesPerCoder;
        activateEffect.defectsPerCoder = defectsPerCoder;
        activateEffect.defectsPerDebugger = defectsPerDebugger;
        activateEffect.loseCodingTurn = loseCodingTurn;
        activateEffect.loseEffectTurn = loseEffectTurn;
        activateEffect.roleChange = roleChange;
        activateEffect.loseRandomLastingEffect = loseRandomLastingEffect;
        activateEffect.percentDefects = percentDefects;

        gameSystem = GameObject.Find("GameSystem");

    }

	public bool Activate(){
		Debug.Log ("Activating card " + title);
		gameSystem = GameObject.Find ("GameSystem");
		GameSystem system = gameSystem.GetComponent<GameSystem> ();
			system.linesPerCoder (activateEffect.linesPerCoder);
			system.defectsPerCoder (activateEffect.defectsPerCoder);
			system.flatLines (activateEffect.flatLines);
			system.flatDefects (activateEffect.flatDefects);
			system.flatLinesObjective (activateEffect.flatLinesObjective + system.percentDefects(activateEffect.percentDefects));
			system.flatDevelopers (activateEffect.flatDevelopers);
			system.flatCoders (activateEffect.flatCoders);
			system.flatDays (activateEffect.flatDays);
			system.changeDefectModifier (activateEffect.defectModifier);
			system.changeLinesModifier (activateEffect.linesModifier);
			system.flatBudget (activateEffect.flatBudget);
			system.defectsPerDebugger (activateEffect.defectsPerDebugger);
			if (activateEffect.roleChange) {
				system.changeRoles (1);
			}
		if (activateEffect.loseEffectTurn) {
			system.loseEffect ();
		}
		if (activateEffect.loseCodingTurn) {
			system.loseCoding ();
		}
			return true;
		return false;
	}
	public void Deactivate(){
		GameSystem system = gameSystem.GetComponent<GameSystem> ();
		Debug.Log ("Deactivating card " + title);
		system.flatDevelopers (deactivateEffect.flatDevelopers);
		system.flatCoders (deactivateEffect.flatCoders);
		system.changeDefectModifier (deactivateEffect.defectModifier);
		system.changeLinesModifier (deactivateEffect.linesModifier);
	}

    // Update is called once per frame
    void Update () {
	
	}
}
