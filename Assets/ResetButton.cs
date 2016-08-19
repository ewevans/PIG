using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetButton : MonoBehaviour {
	public NewGame game;
	Button button;
	// Use this for initialization
	void Awake () {
		button = GetComponent<Button> ();


		button.onClick.AddListener (() => {
			whenClicked ();
		});
	}

	void whenClicked(){
		GameStats.gamestats.ResetData ();
		//GameObject game = GetComponent<NewGame> ();
		//NewGame game = GetComponent<NewGame>();
		game.StartNewGame(2);
		//NewGame.StartNewGame (2);

	}

	// Update is called once per frame

}
