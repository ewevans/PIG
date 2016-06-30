using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEnlarge : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void OnPointerClick(PointerEventData eventData){
		Debug.Log ("Click");
		transform.localScale = new Vector3 (2, 2, 2);
		GameObject.Find ("ControlBlocker").GetComponent<ControlBlocker> ().block (gameObject);
	}
}
