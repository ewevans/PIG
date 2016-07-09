using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerStats : MonoBehaviour {
	public static PlayerStats playerstats;

	public int totallinesdone;
	public int totallinesobjective;
	public int totaldefects;
	public int totalscore;
	public string rank;
	public string nextrank;
	public string nextrankscore;


	// Use this for initialization
	void Awake(){
		
		if (playerstats == null) 
		{
			playerstats = this;
		} 
		else if (playerstats != this)
			Destroy (gameObject);
		StatsLoad();
		//RankManager.rankmanager.initiateRanks ();

	}

	public void StatsLoad()
	{
		if (File.Exists (Application.persistentDataPath + "/playerstats.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerstats.dat", FileMode.Open);
			GameStats.PlayerData data= (GameStats.PlayerData)bf.Deserialize(file);
			file.Close ();

			totallinesdone = data.linesdone;
			totallinesobjective = data.linesobjective;
			totaldefects = data.defects;
			totalscore = data.score;
			rank = data.rank;
			nextrank = data.nextrank;
			nextrankscore = data.nextrankscore;


		}
	}






}
