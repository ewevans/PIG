using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour {
	public GameObject zone1;
	public GameObject zone2;
	public GameObject gameSystem;


	public string text;

	void Start () {
	}
	public void init(string title, string body){

		transform.parent.localScale = new Vector3 (1, 1, 1);

		//GameSystem system = gameSystem.GetComponent<GameSystem> ();
		//zone1 = zone1.GetComponent<GameObject> ();
		zone1.transform.SetParent (transform);
		zone1.transform.localScale = new Vector3 (1, 1, 1);
		zone1.GetComponent<Text>().text = title;

		zone2.transform.SetParent (transform);
		zone2.transform.localScale = new Vector3 (1, 1, 1);
		zone2.GetComponent<Text>().text = body;

	}

	public void OnClick(){
		transform.parent.localScale = new Vector3 (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {

	}
}
