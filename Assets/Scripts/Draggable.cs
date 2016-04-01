using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	private Vector2 offset;
	public Transform parentTo = null;
	public Transform potentialParent = null;

	private bool reordering = true;
	public void Reordering(bool r){
		reordering = r;
	}

	public GameObject placeHolder = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log ("BeginDrag");
		placeHolder = new GameObject ();
		placeHolder.transform.SetParent (transform.parent);
		LayoutElement le = placeHolder.AddComponent<LayoutElement> ();
		le.preferredWidth = GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleHeight = 0;
		le.flexibleWidth = 0;

		placeHolder.transform.SetSiblingIndex (transform.GetSiblingIndex ());

		offset.x = transform.position.x - eventData.position.x;
		offset.y = transform.position.y - eventData.position.y;

		parentTo = transform.parent;
		potentialParent = parentTo;
		transform.SetParent (parentTo.parent);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;

	}
	public void OnDrag(PointerEventData eventData){
		transform.position = eventData.position + offset;
		if (reordering) {
			if(placeHolder.transform.parent != potentialParent){
				placeHolder.transform.SetParent (potentialParent);
			}
			int newIndex = potentialParent.childCount;
			
			for (int index = 0; index < potentialParent.childCount; ++index) {
				if (transform.position.x < potentialParent.GetChild (index).position.x) {
					newIndex = index;
					if (placeHolder.transform.GetSiblingIndex () < newIndex) {
						--newIndex;
					}
					break;
				}
			}
			placeHolder.transform.SetSiblingIndex (newIndex);
		}
	}
	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("EndDrag");
		transform.SetParent (parentTo);
		transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Destroy (placeHolder);
	}
}
