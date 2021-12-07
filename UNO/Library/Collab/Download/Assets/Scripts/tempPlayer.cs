using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    private List<Deck> currentHand;
    private Text name;
    private Text username;
    private Text password;

    void Awake(){
        currentHand = new List<Deck>();
        createHand();
    }

    public void setUsername(Text givenUsername){
        username = givenUsername;
    }

    public Text getUsername(){
        return username;
    }

    public void setPassword(Text givenPassword){
        password = givenPassword;
    }

    public Text getPassword(){
        return username;
    }

    public Text getName(){
        return name;
    }

    public void createHand(){
        for(int i = 0; i < 7; i++){
            this.drawCard();
        }
        // List<Deck> deck = Deck.deckInstance.getDeck();
        // for(int i =0; i < deck.Count; i++){
        //     Debug.Log(deck[i].MyColor);
        //     Debug.Log(deck[i].MyValue);
        // }
    }

    public List<Deck> getCurrentHand(){
        return currentHand;
    }

    public void drawCard(){
        List<Deck> deck = Deck.deckInstance.getDeck();
        this.currentHand.Add(deck[0]);
        Debug.Log(deck[0].MyColor);
        Debug.Log(deck[0].MyValue);
        Deck.deckInstance.removeCard();
    }

    public void playCard(Deck card){
        List<Deck> deck = Deck.deckInstance.getDeck();
        if(card.MyColor == Deck.deckInstance.getLastPlayed().MyColor || (int)card.MyValue > 12 || tempGame.gameInstance.getNumTurns() == 0 || tempGame.gameInstance.getNumTurns() == 0){
            for(int i = 0; i < deck.Count; i++){
                //we have to check for uno here
                if(deck[i] == card){
                    this.currentHand.RemoveAt(i);
                    Deck.deckInstance.setLastPlayed(card);
                    break;
                }
            }
        }
        else{
            Debug.Log("Invalid Card");
        }
    }

}
