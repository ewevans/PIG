using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlBlocker : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	public GameObject blockingObject;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void block(GameObject blocker){
		blockingObject = blocker;
		transform.localScale = new Vector3 (1, 1, 1);
	}
	public void OnPointerClick(PointerEventData eventData){
		blockingObject.transform.localScale = new Vector3 (1, 1, 1);
		transform.localScale = new Vector3 (0, 0, 0);
	}
}
