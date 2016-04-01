using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public Card.CardType type;
	public int capacity = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("PointerEnter");

		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			if (card != null && card.type == type && transform.childCount != capacity) {
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				drag.Reordering (true);
				if (drag != null) {
					drag.potentialParent = transform;
				}
			}
		}
	}
	public void OnDrop(PointerEventData eventData){
		Debug.Log ("Card " + eventData.pointerDrag.name + " was dropped on " + name);
		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			if (card != null && card.type == type) {
				
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				if (drag != null) {
					drag.parentTo = drag.potentialParent;
				}
			}
		}
	}
	public void OnPointerExit(PointerEventData eventData){
		//Debug.Log ("PointerExit");
		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			if (card != null && card.type == type) {
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				drag.Reordering (false);
				if (drag != null && drag.potentialParent == transform) {
					drag.potentialParent = drag.parentTo;
				}
			}
		}
	}
}
