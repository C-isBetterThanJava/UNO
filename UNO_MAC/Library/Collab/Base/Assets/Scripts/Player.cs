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



    public void drawCard(){
        DeckOfCards deck = new DeckOfCards();
        //for (int i = 0; i < currentHand.Count; i++)
        //{
         //   Debug.Log(currentHand[i]);
        //}
        currentHand.Add(deck.getDeck[0]);
        Debug.Log(deck.getDeck[0]);
        deck.getDeck.Remove(deck.getDeck[0]);
        //for (int i = 0; i < currentHand.Count; i++)
        //{
         //   Debug.Log(currentHand[i]);
        //}
    }

    public void playCard(UnoCard cardSelected){
        DeckOfCards deck = new DeckOfCards();

        if(cardSelected.MyColor == deck.getLastPlayed().MyColor || (int)cardSelected.MyValue > 12){ //same color or wild/wild+4
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
