using UnityEngine;
using System.Collections;

public class RankManager{
	public static RankManager rankmanager;
	public string currentrank;
	public string currentnextrank;
	public string currentnextrankscore;

	public void initiateRanks()
	{
		var ranklist = new[] {
			new{
				rank = "Scrum Novice I",
				nextrank = "Scrum Novice II",
				nextrankscore = "2,000"
				},
			new{
				rank = "Scrum Novice II",
				nextrank = "Scrum Apprentice I",
				nextrankscore = "3,500"
				},
			new{
				rank = "Scrum Apprentice I",
				nextrank = "Scrum Apprentice II",
				nextrankscore = "4,500"
				},
			new{
				rank = "Scrum Apprentice II",
				nextrank = "Scrum Journeyman I",
				nextrankscore = "6,000"
				},
			new{
				rank = "Scrum Journeyman I",
				nextrank = "Scrum Journeyman II",
				nextrankscore = "8,000"
				},
			new{
				rank = "Scrum Journeyman II",
				nextrank = "Code Crusader I",
				nextrankscore = "10,000"
				},
			new{
				rank = "Code Crusader I",
				nextrank = "Code Crusader II",
				nextrankscore = "13,000"
				},
			new{		
				rank = "Code Crusader II",
				nextrank = "Agile Acolyte I",
				nextrankscore = "16,000"
				},
			new{
				rank = "Agile Acolyte I",
				nextrank = "Agile Acolyte II",
				nextrankscore = "20,000"
				},
			new{
				rank = "Agile Acolyte II",
				nextrank = "Sprint Sensei",
				nextrankscore = "24,000"
				},
			new{
				rank = "Sprint Sensei",
				nextrank = "Backlog Baron",
				nextrankscore = "28,000"
				},
			new{
				rank = "Backlog Baron",
				nextrank = "Project Whisperer",
				nextrankscore = "32,000"
				},
			new{
				rank = "Project Whisperer",
				nextrank = "Lord of the Scrum",
				nextrankscore = "37,000"
				},
			new{
				rank = "Lord of the Scrum",
				nextrank = "Max Rank Achieved!",
				nextrankscore = "---"
				}
		};

		if (GameStats.gamestats.cumulativescore <= 1999) {
			currentrank = ranklist [0].rank;
			currentnextrank = ranklist [0].nextrank;
			currentnextrankscore = ranklist [0].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 2000 && GameStats.gamestats.cumulativescore <= 3499) {
			currentrank = ranklist [1].rank;
			currentnextrank = ranklist [1].nextrank;
			currentnextrankscore = ranklist [1].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 3500 && GameStats.gamestats.cumulativescore <= 4499) {
			currentrank = ranklist [2].rank;
			currentnextrank = ranklist [2].nextrank;
			currentnextrankscore = ranklist [2].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 4500 && GameStats.gamestats.cumulativescore <= 5999) {
			currentrank = ranklist [3].rank;
			currentnextrank = ranklist [3].nextrank;
			currentnextrankscore = ranklist [3].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 6000 && GameStats.gamestats.cumulativescore <= 7999) {
			currentrank = ranklist [4].rank;
			currentnextrank = ranklist [4].nextrank;
			currentnextrankscore = ranklist [4].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 8000 && GameStats.gamestats.cumulativescore <= 9999) {
			currentrank = ranklist [5].rank;
			currentnextrank = ranklist [5].nextrank;
			currentnextrankscore = ranklist [5].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 10000 && GameStats.gamestats.cumulativescore <= 12999) {
			currentrank = ranklist [6].rank;
			currentnextrank = ranklist [6].nextrank;
			currentnextrankscore = ranklist [6].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 13000 && GameStats.gamestats.cumulativescore <= 15999) {
			currentrank = ranklist [7].rank;
			currentnextrank = ranklist [7].nextrank;
			currentnextrankscore = ranklist [7].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 16000 && GameStats.gamestats.cumulativescore <= 19999) {
			currentrank = ranklist [8].rank;
			currentnextrank = ranklist [8].nextrank;
			currentnextrankscore = ranklist [8].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 20000 && GameStats.gamestats.cumulativescore <= 23999) {
			currentrank = ranklist [9].rank;
			currentnextrank = ranklist [9].nextrank;
			currentnextrankscore = ranklist [9].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 24000 && GameStats.gamestats.cumulativescore <= 27999) {
			currentrank = ranklist [10].rank;
			currentnextrank = ranklist [10].nextrank;
			currentnextrankscore = ranklist [10].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 28000 && GameStats.gamestats.cumulativescore <= 31999) {
			currentrank = ranklist [11].rank;
			currentnextrank = ranklist [11].nextrank;
			currentnextrankscore = ranklist [11].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 32000 && GameStats.gamestats.cumulativescore <= 36999) {
			currentrank = ranklist [12].rank;
			currentnextrank = ranklist [12].nextrank;
			currentnextrankscore = ranklist [12].nextrankscore;
		} 
		else if (GameStats.gamestats.cumulativescore >= 37000) {
			currentrank = ranklist [13].rank;
			currentnextrank = ranklist [13].nextrank;
			currentnextrankscore = ranklist [13].nextrankscore;
		} 
		else {
			currentrank = "NULL";
			currentnextrank = "NULL";
			currentnextrankscore = "NULL";
		}
	}

}
