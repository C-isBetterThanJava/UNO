using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayer : Player 
{
// MonoBehaviour
    private string name;
    public int numberOfAI = 3;
    public Text NumberOfPlayers = null;
    private string[] names = new string[] {"Max Verstappen", "Lewis Hamilton", "Valtteri Bottas", "Lando Norris", "Sergio Perez", "Carlos Sainz", "Charles Leclerc", "Daniel Ricciardo", "Pierre Gasly", "Fernando Alonso", "Esteban Ocon", "Sebastian Vettel", "Lance Stroll", "Yuki Tsunoda", "George Russell", "Nicholas Latifi", "Kimi Räikkönen", "Antonio Giovinazzi", "Mick Schumacher", "Nikita Mazepin"};

    public AIPlayer(string givenName){
        this.name = givenName;
    }
    // public void createAI(){
    //     AIPlayer AIplayer1 = new AIPlayer();
    //     Debug.Log("AI player instance created");
    //     AIPlayer AIplayer2 = new AIPlayer();
    //     Debug.Log("AI player instance created");
    //     AIPlayer AIplayer3 = new AIPlayer();
    //     Debug.Log("AI player instance created");
    // }

    public string grabRandomName(){
        return names[0];
    }

    public string[] getNames(){
        return names;
    }

    // public void Increment()
    // {
    //     if (NumberOfPlayers != null)
    //     {
    //         numberOfAI++;
    //         Debug.Log(numberOfAI);
    //         NumberOfPlayers.text = numberOfAI.ToString();
    //     }   
    // }

    // public void Decrement()
    // {
    //     if (NumberOfPlayers != null)
    //     {
    //         numberOfAI--;
    //         Debug.Log(numberOfAI);
    //         NumberOfPlayers.text = numberOfAI.ToString();
    //     } 
    // }




}
