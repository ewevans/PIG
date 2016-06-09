using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventDeck {

    List<string> eventDeck = new List<string>();

    public EventDeck()
    {
        ResetDeck();
    }

    public void ResetDeck()
    {
        AddMultiple("BlueScreen", 1);
        AddMultiple("CorporateDownsizing", 1);
        AddMultiple("CriticalBug", 1);
        AddMultiple("CriticalDataLoss", 1);
        AddMultiple("IQuit", 1);
        AddMultiple("LongDailyStandUp", 1);
        AddMultiple("MandatoryMeetings", 1);
        AddMultiple("MinorRequirementsEdit", 1);
        AddMultiple("MissedDailyStandUp", 1);
        AddMultiple("MissedDefects", 1);
        AddMultiple("MissedRequirements", 1);
        AddMultiple("MistakeOnTargetDate", 1);
        AddMultiple("ProductOwnerVacation", 1);
        AddMultiple("SickDeveloper", 1);
        AddMultiple("WatchingViralVideos", 1);
        AddMultiple("WorkOverwritten", 1);

        //Add each card to the deck as many times as its probability attribute

    }

    void AddMultiple(string title, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            eventDeck.Add(title);
        }
    }

    //returns a card and removes from deck
    public string ChooseEvent()
    {
        string card = "";
        if (eventDeck.Count == 0)
        {
            //deck reset animation?
            ResetDeck();
        }

        //card will be the index of chosen card to draw
        int index = Random.Range(0, eventDeck.Count - 1);
        card = eventDeck[index];
        //removing card from the list
        eventDeck.RemoveAt(index);

        if (eventDeck.Count == 0)
        {
            //deck is empty after draw
        }

        return card;
    }
}
