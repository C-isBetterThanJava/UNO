using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayer1 : AIPlayer
{
    public static AIPlayer AIPlayerOne;
    public Text AI1CardCount;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AI1CardCount.text = cardCount().ToString();
    }

    void Awake(){
        AIPlayerOne = this;
        AIPlayerOne.name = "AI1";
        AIPlayerOne.setCurrentHand(new List<Deck>());
        AIPlayerOne.createHand();
        
        Debug.Log("size of AI1 hand: " + AIPlayerOne.getCurrentHand().Count);
        /*for(int i = 0; i < AIPlayerOne.getCurrentHand().Count; i++)
        {
            Debug.Log("AI1 current card: " + AIPlayerOne.getCurrentHand()[i].MyColor);
        }*/
    }



    // public List<Deck> getCurrentHand(){
    //     return currentHand;
    // }
}
