using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using UnityEditor;

public class GameStats : MonoBehaviour {
	public static GameStats gamestats;

	public int linesdone;
	public int linesobjective;
	public int defects;
	public int defectsmax;
	public int budget;
	public int score;
	public int cumulativescore;
	public string rank;
	public string nextrank;
	public string nextrankscore;
	public float codepercentage;
	public float defectratio;

	// Use this for initialization
	void Awake(){
		if (gamestats == null) 
		{
			DontDestroyOnLoad (gameObject);
			gamestats = this;
		} 
		else if (gamestats != this)
			Destroy (gameObject);

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Save()
	{
		if (File.Exists (Application.persistentDataPath + "/playerstats.dat")) {
			BinaryFormatter oldbf = new BinaryFormatter ();
			FileStream oldfile = File.Open (Application.persistentDataPath + "/playerstats.dat", FileMode.Open);
			PlayerData olddata = (PlayerData)oldbf.Deserialize(oldfile);
			oldfile.Close ();

			BinaryFormatter newbf = new BinaryFormatter ();
			FileStream newfile = File.Create (Application.persistentDataPath + "/playerstats.dat");
			PlayerData newdata = new PlayerData ();

			newdata.linesdone = olddata.linesdone + linesdone;
			newdata.linesobjective = olddata.linesobjective + linesobjective;
			newdata.defects = olddata.defects + defects;
			newdata.budget = olddata.budget + budget;
			newdata.score = olddata.score + score;
			newdata.rank = olddata.rank;
			newdata.nextrank = olddata.nextrank;
			newdata.nextrankscore = olddata.nextrankscore;
			cumulativescore = olddata.score + score;
		

			newbf.Serialize (newfile, newdata);
			newfile.Close ();
		} 

		else 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/playerstats.dat");
			PlayerData newdata = new PlayerData ();

			newdata.linesdone = linesdone;
			newdata.linesobjective = linesobjective;
			newdata.defects = defects;
			newdata.budget = budget;
			newdata.score = score;
			newdata.rank = rank;
			newdata.nextrank = nextrank;
			newdata.nextrankscore = nextrankscore;
			cumulativescore = 0;

			bf.Serialize (file, newdata);
			file.Close ();
		}
	}


	public void ResetData()
	{
		//To fix Test Case 31, trying to reset by setting to 0 rather than deleting and remaking player data file
		if (File.Exists (Application.persistentDataPath + "/playerstats.dat")) 
		{
			//FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/playerstats.dat");
			File.Delete (Application.persistentDataPath + "/playerstats.dat");
		}
			

		BinaryFormatter newbf = new BinaryFormatter ();
		FileStream newfile = File.Create (Application.persistentDataPath + "/playerstats.dat");
		PlayerData newdata = new PlayerData ();

		newdata.linesdone = 0;
		newdata.linesobjective = 0;
		newdata.defects = 0;
		newdata.budget = 0;
		newdata.score = 0;
		newdata.rank = "Scrum Novice";
		newdata.nextrank = "Scrum Novice II";
		newdata.nextrankscore = "2,000";
		cumulativescore = 0;


		newbf.Serialize (newfile, newdata);
		newfile.Close ();




	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerstats.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerstats.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			GameStats.gamestats.linesdone = data.linesdone;
			GameStats.gamestats.linesobjective = data.linesobjective;
			GameStats.gamestats.defects = data.defects;
			GameStats.gamestats.budget = data.budget;
			GameStats.gamestats.score = data.score;
			GameStats.gamestats.rank = data.rank;
			GameStats.gamestats.nextrank = data.nextrank;
			GameStats.gamestats.nextrankscore = data.nextrankscore;
			cumulativescore = score;
			if (GameStats.gamestats.linesobjective != 0)
				GameStats.gamestats.codepercentage = (GameStats.gamestats.linesdone / GameStats.gamestats.linesobjective) * 100;
			else
				GameStats.gamestats.codepercentage = 0;
		}

	}

[Serializable]
public class PlayerData
{
				public int linesdone;
				public int linesobjective;
				public int defects;
				public int budget;
				public int score;
				public string rank;
				public string nextrank;
				public string nextrankscore;
}


}
