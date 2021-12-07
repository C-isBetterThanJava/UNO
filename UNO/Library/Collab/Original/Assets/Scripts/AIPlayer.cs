using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AIPlayer : tempPlayer
{

    private List<Deck> currentHand = new List<Deck>();
    private string name;
    public Text AI1CardCount;
    public Text AI2CardCount;
    public Text AI3CardCount;

    void Update()
    {
        AI1CardCount.text = cardCount().ToString();
        AI2CardCount.text = cardCount().ToString();
        AI3CardCount.text = cardCount().ToString();
    }

    public AIPlayer()
    {
       
    }

    public string getName()
    {
        Debug.Log("get name");
        return this.name;
    }

    public Deck AIPlayingCard()
    {
        Debug.Log("entering AI Playing Card");
        //This function picks out the card that the AI player will put down to play or ascertain that they have no valid playing card.

        //first look at current deck and compare it to the last card played
        //pick out valid cards that can be played -> put them into a list
        //then look at valid list and choose card that has the highest value and is not a wild
        //if only valid cards are wild, then use wild
        //if there are no valid cards to play, draw one more card
        List<Deck> thisHand = getCurrentHand();
        Debug.Log(thisHand.Count);
        List<Deck> deck = Deck.deckInstance.getDeck();
        List<Deck> possibleCards = new List<Deck>();
        foreach (Deck card in thisHand)
        {
            if (tempGame.gameInstance.validCard(card) == true)
            {
                possibleCards.Add(card);
                Debug.Log(card.MyColor);
            }
            /*if (card.MyColor == Deck.deckInstance.getLastPlayed().MyColor || (int)card.MyValue == ((int)Deck.deckInstance.getLastPlayed().MyValue) || (int)card.MyValue > 12)
            {
                
            }*/
        }
        Debug.Log("size of possible cards: " + possibleCards.Count);
        if (possibleCards.Count == 0)
        {
            //no possible cards to play
            return null;
        }

        Deck tempCard = possibleCards[0];
        for(int i = 0; i < possibleCards.Count; i++) //go through all possible playing cards
        {
            if ((int)tempCard.MyValue < (int)possibleCards[i].MyValue && (int)possibleCards[i].MyValue <= 12) //find highest valued card (except wild)
            {
                tempCard = possibleCards[i];
            }
        }
        if ((int)tempCard.MyValue <= 12) //return/play highest value card other than wild
        {
            return tempCard;
        }


        return possibleCards[0]; //return wild card
    }

    public void createHand(){

        for(int i = 0; i < 7; i++){
            this.drawCard();
        }
    }

    public int cardCount(){
        return this.getCurrentHand().Count;
    }

    // public void drawCard(){
    //     Debug.Log("this is draw card in tempPlayer");
    //     List<Deck> deck = Deck.deckInstance.getDeck();
    //     Debug.Log("after deck");
    //     this.getCurrentHand().Add(deck[0]);
    //     Debug.Log(2);
    //     Deck.deckInstance.removeCard();
    //     for(int i = 0; i< this.getCurrentHand().Count; i++){
    //         Debug.Log(this.getCurrentHand()[i].MyColor);
    //         Debug.Log(this.getCurrentHand()[i].MyValue);
    //     }
    // }
    private void addToCurrentHand(Deck newCard)
    {
        currentHand.Add(newCard);
    }
    
    public void drawCard(){
        if (Deck.deckInstance.getDeck().Count < 8) //refresh deck if not many cards left
        {
            Deck.deckInstance.createDeck();
            Deck.deckInstance.shuffleDeck();
        }
        //Debug.Log("Drawing Card");
        List<Deck> deck = Deck.deckInstance.getDeck();
        //Debug.Log("after Decl");
        if(deck.Count - 1 == 0){
            Debug.Log("cannot draw card, no more cards left in deck"); //we need to recreate the deck here.
        }
        //Debug.Log(deck[0].MyColor);
        //Debug.Log(deck[0].MyValue);
        this.addToCurrentHand(deck[0]);
        /*for (int i = 0; i < this.getCurrentHand().Count; i++)
        {
            Debug.Log("this current card: " + this.getCurrentHand()[i].MyColor);
        }*/
        //this.getCurrentHand().Add(deck[0]);

        Deck.deckInstance.removeCard();
    }

    public void playCard(Deck card){
        int playedCard = 0;
        for(int i = 0; i < this.getCurrentHand().Count; i++){
            if(card.MyColor.ToString() + card.MyValue.ToString() == this.getCurrentHand()[i].MyColor.ToString() + this.getCurrentHand()[i].MyValue.ToString()){
                playedCard = i;
            }
        }
        if(tempGame.gameInstance.validCard(getCurrentHand()[playedCard]))
        {
            string givenCardName = getCurrentHand()[playedCard].MyColor.ToString() + getCurrentHand()[playedCard].MyValue.ToString();
            // if((int)getCurrentHand()[playedCard].MyValue > 12){
            //     // string newColor = tempGame.gameInstance.colorSelection();
            //     tempGame.gameInstance.colorSelection();
            //     // givenCardName = newColor + getCurrentPlayerHand()[playedCard].MyValue.ToString();
            // }
            updatePreviousCard(playedCard, givenCardName);
            getCurrentHand().RemoveAt(playedCard);
        }
        else
        {
            Debug.Log("Invalid Card");
        }
    }

    public List<Deck> getCurrentHand(){
        return currentHand;
    }

    public void setCurrentHand(List<Deck> deck){
        this.currentHand = deck;
    }

    public void updatePreviousCard(int playedCard, string cardName)
    {
        tempHumanPlayer player = tempHumanPlayer.tHumanPlayer;
        for (int j = 0; j < player.cardNames.Length; j++)
        {
            if (player.cardNames[j] == cardName)
            {
                Deck.deckInstance.setLastPlayed(getCurrentHand()[playedCard], player.cardImages[j]);
                // previousCard.sprite = cardImages[j];
                break;
            }
        }
    }

    public void callUno(){
        tempGame game = tempGame.gameInstance;
        tempHumanPlayer player = tempHumanPlayer.tHumanPlayer;
        Debug.Log("Waiting to call uno");
        // new WaitForSeconds(Random.Range(1, 5));
        System.Threading.Thread.Sleep(5000);
        if(game.getUnoCalled() == false){
            game.setUnoCalled(true);
            Debug.Log("Uno called");
            player.drawPlayerCard();
            player.drawPlayerCard();
        }
        else{
            Debug.Log("Uno already called");
        }
    }

   


    


    
    //     public AIPlayer(){
    //         //generateDeck
    //         //grab randomName
    //         string randomName = grabRandomName();
    //         name = randomName;
    //     }

    //     public void createAI(){
    //         AIPlayer AIplayer1 = new AIPlayer();
    //         Debug.Log("AI player instance created");
    //         AIPlayer AIplayer2 = new AIPlayer();
    //         Debug.Log("AI player instance created");
    //         AIPlayer AIplayer3 = new AIPlayer();
    //         Debug.Log("AI player instance created");
    //     }

    //     public string grabRandomName(){
    //         string[] names = new string[] {"Max Verstappen", "Lewis Hamilton", "Valtteri Bottas", "Lando Norris", "Sergio Perez", "Carlos Sainz", "Charles Leclerc", "Daniel Ricciardo", "Pierre Gasly", "Fernando Alonso", "Esteban Ocon", "Sebastian Vettel", "Lance Stroll", "Yuki Tsunoda", "George Russell", "Nicholas Latifi", "Kimi Räikkönen", "Antonio Giovinazzi", "Mick Schumacher", "Nikita Mazepin"};
    //         return names[0];
    //     }

    // public void Increment()
    // {
    //     if (NumberOfPlayers != null)
    //     {
    //         numberOfAI++;
    //         Debug.Log(numberOfAI);
    //         NumberOfPlayers.text = numberOfAI.ToString();
    //     }   
    // }

    // public void Decrement()
    // {
    //     if (NumberOfPlayers != null)
    //     {
    //         numberOfAI--;
    //         Debug.Log(numberOfAI);
    //         NumberOfPlayers.text = numberOfAI.ToString();
    //     } 
    // }




}
