using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gControl : MonoBehaviour
{
    private static int diceValue;
    private static bool finished = false;

    public static bool gameOver() //checks if we reached last position.
    {
        return finished;
    }
    public static void setDiceValue(int val) //set for the dice value.
    {
        diceValue = val;
    }
    public static int getDiceValue() //get the dice value.
    {
        return diceValue;
    }
    public static void plusDiceValue(int val)
    {
        diceValue += val;
    }
}
