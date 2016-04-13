using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {

	public float targetRatioWidth = 16.0f;
	public float targetRatioHeight = 9.0f;
	public GameObject UIScaler;

	// Use this for initialization
	void Start () {
		float targetAspect = targetRatioWidth / targetRatioHeight;
		float windowAspect = (float)Screen.width / (float)Screen.height;

		float scaleHeight = windowAspect / targetAspect;

		Camera camera = GetComponent<Camera> ();

		if (scaleHeight < 1.0f) {
			Rect rect = camera.rect;
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;
			UIScaler.transform.localScale = new Vector3 (1, scaleHeight, 1);

			camera.rect = rect;
		} else {
			float scaleWidth = 1.0f / scaleHeight;
			Rect rect = camera.rect;
			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scaleWidth) / 2.0f;
			rect.y = 0;
			UIScaler.transform.localScale = new Vector3 (scaleWidth, 1, 1);

			camera.rect = rect;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
