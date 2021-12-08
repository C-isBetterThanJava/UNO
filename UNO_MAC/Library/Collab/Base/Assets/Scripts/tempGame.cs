using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using System.Net.Http;
using System.Threading.Tasks;



public class tempGame : MonoBehaviour
{
    public Text AI1CardCount;
    public Text AI2CardCount;
    public Text AI3CardCount;
    // Start is called before the first frame update
    void Start()
     {
        
     }

    // // Update is called once per frame
     void Update()
     {
        AI1CardCount.text = players[0].cardCount().ToString();
        AI2CardCount.text = players[1].cardCount().ToString();
        AI3CardCount.text = players[2].cardCount().ToString();

        foreach(AIPlayer newPlayer in players)
        {
            if (newPlayer.cardCount() == 0)
            {
                Debug.Log("An AI player has won the game! Better luck next time..."); //ask Seok how to add in text that you can customize text for and make visible and invisible
            }
        }
        if (tempHumanPlayer.tHumanPlayer.getCurrentPlayerHand().Count == 0)
        {
            Debug.Log("You won the game, congratulations!!");
        }
    }
    private int currPlayerTurn;
    private AIPlayer nextPlayer;
    private bool directionOfPlay;
    private List<AIPlayer> players = new List<AIPlayer>();
    private tempHumanPlayer human;
    private int numTurns;
    public static tempGame gameInstance;
    private bool unoCalled;
    public Image colorSelector;
    public Text currentColor;
    public Text currentPlayer;


    //buttons aka cards
    public Button CardOneButton;
    public Button CardTwoButton;
    public Button CardThreeButton;
    public Button CardFourButton;
    public Button CardFiveButton;
    public Button CardSixButton;
    public Button CardSevenButton;
    public Button drawCard;
    public Button nextCard;
    public Button previousCard;

    public Button green;
    public Button blue;
    public Button yellow;
    public Button red;
    public bool colorChosen = false;

    public Button uno;
    public bool check;
    bool lastSkip = false;
    bool skipHuman = false;
    bool AIDrewCard = false;

    public bool getAIDrewCard()
    {
        return AIDrewCard;
    }
    public void setAIDrewCard(bool setCard)
    {
        AIDrewCard = setCard;
    }

    void Awake(){
        gameInstance = this;
        directionOfPlay = false;
        numTurns = 0;

        Debug.Log("about to create AI players");
        this.createAIPlayers();
        Debug.Log("done creating AI players, number = " + players.Count);
        Debug.Log("player: " + players[0]);
    }

    public tempGame(){
        currPlayerTurn = 0;
        directionOfPlay = true;
        numTurns = 0;
    }

    public void setDirectionOfPlay(bool direction){
        this.directionOfPlay = direction;
    }

    public bool getDirectionOfPlay(){
        return this.directionOfPlay;
    }

    public void setCurrPlayerTurn(int player){
        this.currPlayerTurn = player;
    }

    public int getCurrPlayerTurn(){
        return this.currPlayerTurn;
    }

    public int getNumTurns(){
        return numTurns;
    }

    public void setNumTurns(int turns){
        numTurns = turns;
    }

    public void playGame()
    {
        tempGame game = gameInstance;
        game.setDirectionOfPlay(false); //reverse is false so goes player, AI1, AI2, AI3

        //talk w/ Seok about whether we want to put create player deck into here or keep it in humanPlayer
        tempHumanPlayer human = tempHumanPlayer.tHumanPlayer;


    }

    public bool validCard(Deck card)
    {
         Debug.Log("Validating card");
         try {
             if (Deck.deckInstance.getLastPlayed().MyColor != null || Deck.deckInstance.getLastPlayed().MyValue != null)
             {
                 if (card.MyColor == Deck.deckInstance.getLastPlayed().MyColor || card.MyValue == Deck.deckInstance.getLastPlayed().MyValue || (int)card.MyValue > 12)
                 {
                     Debug.Log("last played was not null");
                     return true;
                 }
                
             }
         }
         catch (Exception e) {
             Debug.Log("last played was null");
             return true;
         }
         return false;
    }
     public void createAIPlayers()
     {
        //create instances of the 3 AI players

         AIPlayer aiPlayer1 = new AIPlayer("AI1");
         aiPlayer1.createHand();
         players.Add(aiPlayer1);
         AIPlayer aiPlayer2 = (AIPlayer)aiPlayer1.getClone(); //design pattern: prototype -> used to clone an instance of AIPlayer
         aiPlayer2.setName("AI2");
         AIPlayer aiPlayer3 = (AIPlayer)aiPlayer1.getClone();
         aiPlayer3.setName("AI3");

         aiPlayer2.createHand();
         players.Add(aiPlayer2);

         aiPlayer3.createHand();
         players.Add(aiPlayer3);
    }
    
