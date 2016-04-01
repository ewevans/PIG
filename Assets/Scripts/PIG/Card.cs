using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public enum CardType{
		LASTING_EFFECT,
		INSTANT_EFFECT,
		DEVELOPMENT,
		ANY
	};
	public CardType type = CardType.ANY;
	public string title;
	public string description = null;
	public double probability = 0.0;
	public void Activate(){
		Debug.Log ("Activating card " + title);
	}
	public void Deactivate(){
		Debug.Log ("Deactivating card " + title);
	}
}
