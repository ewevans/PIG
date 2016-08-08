using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck
{

    //list of cards in the player's deck not drawn yet
    private List<string> deck = new List<string>();
    public Deck()
    {
        ResetDeck();
    }


    public void ResetDeck()
    {
        AddMultiple("AutomatedTools", 2);
        AddMultiple("BackupSystem", 2);
        AddMultiple("Coding30", 8);
        AddMultiple("Coding40", 8);
        AddMultiple("Coding50", 5);
        AddMultiple("CongregateTeam", 2);
        AddMultiple("DailyStandupUpgrade", 2);
        AddMultiple("Debugging", 5);
        AddMultiple("DebuggingMore", 5);
        AddMultiple("ExecutiveSupport", 2);
        AddMultiple("FireDeveloper", 2);
        //AddMultiple("FormalTechReview", 2);
        AddMultiple("HireCoder", 1);
		AddMultiple("HireDebugger", 1);
		AddMultiple("HireTester", 1);
        AddMultiple("NegotiateBudget", 2);
        AddMultiple("NegotiateTime", 2);
        AddMultiple("PairProgramming", 2);
        AddMultiple("QAContractor", 2);
        AddMultiple("RequirementsContract", 2);
        AddMultiple("RoleAllocation", 2);
        AddMultiple("SubjectMatterExpert", 2);
        AddMultiple("TakeShortcuts", 2);
        //AddMultiple("TestPlan", 2);
        AddMultiple("ThirdPartySoftware", 2);
        //AddMultiple("UnitTesting", 2);

        

        //Add each card to the deck as many times as its probability attribute

    }

    void AddMultiple(string title, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            deck.Add(title);
        }
    }

    //returns a card and removes from deck
    public string DealCard()
    {
        string card = "";
        if (deck.Count == 0)
        {
            //deck reset animation?
            ResetDeck();
        }

        //card will be the index of chosen card to draw
        int index = Random.Range(0, deck.Count - 1);
        card = deck[index];
        //removing card from the list
        deck.RemoveAt(index);

        if (deck.Count == 0)
        {
            //deck is empty after draw
        }

        return card;
    }

	public string DealTesterCard()
	{
		float rand = Random.value;
		string card = "";

		if (rand < .33f) {
			card = "UnitTesting";	
		} else if (rand < .66f) {
			card = "FormalTechReview";
		} else {
			card = "TestPlan";
		}

		return card;
	}

}


