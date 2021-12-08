using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Deck : MonoBehaviour
{
   // credit given to https://www.youtube.com/watch?v=6SovupmTRt8 for showing us how to create the Deck class and then implement the createDeck() and shuffleDeck() functions based off of a poker game
    const int NUM_OF_CARDS = 60;
    private List<Deck> deck = new List<Deck>();
    private Deck lastPlayed;
    public static Deck deckInstance;
    public Image previousCard;

    public enum COLOR
    {
        RED, YELLOW, GREEN, BLUE 
    }
    public enum VALUE
    {
        ZERO = 0, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, SKIP, DRAW2, REVERSE, WILD, WILDDRAW4
    }
    public COLOR MyColor { get; set; }
    public VALUE MyValue { get; set; }

    void Awake(){
        deckInstance = this;
        createDeck();
        shuffleDeck();
    }

    public void createDeck(){
        deck.Clear();
        //deck = new List<Deck>();
        Debug.Log("setUpDeck");
        int i = 0;
        foreach (COLOR c in Enum.GetValues(typeof(COLOR)))
        {
            foreach (VALUE v in Enum.GetValues(typeof(VALUE)))
            {
                deck.Add(new Deck { MyColor = c, MyValue = v }); //this line errors
                i++;
            }
        }
        Debug.Log("Shuffling Cards");
    }

    public void shuffleDeck(){
        System.Random rand = new System.Random();
        Deck temp;

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

    public List<Deck> getDeck(){
        return deck;
    }
    
    public void removeCard(){
        deck.RemoveAt(0);
    }

    public Deck getLastPlayed(){
        return lastPlayed;
    }

    public void setLastPlayed(Deck card, Sprite newLastPlayed){
        // Debug.Log("lastPlayed");
        tempGame.gameInstance.currentColor.text = card.MyColor.ToString();
        previousCard.sprite = newLastPlayed;
        lastPlayed = card;
        // Debug.Log(lastPlayed.MyColor + " " + lastPlayed.MyValue);
    }
}
