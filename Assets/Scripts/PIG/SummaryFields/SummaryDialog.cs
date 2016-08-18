using UnityEngine;
using System.Collections;

public class SummaryDialog : MonoBehaviour {

	public DialogBox dialogBox;

	// Use this for initialization
	void Start () {
		/*//updateDialogBox ("Note on Retro!", "After each sprint is finished and the deliverable is completed, the team " +
			"meets together to discuss how to improve for the next sprint. Topics of discussion include thoughts about" +
			" increasing team efficiency, the delevopment process, and more. One technique to conduct a retro is to ask " +
			"the team what they should start doing, stop doing, and continue doing." );*/
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateDialogBox(string title, string body){
		DialogBox dialog = dialogBox.GetComponent<DialogBox> ();
		dialog.init(title, body);
	}

}
