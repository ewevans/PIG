using UnityEngine;
using System.Collections;

public static class TutorialMessages {
	private static string[][] message = new string[][]{
		new string[]{"Each Day of the Sprint",
			"The goal of each Sprint is to complete the scheduled tasks under budget and with minimal defects " +
			"before the Sprint is complete.\nDuring each day of the Sprint, you have the option to play an effect card " +
			"then an optional development card.\nThe day is ended by dragging a card to the Discard pile to remove it from play."},
		new string[]{"Play Cards",
			"All cards are played by dragging to the correct pile.\nTo play an Instant Effect or Development card, drag them to " +
			"the Play Pile.\nTo play a Lasting Effect, drag it to one of the Lasting Effect Piles to the left of the Play Pile."},
		new string[]{"Produce Lines of Code with Cards!",
			"Lines of Code (LOC) are produced by playing Development cards, which also produce Defects as a byproduct.\n" +
			"To play one, drag the card from your hand into the play pile.  Just remember to play the effect card of the day " +
			"before the Development card!"},
		new string[]{"Manage your Team!",
			"Your development team is comprised of Coders, Debuggers, and Testers.\n\nCoders are responsible for producing " +
			"Lines of Code (LOC) by multiplying the LOC of Development cards by the number of Coders on your team.\n\n" +
			"Debuggers have the job of removing all the Defects that are produced by the Development cards. " +
			"Some Instant Effect cards are Debugging cards which rely on the Debuggers to work.\n\n" +
			"Testers give a chance of drawing more valuable Debugging cards, which help complete the more difficult tasks.\n\n"},
		new string[]{"Defects?!",
			"Defects are produced when Development cards are played.  The current total of Defects can be seen on the Defect Bar " + 
			"on the top left of the screen.\nAs Defects are accumulated, the Defect Bar changes from green to yellow, and finally RED " +
			"indicating that the Defect limit has been reached.  Once the limit is reached, Development cards are NOT PLAYABLE " +
			"until the Defect situation is handled."},
		new string[]{"Budget",
			"Your team starts with a set Budget at the beginning of each Sprint.  Each Developer costs $100 a day to keep employed, " +
			"and can be fired with the \"Fire Developer\" Instant Effect card.\nEffect cards may also affect your Budget. "+
			"Don't forget to plan ahead to see if you will have the money for each purchase."},
		new string[]{"The Daily Stand-Up",
			"In Scrum methodology, your team meets every morning to go over yesterday's and today's work, as well as any " +
			"impediments to any current work.\nThe pop-up window on the right side of the screen contains details from " +
			"a day's Daily Stand-Up, including extra information about the state of your Sprint.\nSometimes events occur " +
			"which affect your Project's resources, and these will be described in the Daily Stand-Up."},
		new string[]{"Categories",
			"When you have a Lasting Effect card in play of the same Category (Defect, Communication, etc.) as an Event " +
			"that occurs, the event is cancelled.\n If a Lasting Effect card has a Category, it can be found underneath " +
			"the hourglass."},
		new string[]{"Discard Hand",
			"If you have a hand full of cards you don't want, the Discard Hand button will draw an entirely new hand, " +
			"but will skip that day.  The button will not work after any card has been played on that day."},
		new string[]{"Tasks",
			"To finish an entire Project, each Task in the Product Backlog must be completed before the end of the Project's last Sprint."},
		new string[]{"Scrum Whiteboard",
			"If you want to check your team's progress through the Sprint, check out the Scrum Whiteboard on the bottom right of the screen.  " +
			"The Whiteboard will show the current tasks, as well as a burndown chart to visualize your progress."},
		new string[]{"Finish Early?",
			"If the LOC objective is reached early in a Sprint, you are given an option to finish the Sprint early or continue playing.\n\n" +
			"Ending early causes your team to focus on Debugging, removing 1 defect every day that remains.\n\n" +
			"If you choose to continue playing, you must play until the end of the Sprint.  Lines of Code past the objective are wasted, " + 
			"but if you focus on Debugging you can remove more than 1 a day.  Be careful, Events can still occur which may modify " +
			"your Lines of Code!"}
		
	};
	public static string[] getMessage(int day){
		if(day < message.Length)
			return message[day];
		else
			return null;
	}
}
