using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hand : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler {

	private GameObject dragItem = null;
	private GameObject canvas = null;
	public GameObject controller;
	private bool down = false;
	private bool inHand = false;
	private GameObject placeHolder;
	private bool dragging = false;

	private GameObject hoverZone = null;
	public void OnPointerDown(PointerEventData eventData){
		Debug.Log (name + " Pointer Down");
		down = true;
		if (dragItem != null) {
			startDrag ();
		}
	}
	public void setHoverZone(GameObject go){
		if (go != null && dragging) {
			if (dragItem.GetComponent<Card> ().valid (go.GetComponent<DropZone> ().type)) {
				hoverZone = go;
			}
		} else {
			hoverZone = null;
		}
	}
	private void startDrag(){
		placeHolder = new GameObject ();
		placeHolder.transform.SetParent (dragItem.transform.parent);
		placeHolder.transform.SetSiblingIndex (dragItem.transform.GetSiblingIndex ());
		LayoutElement le = placeHolder.AddComponent<LayoutElement> ();
		le.preferredWidth = dragItem.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = dragItem.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleHeight = 0;
		le.flexibleWidth = 0;
		dragItem.transform.SetParent (canvas.transform);
		dragItem.transform.localScale = new Vector3 (2, 2, 1);
		dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	private void endDrag(){
		if (dragItem != null && placeHolder != null) {
			dragItem.transform.localScale = new Vector3 (1, 1, 1);
			dragItem.transform.SetParent (placeHolder.transform.parent);
			dragItem.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
			dragItem.transform.position = placeHolder.transform.position;
			Destroy (placeHolder);
			dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}
	}
	public void OnPointerUp(PointerEventData eventData){
		Debug.Log (name + " Pointer Up");
		if (!inHand) {
			if (dragging) {
				stopDragging ();
			}
			dragItem = null;
		} else {
			endDrag ();
		}
		down = false;
		dragging = false;
	}
	private void startDragging(){
		dragging = true;
		placeHolder = new GameObject ();
		placeHolder.transform.SetParent (dragItem.transform.parent);
		placeHolder.transform.SetSiblingIndex (dragItem.transform.GetSiblingIndex ());
		LayoutElement le = placeHolder.AddComponent<LayoutElement> ();
		le.preferredWidth = dragItem.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = dragItem.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleHeight = 0;
		le.flexibleWidth = 0;
		dragItem.transform.SetParent (canvas.transform);
		dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	private void stopDragging(){
		dragging = false;
		if (dragItem != null && placeHolder != null) {
			dragItem.transform.localScale = new Vector3 (1, 1, 1);
			if (hoverZone != null) {
				DropZone zone = hoverZone.GetComponent<DropZone> ();
				if (zone.type == DropZone.Type.DISCARD) {
					zone.parentItem (dragItem);
					dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = false;
					controller.GetComponent<GameSystem> ().endTurn ();
				} else if (zone.type == DropZone.Type.PLAY) {
					if (dragItem.GetComponent<Card> ().Activate ()) {
						zone.parentItem (dragItem);
						dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = false;
					} else {
						dragItem.transform.SetParent (placeHolder.transform.parent);
						dragItem.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
						dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = true;
					}
				}
				else if(zone.type == DropZone.Type.LASTING_EFFECT){
					if (dragItem.GetComponent<Card> ().Activate ()) {
						zone.parentItem (dragItem);
						dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = true;
						dragItem.GetComponent<Draggable> ().enabled = true;
					} else {
						dragItem.transform.SetParent (placeHolder.transform.parent);
						dragItem.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
						dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = true;
					}
				}
			} else {
				dragItem.transform.SetParent (placeHolder.transform.parent);
				dragItem.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
				dragItem.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			}
			Destroy (placeHolder);
		}
	}
	public void OnPointerExit(PointerEventData eventData){
		Debug.Log (name + " Pointer Exit");
		inHand = false;
		if (down) {
			endDrag ();
			startDragging ();
		}
	}
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log (name + " Pointer Enter");
		inHand = true;
		if (dragging) {
			dragItem.GetComponent<Image> ().color = Color.clear;
			stopDragging ();
			dragItem.GetComponent<Image> ().color = Color.white;
			//startDrag ();
		}
	}
	public void setDragItem(GameObject obj){
		if(down)
			endDrag ();
		dragItem = obj;
		if(down)
			startDrag ();
	}
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (down) {
			if (dragItem != null) {
				//Debug.Log ("Drag " + dragItem.name);
				if (dragging) {
					dragItem.transform.position = new Vector3 (
						Input.mousePosition.x, Input.mousePosition.y - dragItem.GetComponent<RectTransform>().rect.height * canvas.GetComponent<Canvas>().scaleFactor * 1.5f, dragItem.transform.position.z);
				}
			}
		}
	}
}
