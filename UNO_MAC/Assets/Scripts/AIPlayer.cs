using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AIPlayer : tempPlayer, AIPrototype
{

    private List<Deck> currentHand = new List<Deck>();
    private string name;
    

    void Update()
    {
        
    }
    public AIPrototype getClone()
    {
        //credit to https://www.javatpoint.com/prototype-design-pattern for showing us how to implement a prototype design pattern
        return new AIPlayer(name);
    }

    public AIPlayer(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        Debug.Log("get name");
        return this.name;
    }
    public void setName(string newName)
    {
        name = newName;
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
        }
        Debug.Log("size of possible cards: " + possibleCards.Count);
        if (possibleCards.Count == 0)
        {
            //no possible cards to play
            Debug.Log("AI has no cards to play -> drawing new card");
            this.drawCard(); //have to grab a new card b/c there are no other available options
            tempGame.gameInstance.setAIDrewCard(true);
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

        for(int i = 0; i < 7; i++){ //creates a deck of 7 starting out for each AI player
            this.drawCard();
        }
    }

    public int cardCount(){
        return this.getCurrentHand().Count; 
    }

    private void addToCurrentHand(Deck newCard)
    {
        currentHand.Add(newCard); //adds card to deck
    }
    
    public void drawCard(){
        Debug.Log("size of deck: " + Deck.deckInstance.getDeck().Count);
        if (Deck.deckInstance.getDeck().Count < 8) //refresh deck if not many cards left
        {
            Deck.deckInstance.createDeck();
            Deck.deckInstance.shuffleDeck();
            Debug.Log("size of deck: " + Deck.deckInstance.getDeck().Count);
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

        Deck.deckInstance.removeCard(); //since card was added to someone's current hand, need to remove from available deck
    }

    public void playCard(Deck card){
        int playedCard = 0;
        for(int i = 0; i < this.getCurrentHand().Count; i++)
        {
            //checks input against all the cards in their deck
            if(card.MyColor.ToString() + card.MyValue.ToString() == this.getCurrentHand()[i].MyColor.ToString() + this.getCurrentHand()[i].MyValue.ToString()){
                playedCard = i;
            }
        }
        if(tempGame.gameInstance.validCard(getCurrentHand()[playedCard]))
        {
            //confirms that chosen card is valid to play -> same color, number, wildcard
            string givenCardName = getCurrentHand()[playedCard].MyColor.ToString() + getCurrentHand()[playedCard].MyValue.ToString();
            updatePreviousCard(playedCard, givenCardName); //for UI
            getCurrentHand().RemoveAt(playedCard); //since played card, remove that from current hand
        }
        else
        {
            Debug.Log("Invalid Card");
        }
        if(this.getCurrentHand().Count == 1){
            tempGame.gameInstance.unoButton(); //only reveals uno button if there is one card left
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
        //function is meant to update UI
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
        tempGame.gameInstance.unoButton();
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

    public void notifyObserver() {
        observer.uno();
    }



}
