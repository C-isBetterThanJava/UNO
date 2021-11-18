using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class tempGame : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    private int currPlayerTurn;
    private bool directionOfPlay;
    private int numAI = 3;
    private List<tempPlayer> players;
    private tempHumanPlayer human;
    private int numTurns;
    public static tempGame gameInstance;

    void Awake(){
        gameInstance = this;
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
    

}
