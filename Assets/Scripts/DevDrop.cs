using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DevDrop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public enum Type{
		CODER,
		DEBUGGER,
		TESTER
	}
	public Type type;
	public int capacity = 6;
	private RoleAllocation RA;

	// Use this for initialization
	void Start () {
		RA = transform.parent.GetComponent<RoleAllocation> ();
	}

	// Update is called once per frame
	void Update () {

	}
	public void parentItem(GameObject go){
		//if (transform.childCount > 0)
			//Destroy (transform.GetChild (0).gameObject);
		go.transform.SetParent (transform);
	}
	public void OnPointerEnter(PointerEventData eventData){
	}
	public void OnDrop(PointerEventData eventData){

		Debug.Log ("Item " + eventData.pointerDrag.name + " was dropped on " + name);
		if (eventData.pointerDrag != null) {
			DevDrag drag = eventData.pointerDrag.GetComponent<DevDrag> ();
			if (drag != null && RA.allowDrop(this)) {
				drag.parentTo = transform;
			}
		}
		/**/
	}
	public void OnPointerExit(PointerEventData eventData){
		Debug.Log (transform.childCount);
	}
}
