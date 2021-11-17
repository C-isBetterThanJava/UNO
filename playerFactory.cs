using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFactory : MonoBehaviour
{
    static public Player create(string name){
        switch(name){
            default:
                return new humanPlayer(name);
            case "Max Verstappen":
                return new AIPlayer(name);
            case  "Lewis Hamilton":
                return new AIPlayer(name);
            case "Pierre Gasly":
                return new AIPlayer(name);
            case "Antonio Giovinazzi":
                return new AIPlayer(name);
        }
    }
}
