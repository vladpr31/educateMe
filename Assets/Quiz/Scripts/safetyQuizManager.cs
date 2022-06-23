using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class safetyQuizManager : MonoBehaviour
{
    
#pragma warning disable 649
    //ref to the QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;
    //ref to the scriptableobject file
    [SerializeField] private List<safetyQuizDataScriptable> quizDataList;
    //[SerializeField] private float timeInSeconds;
    [SerializeField] GameObject[] dialogueItems;
    [SerializeField] GameObject gamePanel;
#pragma warning restore 649

    private string currentCategory = "";
    private int correctAnswerCount = 0;
    //questions data
    private List<Question> questions;
    //current question data
    private Question selectedQuetion = new Question();
    private int gameScore,wrongScore;
    
    //private int lifesRemaining;
    //private float currentTime;
    private safetyQuizDataScriptable dataScriptable;

    private safetyGameStatus gameStatus = safetyGameStatus.NEXT;

    public safetyGameStatus GameStatus { get { return gameStatus; } }

    public List<safetyQuizDataScriptable> QuizData { get => quizDataList; }
    
    public void StartGame(int categoryIndex, string category)
    {
        currentCategory = category;
        correctAnswerCount = 0;
        gameScore = 0;
        wrongScore = 0;
        //lifesRemaining = 3;
        //currentTime = timeInSeconds;
        //set the questions data
        questions = new List<Question>();
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);
        //select the question
        SelectQuestion();
        gameStatus = safetyGameStatus.PLAYING;
    }

    /// <summary>
    /// Method used to randomly select the question form questions data
    /// </summary>
    private void SelectQuestion()
    {
        //get the random number
        int val = UnityEngine.Random.Range(0, questions.Count);
        //set the selectedQuetion
        selectedQuetion = questions[val];
        //send the question to quizGameUI
        quizGameUI.SetQuestion(selectedQuetion);
        questions.RemoveAt(val);
    }




    /// <summary>
    /// Method called to check the answer is correct or not
    /// </summary>
    /// <param name="selectedOption">answer string</param>
    /// <returns></returns>
    public bool Answer(string selectedOption)
    {
        //set default to false
        bool correct = false;
        //if selected answer is similar to the correctAns
        if (selectedQuetion.correctAns == selectedOption)
        {
            //Yes, Ans is correct
            correctAnswerCount++;
            correct = true;
            gameScore += 1;
            quizGameUI.ScoreText.text = gameScore + ":תונוכנ תובושת";
        }
        else
        {
            wrongScore += 1;
            quizGameUI.WrongScoreText.text = wrongScore + ":תונוכנ אל תובושת";
        }

        if (gameStatus == safetyGameStatus.PLAYING)
        {
            if (questions.Count > 0 && correct)
            {
                //call SelectQuestion method again after 1s
                Invoke("SelectQuestion", 0.5f);
            }
            else if(questions.Count <= 0)
            {
                GameEnd();
            }
        }
        //return the value of correct bool
        return correct;
    }

    private void GameEnd()
    {
        gameStatus = safetyGameStatus.NEXT;

        for(int i=0;i<dialogueItems.Length;i++)
        {
            dialogueItems[i].SetActive(true);
        }
        gamePanel.SetActive(false);
        //PlayerPrefs.SetInt(currentCategory, correctAnswerCount); //save the score for this category
    }
}

//Datastructure for storeing the quetions data
[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public AudioClip audioClip;         //audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //video for video type
    public List<string> options;        //options to select
    public string correctAns;           //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

[SerializeField]
public enum safetyGameStatus
{
    PLAYING,
    NEXT
}