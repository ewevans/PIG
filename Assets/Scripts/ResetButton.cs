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
		PlayerPrefs.SetInt ("projectProgress", 0);
		GameStats.gamestats.ResetData ();

		game.StartNewGame(2);


	}

	// Update is called once per frame

}
