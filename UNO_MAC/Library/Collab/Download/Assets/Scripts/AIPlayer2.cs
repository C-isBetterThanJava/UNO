using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayer2 : AIPlayer
{

    public static AIPlayer AIPlayerTwo;
    //public Text AI2CardCount;
    // private List<Deck> currentHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AI2CardCount.text = cardCount().ToString();
    }

    void Awake(){
        AIPlayerTwo = this;
        AIPlayerTwo.name = "AI2";
        AIPlayerTwo.setCurrentHand(new List<Deck>());
        AIPlayerTwo.createHand();
    }


    // public List<Deck> getCurrentHand(){
    //     return currentHand;
    // }
}
