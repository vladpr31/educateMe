using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Calculate : MonoBehaviour
{
    public static string move_op;
    
    public void funcplus()
    {
        move_op = "+";
        LoadMainMenu();

    }
    public void funcminus()
    {
        move_op = "-";
        LoadMainMenu();

    }
    public void funcmulti()
    {
        move_op = "*";
        LoadMainMenu();

    }
    public void funcdivide()
    {
        move_op = "/";
        LoadMainMenu();
    }


    public void LoadMainMenu()
    {
        leaderBoard.gameDone = true;
        SceneManager.LoadScene("mathGame");
    }


}
