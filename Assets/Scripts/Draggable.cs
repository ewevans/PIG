using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	private Vector2 offset;
	public Transform parentTo = null;
	public Transform potentialParent = null;
	public Transform oldParent;

	private bool reordering = true;
	private bool touchable = true;
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
		/*
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
		oldParent = transform.parent;
		potentialParent = parentTo;

		transform.SetParent (parentTo.parent);
*/
		offset.x = transform.position.x - eventData.position.x;
		offset.y = transform.position.y - eventData.position.y;

		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		parentTo = transform.parent;
		oldParent = transform.parent;
		potentialParent = parentTo;

		transform.SetParent (parentTo.parent);

	}
	public void OnDrag(PointerEventData eventData){
		transform.position = eventData.position + offset;
		/*
		if(reordering){
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
			if (newIndex != placeHolder.transform.GetSiblingIndex ()) {
				placeHolder.transform.SetSiblingIndex (newIndex);
				int parIndex = placeHolder.transform.GetSiblingIndex();
				eventData.
			}
		}
		/**/
	}
	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("EndDrag");
		transform.SetParent (parentTo);
		if(parentTo.GetComponent<DropZone>().type != DropZone.Type.DISCARD)
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Debug.Log (parentTo.name);
		/*
		transform.SetParent (parentTo);
		if (parentTo == oldParent) {
			transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
		}
		Destroy (placeHolder);
		reordering = true;
		DropZone landedZone = parentTo.gameObject.GetComponent<DropZone> ();
		Debug.Log (parentTo.gameObject.name);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (landedZone != null) {
			if (landedZone.type == DropZone.Type.LASTING_EFFECT) {
			} else if (landedZone.type == DropZone.Type.PLAY) {
				Debug.Log ("Thing1");
				GetComponent<CanvasGroup> ().blocksRaycasts = false;
				Card card = GetComponent<Card> ();
				if (card != null) {
					card.Activate ();
					/*
					GameObject linesNumber = GameObject.Find ("LinesNumber");
					GameObject defectsNumber = GameObject.Find ("DefectsNumber");

					int currentLines = int.Parse (linesNumber.GetComponent<Text> ().text);
					currentLines += card.activateEffect.linesPerCoder * 2;
					linesNumber.GetComponent<Text> ().text = "" + currentLines;

					int currentDefects = int.Parse (defectsNumber.GetComponent<Text> ().text);
					currentDefects += card.activateEffect.defectsPerCoder * 2;
					defectsNumber.GetComponent<Text> ().text = "" + currentDefects;
					/*
				}
			} else if (landedZone.type == DropZone.Type.DISCARD) {
				Debug.Log ("Thing2");
				GetComponent<CanvasGroup> ().blocksRaycasts = false;
			}
		} else {
			Debug.Log ("null");
		}
		/**/
	}
}
