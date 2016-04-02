using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour, IPointerClickHandler {

	GameObject hand = null;
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
				GameObject card = (GameObject)Instantiate (Resources.Load ("Coding30"));
				card.transform.SetParent (hand.transform);
			}
		}
	}
}
