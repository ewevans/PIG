using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlBlocker : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	public GameObject blockingObject;
	public GameObject dimmer;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void block(GameObject blocker){
		blockingObject = blocker;
		transform.localScale = new Vector3 (1, 1, 1);
		dimmer.transform.localScale = new Vector3 (1, 1, 1);
	}
	public void OnPointerClick(PointerEventData eventData){
		blockingObject.transform.SetParent (blockingObject.GetComponent<ClickEnlarge> ().oldParent);
		blockingObject.transform.SetSiblingIndex (blockingObject.GetComponent<ClickEnlarge> ().childIndex);
		blockingObject.GetComponent<Image> ().raycastTarget = true;
		blockingObject.transform.localScale = new Vector3 (1, 1, 1);
		transform.localScale = new Vector3 (0, 0, 0);
		dimmer.transform.localScale = new Vector3 (0, 0, 0);
	}
}
