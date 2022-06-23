using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class hintControl : MonoBehaviour
{
    [SerializeField] Button[] characters;
    [SerializeField] Button hintButton;
    [SerializeField] Text buttonText;
    [SerializeField] AudioClip soundFx;
    [SerializeField] string gameName;
    private static int hintsCount = 0;
    private string answer;
    private int mathAnswer;
    private bool pressed = false;

    private void Start()
    {
        hintsCount = 0;
    }
    public void OnMouseDown()
    {
            
            //Words Game Hints.
            if (gameName == "wordsGame")
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    answer = QuizManager.instance.getCurrentAnswer();
                    if (answer.Contains(characters[i].GetComponentInChildren<TextMeshProUGUI>().text))
                    {
                        if (characters[i].image.color == Color.yellow && pressed!=true) { characters[i].image.color = Color.white; }
                        else 
                        { 
                            characters[i].image.color = Color.yellow;
                            AudioSource.PlayClipAtPoint(soundFx, new Vector3(0, 0, 0));
                        }
                    }
                }
            }
            //Math Game Hints.
            if (gameName == "mathGame")
            {
                if (hintsCount != 3)
                {
                    if (hintButton.image.color == Color.white && pressed !=true )
                    {
                        pressed = true;
                        hintButton.image.color = Color.yellow;
                        hintsCount += 1;
                        buttonText.text = hintsCount.ToString() + "/3";
                    }
                    else
                    {
                        hintButton.image.color = Color.white;
                    }
                    for (int i = 0; i < characters.Length; i++)
                    {
                        mathAnswer = operator_manger.rightAnswer;
                        Debug.Log(mathAnswer);
                        if (characters[i].GetComponentInChildren<TextMeshProUGUI>().text == mathAnswer.ToString())
                        {
                            if (characters[i].image.color == Color.yellow && pressed!=true) { characters[i].image.color = Color.white; }
                            else 
                            { 
                                characters[i].image.color = Color.yellow;
                                AudioSource.PlayClipAtPoint(soundFx, new Vector3(0, 0, 0));
                            }
                        }
                    }
                }
            }
    }
    public void resetHints()
    {
        if (hintsCount != 3) { hintButton.image.color = Color.white; pressed = false; }
        else { if (gameName != "wordsGame") { hintButton.image.color = Color.red; } }
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].image.color = Color.white;
        }
    }
    public void resetHintsCount()
    {
        hintsCount = 0;
    }
}
