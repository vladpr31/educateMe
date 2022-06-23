using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreControl : MonoBehaviour
{
    public static int scoreValue = 0;
    public GameObject[] dialogueObjects; //Dialogue for after the player collected the 3 objects.
    Text score;
    void Start()
    {
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = "Items Collected: " + scoreValue.ToString() + " / 3"; //Text of the score.
        if(scoreValue==3)
        {
            for(int i = 0; i < dialogueObjects.Length; i++) //Loop to activate all of the instances of the Dialogue.
            {
                dialogueObjects[i].SetActive(true);
            }
            nextMapScene.triggered = true; //Sets trigger for invisible wall in nextMapScene.cs
        }
    }
}
