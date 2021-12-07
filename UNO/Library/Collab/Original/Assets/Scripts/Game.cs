using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{
    // public string[] deck = new string[5][];
    // public string[] playedCards = new string[];
    private int currPlayerTurn = 0;
    private bool directionOfPlay = true;
    private Game() {}  
    private static Game instance = null;  


    void Awake(){
        Game game = GameInstance; //create game object (this should only happen once)
        Debug.Log("Game Object Created");
        DeckOfCards deck = DeckOfCards.originalDeck; //create the deck
        Debug.Log("Deck Created");
        Debug.Log("Setting up deck");
        deck.setUpDeck(); //set up the deck (this should only happen once)
        Debug.Log("Deck set up Complete");
    }

    public void LoadGame(){
        Game game = GameInstance;
        // game.createDeck(); This aint working lmao NullReferenceException: Object reference not set to an instance of an object
        // Debug.Log("Game deck created");
        game.setCurrPlayerTurn(0);
        Debug.Log("Game turn set");
        game.setDirectionOfPlay(true);
        Debug.Log("Game play of direction set");
    }
    

    //this is a singleton
    public static Game GameInstance {  
        get {  
            if (instance == null) {  
                instance = new Game();  
            }  
            return instance;  
        }  
    }

    void setDirectionOfPlay(bool direction){
        this.directionOfPlay = direction;
    }

    bool getDirectionOfPlay(){
        return this.directionOfPlay;
    }

    void setCurrPlayerTurn(int player){
        this.currPlayerTurn = player;
    }

    int getCurrPlayerTurn(){
        return this.currPlayerTurn;
    }

    



    //Guide 
    //Index [0] = Red
    //Index [1] = Green
    //Index [2] = Blue
    //Index [3] = Yellow
    //Index [4] = Wild

    // void createDeck(){
    //     Console.WriteLine("Here");
    //     int counter = 1;
    //     for(int i = 0; i < 19; i++){
    //         if(i - 2 == counter){
    //             counter++;
    //         }
    //         if(i == 0){
    //             deck[0][i] = i.ToString();
    //             deck[1][i] = i.ToString();
    //             deck[2][i] = i.ToString();
    //             deck[3][i] = i.ToString();
    //         }
    //         else{
    //             deck[0][i] = counter.ToString();
    //             deck[1][i] = counter.ToString();
    //             deck[2][i] = counter.ToString();
    //             deck[3][i] = counter.ToString();
    //         }
    //     }
    // }


    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
