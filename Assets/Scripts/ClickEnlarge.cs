using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEnlarge : MonoBehaviour, IPointerClickHandler {

	public int childIndex;
	public Transform oldParent;
	private bool enlarged = false;
	// Use this for initialization
	void Start () {
		childIndex = transform.GetSiblingIndex ();
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void OnPointerClick(PointerEventData eventData){
		Debug.Log ("Click");
		transform.localScale = new Vector3 (2, 2, 2);
		oldParent = transform.parent;
		transform.SetParent (transform.parent.parent);
		GetComponent<Image> ().raycastTarget = false;
		transform.SetSiblingIndex (transform.parent.childCount - 2);
		GameObject.Find ("ControlBlocker").GetComponent<ControlBlocker> ().block (gameObject);
	}
}
