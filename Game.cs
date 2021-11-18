// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using System;

// public class Game : MonoBehaviour
// {
//     // public string[] deck = new string[5][];
//     // public string[] playedCards = new string[];
//     private int currPlayerTurn = 0;
//     private bool directionOfPlay = true;
//     private Game() {}  
//     private static Game instance = null;
//     private List<Player> players; //list where all the players reside
//     private int numAI = 3; //default w/ 3 AI players
//     private humanPlayer human;
//     private int numTurns = 0;


//     public void setNumTurns(int turns)
//     {
//         numTurns += turns;
//     }
//     public int getNumTurns()
//     {
//         return numTurns;
//     }
//     public void setHuman(humanPlayer person)
//     {
//         human = person;
//     }
//     public humanPlayer getHuman()
//     {
//         return human;
//     }
//     public void setNumAI(int num)
//     {
//         numAI = num;
//     }
//     public int getNumAI()
//     {
//         return numAI;
//     }
//     private void setPlayers(Player newPlayer)
//     {
//         players.Add(newPlayer);
//     }
//     private List<Player> GetPlayers()
//     {
//         return players;
//     }


//     void Awake(){
//         Game game = GameInstance; //create game object (this should only happen once)
//         Debug.Log("Game Object Created");
//     }

//     public void LoadGame(){
//         Game game = GameInstance;
//         // game.createDeck(); This aint working lmao NullReferenceException: Object reference not set to an instance of an object
//         // Debug.Log("Game deck created");
//         game.setCurrPlayerTurn(0);
//         Debug.Log("Game turn set");
//         game.setDirectionOfPlay(true);
//         Debug.Log("Game play of direction set");
//     }
    

//     //this is a singleton
//     public static Game GameInstance {  
//         get {  
//             if (instance == null) {  
//                 instance = new Game();  
//             }  
//             return instance;  
//         }  
//     }

//     void setDirectionOfPlay(bool direction){
//         this.directionOfPlay = direction;
//     }

//     bool getDirectionOfPlay(){
//         return this.directionOfPlay;
//     }

//     void setCurrPlayerTurn(int player){
//         this.currPlayerTurn = player;
//     }

//     int getCurrPlayerTurn(){
//         return this.currPlayerTurn;
//     }

//     public void runGame()
//     {
//         //need to instantiate deck
//         //set up deck - idk if this is handled in Awake function? check w/ Seok about following 3 lines
//         Game game = GameInstance; //create game object (this should only happen once)
//         List<UnoCard> deck = DeckOfCards.deck; //create the deck
//         deck.setUpDeck(); //set up the deck (this should only happen once)

//         game.setUpPlayers(game); //AI and human player have been instantiated

//         //now all players need to receive their own hand of cards - 7 for each
//         game.drawStartingHand(game);

//         //for easy start -> human player will begin the round
//         //idk how to use onClick to choose a card -> assuming that the player will be choosing a UnoCard object
        
//         bool gameOver = false;
//         while(gameOver == false)
//         {
//             game.playOneRound(game);
//         }
        

//     }

//     public bool playOneRound(Game game)
//     {
//         //need to have a check to make sure that the played card is not a reverse

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
//     }

//     public void drawStartingHand(Game game)
//     {
//         //new hand of 7 for each AI player and human
//         foreach(Player playah in game.players)
//         {
//             for (int i = 0; i < 7; i++)
//             {
//                 //why can't an AIPlayer use the functions from Player? -> ask Seok if it's okay that I switched AIPlayer to inherit from Player instead of MonoBehaviour
//                 playah.drawCard();
//             }
//         }
        
//     }

//     public void setUpPlayers(Game game)
//     {
//         //need to instantiate the players - 1 humanPlayer, dependent # of AI players
//         //ask Seok how to access the created humanPlayer
//         humanPlayer human = new humanPlayer(); //do we want to add name, etc. here? 
//         setHuman(human);
//         setPlayers(human);


//         int totalAI = getNumAI();
//         if (totalAI == 2) //generate diff number of AI players based off of user preference
//         {
//             AIPlayer AIPlayer1 = new AIPlayer();
//             AIPlayer1.setName("AIPlayer1");
//             setPlayers(AIPlayer1);
//             AIPlayer AIPlayer2 = new AIPlayer();
//             AIPlayer2.setName("AIPlayer2");
//             setPlayers(AIPlayer2);
//         }
//         else if (totalAI == 3)
//         {
//             AIPlayer AIPlayer1 = new AIPlayer();
//             AIPlayer1.setName("AIPlayer1");
//             setPlayers(AIPlayer1);
//             AIPlayer AIPlayer2 = new AIPlayer();
//             AIPlayer2.setName("AIPlayer2");
//             setPlayers(AIPlayer2);
//             AIPlayer AIPlayer3 = new AIPlayer();
//             AIPlayer3.setName("AIPlayer3");
//             setPlayers(AIPlayer3);
//         }
//         else
//         {
//             AIPlayer AIPlayer1 = new AIPlayer();
//             AIPlayer1.setName("AIPlayer1");
//             setPlayers(AIPlayer1);
//             AIPlayer AIPlayer2 = new AIPlayer();
//             AIPlayer2.setName("AIPlayer2");
//             setPlayers(AIPlayer2);
//             AIPlayer AIPlayer3 = new AIPlayer();
//             AIPlayer3.setName("AIPlayer3");
//             setPlayers(AIPlayer3);
//             AIPlayer AIPlayer4 = new AIPlayer();
//             AIPlayer4.setName("AIPlayer4");
//             setPlayers(AIPlayer4);
//         }
//     }
// }
