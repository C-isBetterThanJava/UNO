using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayer3 : AIPlayer
{
    public static AIPlayer AIPlayerThree;
    //public Text AI3CardCount;
    
    // private List<Deck> currentHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AI3CardCount.text = cardCount().ToString();
    }

    void Awake(){
        AIPlayerThree = this;
        AIPlayerThree.name = "AI2";
        AIPlayerThree.setCurrentHand(new List<Deck>());
        AIPlayerThree.createHand();
    }



}
