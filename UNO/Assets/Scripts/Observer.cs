using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public interface unoTracker<T>
// {
//     void update(T action);
// }

public interface unoTracker
{
    //This is the observer pattern
    void uno();
    void color(string color);
    void player(string player);
}
