// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class humanPlayer : Player
// {
//     public humanPlayer() {}  
//     private static humanPlayer hPlayer = null;

//     void Awake(){
//         hPlayer = humanPlayerInstance; //creating the one and only human player(this should only happen once)
//         Debug.Log("Human Player Object created");
//     }

    

//     public static humanPlayer humanPlayerInstance {  
//         get {  
//             if (hPlayer == null) {  
//                 hPlayer = new humanPlayer();  
//             }
//             return hPlayer;  
//         }  
//     }

//     public List<UnoCard> createHand(){
//         for(int i = 0; i < 7; i++){
//             this.drawCard();
//         }
//         return getCurrentHand();
//     }

//     public void createPlayer(string givenName){
//         humanPlayer hPlayer = humanPlayerInstance;
//         this.name = givenName;
//         hPlayer.getCurrentHand();
//         Debug.Log("human player instance created");
//         // generateHand();
//         Debug.Log("Creating Hand");
//         // for(int i = 0; i < 7; i++)
//         // {
            
//         //     drawCard();
//         // }
//         // for(int i = 0; i < 7; i++){
//         //     Debug.Log(hPlayer.currentHand[i]);
//         // }
//     }
// }
