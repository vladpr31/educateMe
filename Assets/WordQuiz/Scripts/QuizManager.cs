using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class QuizManager : MonoBehaviour
{
    public static QuizManager instance; //Instance to make is available in other scripts without reference
    [SerializeField] private GameObject gameComplete;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataScriptable questionDataScriptable;
    [SerializeField] private Image questionImage;           //image element to show the image
    [SerializeField] private WordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private WordData[] optionsWordList;    //list of options word in the game
    [SerializeField] GameObject particles1, paritcles2;
    [SerializeField] AudioClip particlesFX;
    private hintControl hc;
    private List<char> hebrewChars;
    int score;
    public static int currentQuestionIndex = 0;
    //Encoding latinEncoding = Encoding.GetEncoding("Windows-1252");
    //Encoding hebrewEncoding = Encoding.GetEncoding("Windows-1255");


    private GameStatus gameStatus = GameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[12];               //array which store char of each options

    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update


    void Start()
    {
        hebrewChars = new List<char> { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ך', 'ל', 'מ', 'ם', 'נ',
            'ן', 'ס', 'ע', 'פ', 'ף', 'צ', 'ץ', 'ק', 'ר', 'ש', 'ת' };
        selectedWordsIndex = new List<int>();           //create a new list at start
        SetQuestion();                                  //set question
        score = 0;
        particles1.SetActive(false);
        paritcles2.SetActive(false);
        questionDataScriptable.questions = questionDataScriptable.questions.OrderBy(i => Guid.NewGuid()).ToList();
    }

    void SetQuestion()
    {
        Debug.Log(currentQuestionIndex);
       if (currentQuestionIndex == questionDataScriptable.questions.Count && getMap.getMapID() == 5)
       {
            currentQuestionIndex = 0;
       }
        particles1.SetActive(false);
        paritcles2.SetActive(false);
        gameStatus = GameStatus.Playing;                //set GameStatus to playing 

        //set the answerWord string variable
        answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
        Debug.Log(answerWord);
        //set the image of question
        questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;
            
        ResetQuestion();                               //reset the answers and options value to orignal     

        selectedWordsIndex.Clear();                     //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array

        //add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {

            wordsArray[i] = char.ToUpper(answerWord[i]);
            //Debug.Log(wordsArray[i]);
        }

        //add the dummy char to wordsArray
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            //wordsArray[j] = (char)UnityEngine.Random.Range(224, 251);

            //byte[] latinBytes = latinEncoding.GetBytes(((char)UnityEngine.Random.Range(224, 251)).ToString());     

            //string hebrewString = hebrewEncoding.GetString(latinBytes);
            char character = hebrewChars[UnityEngine.Random.Range(0,hebrewChars.Count)];

            wordsArray[j] = character;
            //Debug.Log(character);


        }

        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array

        //set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }

    }

    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
        //activate all the answerWordList gameobject and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetWord('_');
        }

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(false);
        }

        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
        }

        currentAnswerIndex = 0;
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(WordData value)
    {
        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            correctAnswer = true;   //default value
            //loop through answerWordList
            for (int i = 0; i < answerWord.Length; i++)
            {
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                {
                    correctAnswer = false; //set it false
                    break; //and break from the loop
                }
            }

            //if correctAnswer is true
            if (correctAnswer)
            {
                score = score + 1;
                hc = GameObject.FindObjectOfType(typeof(hintControl)) as hintControl;
                hc.resetHints();
                particles1.SetActive(true);
                paritcles2.SetActive(true);
                AudioSource.PlayClipAtPoint(particlesFX, new Vector3(25, 0, -10),1f);
                new WaitForSeconds(particlesFX.length);
                gameStatus = GameStatus.Next; //set the game status
                currentQuestionIndex++; //increase currentQuestionIndex
                if (score == 4 && getMap.getMapID()!=5)
                {
                    gameComplete.SetActive(true);
                }

                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 2.5f); //go to next question

                }
                else
                {
                  gameComplete.SetActive(true); 
                }
            }
        }
    }

    public void ResetLastWord()
    {
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);
            currentAnswerIndex--;
            answerWordList[currentAnswerIndex].SetWord('_');
        }
    }

    public string getCurrentAnswer()
    {
        return answerWord;
    }
}



[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;
    public string answer;
}

public enum GameStatus
{
   Next,
   Playing
}
