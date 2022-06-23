using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quizAnswers : MonoBehaviour
{
    public bool correct=false; //whether asnwer is correct or not on initalizing all answer marked as incorrect.
    public quizGame quizGame; //quizGame.cs holder as we use functions from them
    public Sprite wrongSprite; //loads sprite with "X" mark on it when answer is incorrect.
    [SerializeField] AudioClip good, bad;
  public void Answer()
    {
        if (correct)
        {
            Debug.Log("Correct");
            AudioSource.PlayClipAtPoint(good, transform.position);
            quizGame.answeredCorrectly(); //if correct calls this function from quizGame.cs.
        }
        else
        {
            GetComponent<Button>().image.sprite = wrongSprite; //changes sprite to "X" to allow player to see that he is incorrect.
            AudioSource.PlayClipAtPoint(bad, transform.position);
            Debug.Log("Incorrect");
        }
    }
}
