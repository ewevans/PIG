using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DevDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	private Vector2 offset;
	public Transform parentTo = null;
	public Transform potentialParent = null;
	public Transform oldParent;
	public GameObject roleAllocate;
	private bool reordering;
	public void Reordering(bool r){
		reordering = r;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		roleAllocate = GameObject.FindWithTag ("Roles");

	}

	// Update is called once per frame
	void Update () {

	}
	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log ("BeginDrag");

		offset.x = transform.position.x - eventData.position.x;
		offset.y = transform.position.y - eventData.position.y;

		parentTo = transform.parent;
		oldParent = transform.parent;
		potentialParent = parentTo;
		transform.SetParent (parentTo.parent);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;

	}
	public void OnDrag(PointerEventData eventData){
		transform.position = eventData.position + offset;
	}
	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("EndDrag");
		transform.SetParent (parentTo);
		reordering = true;
		DropZone landedZone = parentTo.gameObject.GetComponent<DropZone> ();
		Debug.Log (parentTo.gameObject.name);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Debug.Log ("Parent Children: " + parentTo.childCount);
		for (int index = 0; index < parentTo.childCount; ++index) {
			Debug.Log ("Child " + index + parentTo.GetChild (index).name);
		}
		roleAllocate.GetComponent<RoleAllocation> ().checkMoves ();
	}
}
