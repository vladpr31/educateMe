using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class quizGame : MonoBehaviour
{

    public List<quizQA> QA; //List of Questions and Answers.
    public GameObject dialoguePanel,dialogueTextPanel,dialogueController; //Dialogues Holders.
    public GameObject quizPanel; //Panel of the question and answer just to set active or inactive.
    public GameObject[] options; //Options for answers.
    public int currentQuestion; //the index of the correct answer from the Options array.
    public TextMeshProUGUI questionText; //Question itself.
    public Sprite defaultSprite; //Sprite of default boxes (Of Options buttons).
    //public AudioSource audioData;
    private void Start()
    {
        questionGenerator(); //Generates question.
    }
    public void answeredCorrectly() //if answered correctly generates next question.
    {
        if (QA[currentQuestion] != null)
        {
            QA.RemoveAt(currentQuestion); //we remove the question so it wont repeat and generate a new one.

            questionGenerator();
        }
    }
    void setAnswer() //sets the answers for each question, default is 4 answers as we have 4 Buttons.
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<quizAnswers>().correct = false; //We make all the questions set to 'Incorrect' at the start of game.
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QA[currentQuestion].Answers[i]; //takes text from the Lista of QAs.
            if(QA[currentQuestion].correct==i)
            {
                options[i].GetComponent<quizAnswers>().correct = true; //if answer is correct sets the correct to True.
            }
            options[i].GetComponent<Button>().image.sprite = defaultSprite;

        }
    }
    void questionGenerator() //Generates Question randomly.
    {
        if(QA.Count>0)
        {
            currentQuestion = Random.Range(0, QA.Count); //Randomly takes a question from the questions we have.
            questionText.text = QA[currentQuestion].Question; //Takes the Text of question from thr QA list.
            setAnswer();  
        }
        else //when all questions are done and answered calls the dialogue.
        {
            quizPanel.SetActive(false);
            dialoguePanel.SetActive(true);
            dialogueTextPanel.SetActive(true);
            dialogueController.SetActive(true);
        }
        
    }
}