    public void playAIRound()
    {
        //COMPOSITE pattern is found in this function -> we use players list as a singular
        //credit given to https://ronnieschaniel.medium.com/object-oriented-design-patterns-explained-using-practical-examples-84807445b092 for showing us how to use the composite pattern
        Debug.Log("entered AI round:" + players.Count);
        tempGame game = gameInstance;
        List<Deck> deck = Deck.deckInstance.getDeck(); //current cards available to draw in deck
        bool isContinue = true;
        int playerTracker;
        
        if (getDirectionOfPlay() == false) //reverse is false so goes player, AI1, AI2, AI3
        {
            playerTracker = 0;
        }
        else //reverse is false so goes player, AI3, AI2, AI1
        {
            playerTracker = 2;
        }

        while (isContinue == true)
        {
            Debug.Log("last played value: " + Deck.deckInstance.getLastPlayed().MyValue);
            Debug.Log("playerTracker: " + playerTracker);
            bool humanSkip = tempHumanPlayer.tHumanPlayer.getSkipHuman();

            if ((int)Deck.deckInstance.getLastPlayed().MyValue == 10 && (lastSkip == true || humanSkip == true)) //skip this turn
            {
                Debug.Log(players[playerTracker] + " has been skipped");
                if (getDirectionOfPlay() == false) //reverse is false so goes player, AI1, AI2, AI3
                {
                    playerTracker++;
                }
                else //reverse is false so goes player, AI3, AI2, AI1
                {
                    playerTracker--;
                }
                lastSkip = false;
            }
            else
            {
                if ((int)Deck.deckInstance.getLastPlayed().MyValue == 11) //draw 2
                {
                    Debug.Log(players[playerTracker] + " draws 2");
                    players[playerTracker].drawCard();
                    players[playerTracker].drawCard();
                }
                if ((int)Deck.deckInstance.getLastPlayed().MyValue == 14) //draw 4
                {
                    Debug.Log(players[playerTracker] + " draws 4");
                    players[playerTracker].drawCard();
                    players[playerTracker].drawCard();
                    players[playerTracker].drawCard();
                    players[playerTracker].drawCard();
                }

                Debug.Log("ABOUT TO PICK OUT AI CARD TO PLAY");
                Debug.Log("num AI players " + players.Count);
                Debug.Log("playertracker = " + playerTracker);
                Debug.Log("AI Player = " + players[playerTracker]);
                Deck playedCard = players[playerTracker].AIPlayingCard();
                if (getAIDrewCard() == false)
                {
                    players[playerTracker].playCard(playedCard); //shows off the specific card chosen last by AI
                                                                 //Debug.Log("creating pause right now");
                                                                 //createPause();
                    if ((int)playedCard.MyValue == 10)
                    {
                        lastSkip = true;
                    }
                    Debug.Log("CHOSEN CARD BY AI IS: " + playedCard.MyColor);
                    if ((int)playedCard.MyValue == 12)//reverse
                    {
                        if (getDirectionOfPlay() == false) //reverse is false so goes player, AI1, AI2, AI3
                        {
                            setDirectionOfPlay(true);
                        }
                        else //reverse is false so goes player, AI3, AI2, AI1
                        {
                            setDirectionOfPlay(false);
                        }


                    }
                }
                AIDrewCard = false;
            }
           
            

            if (getDirectionOfPlay() == false) //reverse is false so goes player, AI1, AI2, AI3
            {
                playerTracker++;
            }
            else //reverse is false so goes player, AI3, AI2, AI1
            {
                playerTracker--;
            }
            if (playerTracker == 3 || playerTracker == -1)
            {
                isContinue = false;
            }
            
            tempHumanPlayer.tHumanPlayer.consequence();
        }

        if ((int)Deck.deckInstance.getLastPlayed().MyValue == 10)
        {
            playAIRound(); //human has been skipped
        }

        //now that AI is finishes the human can play their card
        Debug.Log("here");
        enablePlayerButtons();
    }

    public void enablePlayerButtons(){
        CardOneButton.interactable = true;
        CardTwoButton.interactable = true;
        CardThreeButton.interactable = true;
        CardFourButton.interactable = true;
        CardFiveButton.interactable = true;
        CardSixButton.interactable = true;
        CardSevenButton.interactable = true;
        drawCard.interactable = true;
        nextCard.interactable = true;
        previousCard.interactable = true;

        Debug.Log("enabled human buttons");
    }

