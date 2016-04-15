using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour, IPointerClickHandler {

	GameObject hand = null;
	private string[] cards = {
		"Coding30",
		"Coding40",
		"Coding50",
		"Coding75-10",
		"Coding75-0"
	};
	// Use this for initialization
	void Start () {
		hand = GameObject.Find ("Hand");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerClick(PointerEventData eventData){
		Debug.Log ("Draw!");
		if (hand != null) {
			if (hand.transform.childCount < 5) {
				GameObject card = (GameObject)Instantiate (Resources.Load (cards[Random.Range(0,4)]));
				card.transform.SetParent (hand.transform);
				card.transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}
}
