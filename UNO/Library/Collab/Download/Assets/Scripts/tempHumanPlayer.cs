using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class tempHumanPlayer : tempPlayer
{

    public static tempHumanPlayer tHumanPlayer; //pattern design: singleton of human player
    private List<Deck> playerHand; //keeps track of the human's current hand of cards
    public Sprite[] cardImages; //how we populate the deck of human's current hand
    public string[] cardNames;

    public Sprite mySprite; 
    public Image cardOne; //images of current deck
    public Image cardTwo;
    public Image cardThree;
    public Image cardFour;
    public Image cardFive;
    public Image cardSix;
    public Image cardSeven;
    public Image previousCard;

    

    //buttons aka cards

    public Button CardOneButton; //what the user clicks on in order to play a card
    public Button CardTwoButton;
    public Button CardThreeButton;
    public Button CardFourButton;
    public Button CardFiveButton;
    public Button CardSixButton;
    public Button CardSevenButton;


    private int start; //keeps track of current hand indices
    private int end;
    public bool checkcreateDeck;
    private bool lastSkipped = false; //checks to see if AI last played a skip -> human is skipped over
    private bool firstRound = true;


    void Awake()
    {
        tHumanPlayer = this; //first thing that happens when the script runs -> initialize singleton instance and create player hand
        tHumanPlayer.setRange(0);
        playerHand = new List<Deck>();
        checkcreateDeck = true;
        createPlayerHand();
        checkcreateDeck = false;
        showDeck(0); //show the visuals of the deck

}

    public void createPlayerHand()
    {
        for (int i = 0; i < 7; i++)
        {
            this.drawCardConsequence(); //draws 7 cards for the player's starting hand of cards
        }
    }

    public List<Deck> getCurrentPlayerHand()
    {
        return playerHand; //returns the player's current hand of cards
    }

    public void drawPlayerCard()
    {
        //same as drawCardConsequences, but ends the round for the player by calling the endPlayerTurn()
        if (Deck.deckInstance.getDeck().Count < 8) //refresh deck if not many cards left
        {
            Debug.Log("deck refresh");
            Deck.deckInstance.createDeck();
            Deck.deckInstance.shuffleDeck();
        }
        Debug.Log("Drawing Card");
        List<Deck> deck = Deck.deckInstance.getDeck();
        if (deck.Count - 1 == 0)
        {
            Debug.Log("cannot draw card, no more cards left in deck"); //we need to recreate the deck here.
        }
        getCurrentPlayerHand().Add(deck[0]);
        Deck.deckInstance.removeCard();
        Debug.Log(getCurrentPlayerHand().Count);
        if (getCurrentPlayerHand().Count < 8)
        {
            Debug.Log("Showing deck");
            showDeck(getStart());
        }
        Debug.Log("BOOL CREATEDECK: " + checkcreateDeck);
        if(checkcreateDeck == false){
            endPlayerTurn();
        }
    }

    public void drawCardConsequence()
    {
        //same as drawPlayerCard, but does not end the round for the player by calling the endPlayerTurn()
        if (Deck.deckInstance.getDeck().Count < 8) //refresh deck if not many cards left
        {
            Debug.Log("deck refresh");
            Deck.deckInstance.createDeck();
            Deck.deckInstance.shuffleDeck();
        }
        Debug.Log("Drawing Card");
        List<Deck> deck = Deck.deckInstance.getDeck();
        if (deck.Count - 1 == 0)
        {
            Debug.Log("cannot draw card, no more cards left in deck"); //we need to recreate the deck here.
        }
        getCurrentPlayerHand().Add(deck[0]);
        Deck.deckInstance.removeCard();
        Debug.Log(getCurrentPlayerHand().Count);
        if (getCurrentPlayerHand().Count < 8)
        {
            Debug.Log("Showing deck");
            showDeck(getStart());
        }
    }
    public void setRange(int newStart)
    {
        start = newStart; //sets range for images of player's current hand
    }

    public int getStart()
    {
        return start; //grabs the first index of the player's current hand image
    }

    public int getEnd()
    {
        return end; //grabs the last index of the player's current hand image
    }

    public void showDeck(int start)
    {
        //shows seven cards of the player's current hand from the starting index on
        cardOne.sprite = null;
        cardTwo.sprite = null;
        cardThree.sprite = null;
        cardFour.sprite = null;
        cardFive.sprite = null;
        cardSix.sprite = null;
        cardSeven.sprite = null;

        //get all the image names into a sprite array
        //getting all the names of the images we will need to load onto buttons
        List<int> indexes = new List<int>();
        int limit = Math.Min(getCurrentPlayerHand().Count, start + 7);
        for (int i = start; i < limit; i++)
        {
            string path = "";
            path = getCurrentPlayerHand()[i].MyColor.ToString() + getCurrentPlayerHand()[i].MyValue.ToString();
            for (int j = 0; j < cardNames.Length; j++)
            {
                if (cardNames[j] == path)
                {
                    indexes.Add(j);
                    break;
                }
            }
        }

        int counter = 0;
        for (int i = 0; i < indexes.Count; i++)
        { //have to use a case state in a forloop incase theres not 7 cards
            switch (i)
            {
                //decides which card is displayed where
                case 1:
                    cardTwo.sprite = cardImages[indexes[1]];
                    break;
                case 2:
                    cardThree.sprite = cardImages[indexes[2]];
                    break;
                case 3:
                    cardFour.sprite = cardImages[indexes[3]];
                    break;
                case 4:
                    cardFive.sprite = cardImages[indexes[4]];
                    break;
                case 5:
                    cardSix.sprite = cardImages[indexes[5]];
                    break;
                case 6:
                    cardSeven.sprite = cardImages[indexes[6]];
                    break;
                default:
                    cardOne.sprite = cardImages[indexes[0]];
                    break;
            }
        }
    }

    
    public bool getSkipHuman()
    {
        return lastSkipped; //checks if the AI skipped the human
    }

    public void consequence()
    {
        //checks to make sure whether the AI last played a skip, draw 2, or draw 4 and then distributes the "consequence" to the human accordingly 
        if (firstRound == false) //if the first round then throws error when checking for the AI played cards
        {
            Debug.Log("IN HUMAN ROUND, LAST PLAYED: " + Deck.deckInstance.getLastPlayed().MyValue);
            if ((int)Deck.deckInstance.getLastPlayed().MyValue == 10) //skip this turn
             {
                Debug.Log("human has been skipped");
                this.endPlayerTurn();
                return;
             }
            if ((int)Deck.deckInstance.getLastPlayed().MyValue == 11) //AI last set down draw 2
            {
                Debug.Log("human draws 2");
                this.drawCardConsequence(); //draws a new card for player -> does not end turn like other draw function
                this.drawCardConsequence();
            }
            if ((int)Deck.deckInstance.getLastPlayed().MyValue == 14) //draw 4
            {
                Debug.Log("human draws 4");
                this.drawCardConsequence();
                this.drawCardConsequence();
                this.drawCardConsequence();
                this.drawCardConsequence();
            }
        }
    }
    public void playCard(int buttonNumber)
    {
        //connected to the Unity UI -> enables the user to click on a card in their current hand and play that card
        firstRound = false;
        showDeck(0);

        int playedCard = getStart(); //grabs the card that was clicked
        playedCard = start + buttonNumber;
        
        Debug.Log("Clicked button: " + buttonNumber);
        if(tempGame.gameInstance.validCard(getCurrentPlayerHand()[playedCard])) //checks to make sure the card being played is valid: ie is it the same color or value as the previous card
        {
            string givenCardName = getCurrentPlayerHand()[playedCard].MyColor.ToString() + getCurrentPlayerHand()[playedCard].MyValue.ToString();
            if((int)getCurrentPlayerHand()[playedCard].MyValue > 12){    
                tempGame.gameInstance.colorSelection();
                string newColor = tempGame.gameInstance.currentColor.ToString();
                givenCardName = newColor + getCurrentPlayerHand()[playedCard].MyValue.ToString();
            }
            updatePreviousCardImage(playedCard, givenCardName); //updates the last card played and its image
            getCurrentPlayerHand().RemoveAt(playedCard); //removes the played card from the human's current hand
            showDeck(getStart()); //updated images are shown
        }
        else
        {
            Debug.Log("Invalid Card");
        }

        if(getCurrentPlayerHand().Count == 1){
            tempGame.gameInstance.unoButtonPosition();
        }

        /*Deck newestCard = getCurrentPlayerHand()[playedCard];
        lastSkipped = false; //resets the skip functionality
        if ((int)newestCard.MyValue == 10)
        {
            lastSkipped = true;
        }*/
        //disable buttons -> AI plays their turns before human can do theirs
        this.endPlayerTurn(); //starts off the AI turn
    }

    public void endPlayerTurn(){
        tempGame.gameInstance.disablePlayerButtons(); //player can no longer play during AI turn
        Debug.Log("disabled human buttons");
        tempGame.gameInstance.playAIRound(); //all 3 AI players are allowed their turn now
    }

    public void updatePreviousCardImage(int playedCard, string cardName)
    {
        //displays the previous card in the human's deck after clicking the "previous card" button
        for (int j = 0; j < cardNames.Length; j++)
        {
            if (cardNames[j] == cardName)
            {
                Deck.deckInstance.setLastPlayed(getCurrentPlayerHand()[playedCard], cardImages[j]);
                // previousCard.sprite = cardImages[j];
                break;
            }
        }
    }

    public void nextCard()
    {
        //displays the next card in the human's deck after clicking the "next card" button
        int start = tHumanPlayer.getStart();
        if (start + 7 + 1 > getCurrentPlayerHand().Count)
        { //start + 7 is the last card in deck, +1 is the next card 
            Debug.Log("No more cards");
            return;
        }
        else
        {
            start = start + 1;
            tHumanPlayer.setRange(start);
            showDeck(start);
        }
    }

    public void backButton()
    {
        //displays the previous card in the human's deck after clicking the "previous card" button
        int start = tHumanPlayer.getStart();
        if (start - 1 < 0)
        { //make sure we have a card to the left before we go left
            Debug.Log("No more cards");
            return;
        }
        else
        {
            start = start - 1;
            tHumanPlayer.setRange(start);
            showDeck(start);
        }
    }

    


}
