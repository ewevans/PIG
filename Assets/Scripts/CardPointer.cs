using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private Hand hand;
	public void OnPointerEnter(PointerEventData eventData){
		if(transform.parent.GetComponent<Hand>() != null)
			hand.setDragItem (gameObject);
	}
	public void OnPointerExit(PointerEventData eventData){
		//hand.setDragItem (null);
	}
	// Use this for initialization
	void Start () {
		hand = GameObject.Find ("Hand").GetComponent<Hand> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
