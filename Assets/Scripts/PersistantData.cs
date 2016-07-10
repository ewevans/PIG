using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour {
	public static PersistantData persistantData;

	void Awake(){
		if (persistantData == null) 
		{
			DontDestroyOnLoad (gameObject);
			persistantData = this;
		} 
		else if (persistantData != this)
			Destroy (gameObject);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
