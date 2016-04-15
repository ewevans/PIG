using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Reset : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	public GameObject gameSystemHolder;
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
	public void OnPointerClick(PointerEventData eventData){
		GameSystem system = gameSystemHolder.GetComponent<GameSystem> ();
		Sprint sprint = gameSystemHolder.GetComponent<Sprint> ();

		system.flatLines (-sprint.linesDone);
		system.flatDefects (-sprint.defects);
	}
}
