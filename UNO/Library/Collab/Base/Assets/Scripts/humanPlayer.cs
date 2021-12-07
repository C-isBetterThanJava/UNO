using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanPlayer : Player
{
    private humanPlayer() {}  
    private static humanPlayer instance = null; 

    void Awake(){
        humanPlayer hPlayer = humanPlayerInstance; //creating the one and only human player(this should only happen once)
    }

    public static humanPlayer humanPlayerInstance {  
        get {  
            if (instance == null) {  
                instance = new humanPlayer();  
            }  
            return instance;  
        }  
    }

    public void createPlayer(string givenName){
        this.name = givenName;
        Debug.Log("human player instance created");
        // generateHand();
        for(int i = 0; i < 7; i++)
        {
            drawCard();
        }
    }
}
