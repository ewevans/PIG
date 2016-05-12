using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private Hand hand;
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log (name + " Pointer Enter");
		hand.setDragItem (gameObject);
	}
	public void OnPointerExit(PointerEventData eventData){
		Debug.Log (name + " Pointer Exit");
		//hand.setDragItem (null);
	}
	// Use this for initialization
	void Start () {
		hand = GameObject.Find ("Hand").GetComponent<Hand> ();
		Debug.Log(GameObject.Find("Hand").name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
