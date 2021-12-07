using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cards : MonoBehaviour
{


}

//credit to Coding Homework Youtube Channel for helping me figure out how to create card class based off poker video
public class UnoCard
{
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
}

public class DeckOfCards : UnoCard
{
    const int NUM_OF_CARDS = 60; //total cards available
    private UnoCard[] deck; //array of all playing cards

    public DeckOfCards()
    {
        deck = new UnoCard[NUM_OF_CARDS];
    }
    public UnoCard[] getDeck { get { return deck; } } //get current deck

    //create deck of 108 cards: 15 values each with 4 colors
    public void setUpDeck()
    {
        int i = 0;
        foreach (COLOR c in Enum.GetValues(typeof(COLOR)))
        {
            foreach (VALUE v in Enum.GetValues(typeof(VALUE)))
            {
                deck[i] = new UnoCard { MyColor = c, MyValue = v };
                i++;
            }
        }
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
}
