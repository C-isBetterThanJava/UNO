using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeckOfCards : UnoCard
{
    public static DeckOfCards originalDeck;
    const int NUM_OF_CARDS = 60; //total cards available
    private List<UnoCard> deck; //array of all playing cards
    // private bool made = false;
    private UnoCard lastPlayed = null;
    private DeckOfCards() {}  
    private static DeckOfCards instance = null;  

    void Awake(){
        originalDeck = this;
    }

    public static DeckOfCards DeckInstance {  
        get {  
            if (instance == null) {  
                instance = new DeckOfCards();  
            }  
            return instance;  
        }  
    }
        
    
    public List<UnoCard> getDeck { get { return deck; } } //get current deck

    //create deck of 108 cards: 15 values each with 4 colors
    public void setUpDeck()
    {
        Debug.Log("setUpDeck");
        int i = 0;
        foreach (COLOR c in Enum.GetValues(typeof(COLOR)))
        {
            foreach (VALUE v in Enum.GetValues(typeof(VALUE)))
            {
                deck.Add(new UnoCard { MyColor = c, MyValue = v }); //this line errors
                i++;
            }
        }
        Debug.Log("Shuffling Cards");
        ShuffleCards();
    }

    //shuffle the deck 
    public void ShuffleCards()
    {
        System.Random rand = new System.Random();
        UnoCard temp;

        //run the shuffle several times to make as mixed up as possible
        for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
        {
            //goes through 1000 to make sure lots of cards are swapped
            for (int i = 0; i < NUM_OF_CARDS; i++)
            {
                //swapping cards
                int secondCardIndex = rand.Next(15);
                temp = deck[i];
                deck[i] = deck[secondCardIndex];
                deck[secondCardIndex] = temp;
            }
        }
    }
    public UnoCard getLastPlayed(){
        return lastPlayed;
    }

    public void setLastPlayed(UnoCard card){
        lastPlayed = card;
    }
}

