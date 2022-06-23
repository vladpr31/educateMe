using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    [SerializeField] AudioClip[] girlVoice,boyVoice;
    public GameObject sceneMngr;
    public static bool enabledInput = true; //Enables or Disables Input while text animation running.
    public TextMeshProUGUI dialogueText; //the Text which plays.
    public string[] Sentences; //array of the sentences to be played.
    public string[] girlSentences; //array of sentences to be played for Girl.
    private int index = 0; //index of the array above
    public float dialogueSpeed; // speed of the dialogue.
    public Animator dialogueAnimator; //the animation of the dialogue box
    private bool startDialogue = true; //bool for dialogue to pop on and off.
    public GameObject mouseIndicator;
    private IEnumerator Start()
    {
        
        if(sceneTransition.getGender() =="Girl") { Sentences = girlSentences; } //If Girl Gender chosen, a girl Dialogue will be initialized.
        if (startDialogue)
        {
            dialogueAnimator.SetTrigger("Enter"); //Starts animation of dialogue.
            startDialogue = false; 
            enabledInput = false; //doesnt allows player to skip/use input when dialogue plays.
            yield return new WaitForSeconds(2f);
            nextSentence();
        }
    }
    void Update()
    {
        if (enabledInput) //at start input is allowed.
        {
            if (Input.GetMouseButtonDown(0)) //mouse click to start the animation.
            {
                if (startDialogue)
                {
                    dialogueAnimator.SetTrigger("Enter");
                    startDialogue = false;
                    
                }
                else
                {
                    enabledInput = false;
                    nextSentence();
                }

            }
        }
    }
    void nextSentence() //checks if there is any sentence to play next.
    {
        
        if (index < Sentences.Length )
        {
            dialogueText.text = "";
            StartCoroutine(writeSentence()); //starts courtine of the sentence.
            
        }
        else //when no more sentences exit dialogue animation, and disables mouseIndicator and activates scene transition.
        {
            dialogueText.text = "";
            dialogueAnimator.SetTrigger("Exit");
            mouseIndicator.SetActive(false);
            sceneMngr.SetActive(true);
        }
    }

    IEnumerator writeSentence() //function to write sentences
    {
        if (boyVoice.Length == 0) { boyVoice = girlVoice; }
        if (sceneTransition.getGender() != "Girl") { girlVoice = boyVoice; }
        if (girlVoice.Length > 0 && toggleVoice.voiceStatus==true)
        {
            AudioSource.PlayClipAtPoint(girlVoice[index], new Vector3(0, 0, -10), 1f);
        }
        //yield return new WaitForSeconds(1f);
        foreach (char Charachter in Sentences[index].ToCharArray()) //print char by char for "Talking" animation.
        {
            dialogueText.text += Charachter; //Char by Char "Animation".
            yield return new WaitForSeconds(dialogueSpeed); //Dialogue Speed if we change this the dialogue goes faster.
            mouseIndicator.SetActive(false); //Mouse Indicator for when "Talking" is finished, only visible when Text is Done.
        }
        if (girlVoice.Length > 0 && toggleVoice.voiceStatus == true)
        {
            yield return new WaitForSeconds(girlVoice[index].length-1f);
        }
        mouseIndicator.SetActive(true); //Indicates that the player can proceed to the next Dialogue if there is.
        enabledInput = true; //enables mouse click.
        index++;
        
    }

}

/*
 This is the Dialogue Controller Script, this scripts is responsable for the "Playing Dialogue" where the Girl\Boy Talk or Interact 
 with the player.
 */
