using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<UnoCard> currentHand;
    public string name;
    // public List<Player> activePlayers;

    // public void addPlayer(Player player){
    //     activePlayers.Add(player);
    // }

    // public void generateHand(){
    //     for(int i = 0; i < 7; i++){
    //         drawCard();
    //     }
    // }
    public void setName(string n)
    {
        name = n;
    }
    public string getName()
    {
        return name;
    }
    public List<UnoCard> getCurrentHand()
    {
        return currentHand;
    }

    public void drawCard(){
        DeckOfCards deck = DeckOfCards.originalDeck; //create the deck
        currentHand.Add(deck.getDeck[0]);
        Debug.Log(deck.getDeck[0]);
        deck.getDeck.Remove(deck.getDeck[0]);
        for (int i = 0; i < currentHand.Count; i++)
        {
           Debug.Log(currentHand[i]);
        }
    }

    public void playCard(UnoCard cardSelected, Game game){
        DeckOfCards deck = DeckOfCards.originalDeck;
        
        if(cardSelected.MyColor == deck.getLastPlayed().MyColor || (int)cardSelected.MyValue > 12 || game.getNumTurns() == 0 || game.getNumTurns() == 0)
        { //same color or wild/wild+4
            for(int i = 0; i < currentHand.Count; i++){
                if(currentHand[i] == cardSelected){
                    currentHand.Remove(cardSelected);
                    deck.setLastPlayed(cardSelected);
                    break;
                }
            }
        }
        else{
            Debug.Log("Invalid Card");
        }
    }

}
