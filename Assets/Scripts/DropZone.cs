﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public enum Type{
		LASTING_EFFECT,
		PLAY,
		DISCARD,
		NONE
	}
	public Type type;
	public int capacity = 1;

	private Hand hand;

	// Use this for initialization
	void Start () {
		hand = GameObject.Find ("Hand").GetComponent<Hand>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void parentItem(GameObject go){
		if (transform.childCount > 0)
			Destroy (transform.GetChild (0).gameObject);
		go.transform.SetParent (transform);
	}
	public void OnPointerEnter(PointerEventData eventData){
		hand.setHoverZone (gameObject);
		//Debug.Log ("PointerEnter");
		/*
		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			//Debug.Log (card.valid (type));
			if (card != null) {
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				if (drag != null && transform == drag.oldParent) {
					drag.Reordering (true);
				}
			}
		}
		/**/
	}
	public void OnDrop(PointerEventData eventData){
		
		Debug.Log ("Card " + eventData.pointerDrag.name + " was dropped on " + name);
		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			if (card != null) {
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				if (drag != null && card.valid (type)) {
					if (type == Type.DISCARD || type == Type.PLAY || type == Type.LASTING_EFFECT) {
						if (transform.childCount > 1) {
							Destroy (transform.GetChild (0).gameObject);
						}
					}
					drag.parentTo = transform;
				}
			}
		}

		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			if (card != null && card.valid(type)) {
				
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				if (drag != null) {
					drag.parentTo = transform;
				}
			}
		}
/**/
	}
	public void OnPointerExit(PointerEventData eventData){
		hand.setHoverZone (null);
		Debug.Log (transform.childCount);
		//Debug.Log ("PointerExit");
		/*
		if (eventData.pointerDrag != null) {
			Card card = eventData.pointerDrag.GetComponent<Card> ();
			if (card == null)
				//Debug.Log ("null");
			if (card != null) {
				Draggable drag = eventData.pointerDrag.GetComponent<Draggable> ();
				if (drag != null)
					//Debug.Log ("Not null");
					drag.Reordering (false);
			}
		}
		*/
	}
}
