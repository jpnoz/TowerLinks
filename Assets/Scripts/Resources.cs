using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    
    // making a struct for all the resources
    public struct resource
    {
        public int _coins;

        // get method for coins

        public int coins { get { return coins; } }

        // constructor

        public resource(int startingCoins)
        {
            _coins = startingCoins;
        }
    }
}
