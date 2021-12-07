using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcer : MonoBehaviour, unoTracker
{
    // Start is called before the first frame update
    public Text announcer;
    public Text currentColor;
    public Text currentPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void update(string action){
    //     announcer.text = action;
    // }

    public void uno(){
        announcer.text = "Uno has been called by " + tempGame.gameInstance.getCurrPlayerTurn();
    }

    public void color(string color){
        currentColor.text = color;
    }

    public void player(string player){
        currentPlayer.text = player;
    }



}
