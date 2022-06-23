using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollDice : MonoBehaviour
{
    [SerializeField] Sprite[] dices;
    private SpriteRenderer render;
    private bool courtineAllowed = true;
    [SerializeField] GameObject[] Player;
    private void Start()
    {
        render = GetComponent<SpriteRenderer>(); //initialize render of the sprite.
        render.sprite = dices[5]; //initialize dices.
        if(sceneTransition.getGender() == "Girl") //if the chosen gender in the begining of the game is "Girl" then activated the "Girl" character.
        {
            Player[0].SetActive(false); //deactivate boy character.
            Player[1].SetActive(true); //activate girl character.
        }
        else
        {
            Player[0].SetActive(true); //activate boy character.
            Player[1].SetActive(false); //deactivate girl character.
        }
    }

    private void OnMouseDown() //on mouse click function for the dice.
    {
        if(!gControl.gameOver() && courtineAllowed) //if game is not over and courtine is allowed then roll => courtine is for
                                                    // if player is still moving so we cant dice roll.
        {
            StartCoroutine("roll");
        }
    }
    

    private IEnumerator roll() //rolls the dice.
    {
        courtineAllowed = false; 
        int randDice = 0;
        for(int i =0; i<=10; i++) //random dice roll, the loop is for "Lazy Animation" of rolling the dice.
        {
            randDice = Random.Range(0, 6);
            render.sprite = dices[randDice];
            yield return new WaitForSeconds(0.05f);
        }

        gControl.plusDiceValue(randDice + 1); //sets the dice value => how much player walks.
        

        if (sceneTransition.getGender() == "Girl")
        {
            Player[1].GetComponent<moveChar>().setAllowance(true); //allows player to move.
        }
        else
        {
            Player[0].GetComponent<moveChar>().setAllowance(true); //allows player to move.
        }
        courtineAllowed=true;
    }
}
