using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class tempHumanPlayer : tempPlayer
{
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public static tempHumanPlayer tHumanPlayer;
    private List<Deck> playerHand;
    public Sprite[] cardImages;
    public string[] cardNames;

    public Sprite mySprite;
    public Image cardOne;
    public Image cardTwo;
    public Image cardThree;
    public Image cardFour;
    public Image cardFive;
    public Image cardSix;
    public Image cardSeven;
    public Image previousCard;

    

    //buttons aka cards

    public Button CardOneButton;
    public Button CardTwoButton;
    public Button CardThreeButton;
    public Button CardFourButton;
    public Button CardFiveButton;
    public Button CardSixButton;
    public Button CardSevenButton;


    private int start;
    private int end;
    public bool checkcreateDeck;
    private bool lastSkipped = false;
    private bool firstRound = true;


    void Awake()
    {
        tHumanPlayer = this;
        tHumanPlayer.setRange(0);
        playerHand = new List<Deck>();
        checkcreateDeck = true;
        createPlayerHand();
        checkcreateDeck = false;
        showDeck(0);


        /*check = GameObject.FindWithTag("CardOneButton").GetComponent<Button>();
        CardOneButton = GameObject.FindWithTag("CardOneButton").GetComponent<Button>();
        CardTwoButton = GameObject.FindWithTag("CardTwoButton").GetComponent<Button>();
        CardThreeButton = GameObject.FindWithTag("CardThreeButton").GetComponent<Button>();
        CardFourButton = GameObject.FindWithTag("CardFourButton").GetComponent<Button>();
        CardFiveButton = GameObject.FindWithTag("CardFiveButton").GetComponent<Button>();
        CardSixButton = GameObject.FindWithTag("CardSixButton").GetComponent<Button>();
        CardSevenButton = GameObject.FindWithTag("CardSevenButton").GetComponent<Button>();*/
}

    // public tempHumanPlayer(){

    // }

    public void createPlayerHand()
    {
        for (int i = 0; i < 7; i++)
        {
            this.drawPlayerCard();
        }
    }

    public List<Deck> getCurrentPlayerHand()
    {
        return playerHand;
    }

    public void drawPlayerCard()
    {
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
        start = newStart;
    }

    public int getStart()
    {
        return start;
    }

    public int getEnd()
    {
        return end;
    }

    public void showDeck(int start)
    {
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
        return lastSkipped;
    }

    public void consequence(){
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
        //NINE, SKIP, DRAW2, REVERSE, WILD, WILDDRAW4
        //IF NOT ALLOWING USER TO PICK CARD THIS IF STATEMENT IS THE PROBLEM
        
        firstRound = false;
        showDeck(0);

        int playedCard = getStart();
        playedCard = start + buttonNumber;
        
        Debug.Log("Clicked button: " + buttonNumber);
        if(tempGame.gameInstance.validCard(getCurrentPlayerHand()[playedCard]))
        {
            string givenCardName = getCurrentPlayerHand()[playedCard].MyColor.ToString() + getCurrentPlayerHand()[playedCard].MyValue.ToString();
            if((int)getCurrentPlayerHand()[playedCard].MyValue > 12){    
                tempGame.gameInstance.colorSelection();
                string newColor = tempGame.gameInstance.currentColor.ToString();
                givenCardName = newColor + getCurrentPlayerHand()[playedCard].MyValue.ToString();
            }
            updatePreviousCardImage(playedCard, givenCardName);
            getCurrentPlayerHand().RemoveAt(playedCard);
            showDeck(getStart());
        }
        else
        {
            Debug.Log("Invalid Card");
        }

        if(getCurrentPlayerHand().Count == 1){
            tempGame.gameInstance.unoButtonPosition();
            // AIPlayer1.AIPlayerOne.callUno();
            // AIPlayer2.AIPlayerTwo.callUno();
            // AIPlayer3.AIPlayerThree.callUno();
        }

        Deck newestCard = getCurrentPlayerHand()[playedCard];
        lastSkipped = false; //resets the skip functionality
        if ((int)newestCard.MyValue == 10)
        {
            lastSkipped = true;
        }
        //disable buttons -> AI plays their turns before human can do theirs
        this.endPlayerTurn();
    }

    public void endPlayerTurn(){
        tempGame.gameInstance.disablePlayerButtons();
        Debug.Log("disabled human buttons");
        tempGame.gameInstance.playAIRound();
    }

    public void updatePreviousCardImage(int playedCard, string cardName)
    {
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

    public void addObserver(){
        observer.Add(new Announcer());
    }

    public void notifyObserver() {
        for(int i = 0; i < observer.Count; i++){
            observer[i].uno();
        }
    }

}