    public void disablePlayerButtons(){
        CardOneButton.interactable = false;
        CardTwoButton.interactable = false;
        CardThreeButton.interactable = false;
        CardFourButton.interactable = false;
        CardFiveButton.interactable = false;
        CardSixButton.interactable = false;
        CardSevenButton.interactable = false;
        drawCard.interactable = false;
        nextCard.interactable = false;
        previousCard.interactable = false;
        Debug.Log("disabled human buttons");
        
    }

    public bool getUnoCalled(){
        return unoCalled;
    }

    public void setUnoCalled(bool status){
        unoCalled = status;
    }
   
    public void colorSelection(){
        colorSelector.sprite = tempHumanPlayer.tHumanPlayer.cardImages[51];
        colorSelector.transform.position = new Vector3(960.0f, 540.0f, 0);
        green.transform.position = new Vector3(1080.0f, 425.0f, 0);
        blue.transform.position = new Vector3(1080.0f, 665.0f, 0);
        yellow.transform.position = new Vector3(840.0f, 425.0f, 0);
        red.transform.position = new Vector3(840.0f, 665.0f, 0);
        disablePlayerButtons();
    }

    public void changeColor(int colorSelected){
        //disable other buttons

        switch (colorSelected){
            case 1:
                Debug.Log("green selected");
                currentColor.text = "GREEN";
                break;
            case 2:
                Debug.Log("red selected");
                currentColor.text = "RED";
                break;
            case 3: 
                Debug.Log("blue selected");
                currentColor.text = "BLUE";
                break;
            case 4:
                Debug.Log("yellow selected");
                currentColor.text = "YELLOW";
                break;
            default:
                break;

        string cardName = currentColor.text.ToString() + tempHumanPlayer.tHumanPlayer.getCurrentPlayerHand()[tempHumanPlayer.tHumanPlayer.playedCard].MyValue.ToString();
        for (int j = 0; j < tempHumanPlayer.tHumanPlayer.cardNames.Length; j++)
        {
            if (tempHumanPlayer.tHumanPlayer.cardNames[j] == cardName)
            {   
                Deck.deckInstance.setLastPlayed(tempHumanPlayer.tHumanPlayer.getCurrentPlayerHand()[tempHumanPlayer.tHumanPlayer.playedCard], tempHumanPlayer.tHumanPlayer.cardImages[j]);
                tempHumanPlayer.tHumanPlayer.previousCard.sprite = tempHumanPlayer.tHumanPlayer.cardImages[j];
                break;
            }
        }
            
        }
        colorSelector.transform.position = new Vector3(-10000.0f, -10000.0f, 0);
        green.transform.position = new Vector3(-10000.0f, -10000.0f, 0);
        blue.transform.position = new Vector3(-10000.0f, -10000.0f, 0);
        yellow.transform.position = new Vector3(-10000.0f, -10000.0f, 0);
        red.transform.position = new Vector3(-10000.0f, -10000.0f, 0);
        enablePlayerButtons();
    }

    public void unoButtonPosition(){
        disablePlayerButtons();
        uno.transform.position = new Vector3(Random.Range(50.0f, 1850.0f), Random.Range(50.0f, 1000.0f), 0);
    }

    public void unoButton(){
        setUnoCalled(true);
        enablePlayerButtons();
        uno.transform.position = new Vector3(Random.Range(-5000.0f, -18500.0f), Random.Range(-5000.0f, -10000.0f), 0);
        return; //need to do something idk what yet
    }

    public void setCurrentPlayer(string player){
        currentPlayer.text = player;
    }

        



    //need to have a check to make sure that the played card is not a reverse

    //         //user will choose their card to play - Seok how do we get the instance of UnoCard?
    //         List<UnoCard> deck = DeckOfCards.getDeck;//does this get the same deck?



    //         //need to have a function that grabs a card for AI player depending on their deck
    //         foreach (Player playah in game.players)
    //         {
    //             UnoCard selected = deck.getDeck[0];
    //             if (playah.getName().Contains("AI"))
    //             {

    //                 //AI player -> 

    //                 playah.playCard(selected, game); //not correct -> need to change cardSelected to what function returns

    //             }
    //             else
    //             {
    //                 //human player -> 
    //                 playah.playCard(selected, game); //not correct -> need to change cardSelected to clicked on card

    //             }

    //             if (playah.getCurrentHand().Count == 0)
    //             {
    //                 //means that a player won
    //                 return true;
    //             }
    //         }

    //         return false;



}
