using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform rect = GetComponent<RectTransform> ();
		rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 690);
		rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 220);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
