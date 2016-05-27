using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

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
	public enum CardType{
		LASTING_EFFECT,
		INSTANT_EFFECT,
		DEVELOPMENT,
		ANY
	};

	private GameObject gameSystem;

	public CardType type = CardType.ANY;
	public string title;
	public string description = null;
	public double probability = 0.0;
	public Effect activateEffect = null;
	public Effect deactivateEffect = null;
	void Start(){
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

		gameSystem = GameObject.Find ("GameSystem");
	}
	public bool valid(DropZone.Type dztype){
		if (type == CardType.LASTING_EFFECT) {
			return dztype == DropZone.Type.LASTING_EFFECT || dztype == DropZone.Type.DISCARD;
		} else if (type == CardType.INSTANT_EFFECT) {
			return dztype == DropZone.Type.PLAY || dztype == DropZone.Type.DISCARD;
		} else if (type == CardType.DEVELOPMENT) {
			return dztype == DropZone.Type.PLAY || dztype == DropZone.Type.DISCARD;
		} else if (type == CardType.ANY) {
			return true;
		}
		return false;
	}
	public bool Activate(){
		Debug.Log ("Activating card " + title);
		GameSystem system = gameSystem.GetComponent<GameSystem> ();
		if (system.playCard (type)) {
			system.linesPerCoder (activateEffect.linesPerCoder);
			system.defectsPerCoder (activateEffect.defectsPerCoder);
			if (activateEffect.roleChange) {
				system.changeRoles (1);
			}
			return true;
		}
		return false;
	}
	public void Deactivate(){
		Debug.Log ("Deactivating card " + title);
	}

	public class Effect {
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
	}
}
