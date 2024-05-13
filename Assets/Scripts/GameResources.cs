using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    public static event EventHandler OnCoinAmountChanged;
    private static int coinsAmount;

    public static void AddCoinsAmount(int amount)
    {
        coinsAmount += amount;
        if (OnCoinAmountChanged != null)
            OnCoinAmountChanged(null, EventArgs.Empty);

    }

    public static int GetCoinsAmount()
    {
        return coinsAmount;
    }
}
