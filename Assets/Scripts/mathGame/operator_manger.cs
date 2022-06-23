using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class operator_manger : MonoBehaviour
{
    int firstValue, secondValue, tempValue, sum, tempalt1, tempalt2, score;
    public TextMeshProUGUI PrimeDigit, SecondDigit, oper_main, alt1, alt2, alt3, answer;
    public Sprite yes_sprite, no_sprite, trans_sprite;
    public GameObject RawImage_1, RawImage_2, RawImage_3;
    public Transform score_img;
    public Animator answer_anim;
    public AudioSource Source_music;
    public AudioClip sound_yes, sound_no,winner;
    private hintControl hc;
    private List<string> operators;
    [SerializeField] private GameObject sceneObj;
    [SerializeField] Text[] scoresText;
    [SerializeField] GameObject[] genderSprites;
    private int randomGameIndex;
    private string Var_oper;
    public static int rightAnswer;
    private int yes, no = 0;
    private int combo = 1;
    private void Start()
    {
        if (sceneTransition.getGender() == "boy")
        {
            genderSprites[0].SetActive(true);
            genderSprites[1].SetActive(true);
            genderSprites[2].SetActive(false);
            genderSprites[3].SetActive(false);
        }
        else
        {
            genderSprites[0].SetActive(false);
            genderSprites[1].SetActive(false);
            genderSprites[2].SetActive(true);
            genderSprites[3].SetActive(true);
        }
        randomGameIndex = Random.Range(0, 4);
        if (getMap.getMapID() != 5)
        {
            operators = new List<string> { "+", "-", "/", "*" };
            calculatefunc(operators[randomGameIndex]);
        }
        else { calculatefunc(Calculate.move_op); }
        score = 0;
    }
    public void calculatefunc(string oper)
    {
        reset_sprite();
        firstValue = Random.Range(1, 10);
        secondValue = Random.Range(1, 10);

        if (firstValue - secondValue < 0)
        {
            tempValue = secondValue;
            secondValue = firstValue;
            firstValue = tempValue;
        }

        if (oper == "+")
        {
            sum = firstValue + secondValue;
            Var_oper = "+";
            rightAnswer = sum;
        }
        if (oper == "-")
        {
            sum = firstValue - secondValue;
            Var_oper = "-";
            rightAnswer = sum;
        }
        if (oper == "*")
        {
            sum = firstValue * secondValue;
            Var_oper = "*";
            rightAnswer = sum;
        }
        if (oper == "/")
        {
            firstValue = Random.Range(1, 20);
            secondValue = Random.Range(1, 20);
            if (firstValue % secondValue != 0)
            {
                calculatefunc(oper);
            }
            sum = firstValue / secondValue;
            rightAnswer = sum;
            Var_oper = "/";

        }



        PrimeDigit.text = firstValue.ToString();
        SecondDigit.text = secondValue.ToString();



        if (Var_oper == "+")
        {
            oper_main.text = "+";
        }
        if (Var_oper == "-")
        {
            oper_main.text = "-";
        }
        if (Var_oper == "*")
        {
            oper_main.text = "*";
        }
        if (Var_oper == "/")
        {
            oper_main.text = "/";
        }


        tempValue = Random.Range(2, 20);
        while (tempValue == sum)
        {
            tempValue = Random.Range(2, 20);
        }
        tempalt1 = tempValue;

        tempValue = Random.Range(2, 20);
        while ((tempValue == sum) || (tempValue == tempalt1))
        {
            tempValue = Random.Range(2, 20);
        }
        tempalt2 = tempValue;


        tempValue = Random.Range(1, 7);
        if (tempValue == 1)
        {
            alt1.text = sum.ToString(); alt2.text = tempalt1.ToString(); alt3.text = tempalt2.ToString();
        }
        if (tempValue == 2)
        {
            alt1.text = sum.ToString(); alt2.text = tempalt2.ToString(); alt3.text = tempalt1.ToString();
        }
        if (tempValue == 3)
        {
            alt1.text = tempalt1.ToString(); alt2.text = sum.ToString(); alt3.text = tempalt2.ToString();
        }
        if (tempValue == 4)
        {
            alt1.text = tempalt1.ToString(); alt2.text = tempalt2.ToString(); alt3.text = sum.ToString();
        }
        if (tempValue == 5)
        {
            alt1.text = tempalt2.ToString(); alt2.text = sum.ToString(); alt3.text = tempalt1.ToString();
        }
        if (tempValue == 6)
        {
            alt1.text = tempalt2.ToString(); alt2.text = tempalt1.ToString(); alt3.text = sum.ToString();
        }

    }


    public void reset_sprite()
    {
        RawImage_1.GetComponent<Image>().sprite = trans_sprite;
        RawImage_2.GetComponent<Image>().sprite = trans_sprite;
        RawImage_3.GetComponent<Image>().sprite = trans_sprite;
        answer.text = "?";
    }
    public void alt_1_action()
    {
        if (alt1.text == sum.ToString())
        {
            RawImage_1.GetComponent<Image>().sprite = yes_sprite;
            combo += 1;
            right();
        }
        else
        {
            RawImage_1.GetComponent<Image>().sprite = no_sprite;
            combo = 1;
            wrong();

        }
    }

    public void alt_2_action()
    {
        if (alt2.text == sum.ToString())
        {
            RawImage_2.GetComponent<Image>().sprite = yes_sprite;
            leaderBoard.playerScore += 5 * combo;
            combo += 1;
            right();

        }
        else
        {
            RawImage_2.GetComponent<Image>().sprite = no_sprite;
            combo = 1;
            leaderBoard.playerScore -= 5 * combo;
            wrong();
        }
    }

    public void alt_3_action()
    {
        if (alt3.text == sum.ToString())
        {
            RawImage_3.GetComponent<Image>().sprite = yes_sprite;
            combo += 1;
            right();
        }
        else
        {
            RawImage_3.GetComponent<Image>().sprite = no_sprite;
            combo = 1;
            wrong();
        }
    }

    public void right()
    {
        answer.text = sum.ToString();
        yes += 1;
        score += 1;
        answer_anim = GameObject.Find("main_alt").GetComponent<Animator>();
        answer_anim.Play("main_alt_move");
        scoresText[0].text = yes.ToString();
        Instantiate(score_img, new Vector2(-760f + (score - 1) * 80f, 280.8487f), Quaternion.identity);
        Source_music.clip = sound_yes;
        Source_music.Play();
        if (score == 5 && getMap.getMapID() != 5 || score == 2000)
        {
            AudioSource.PlayClipAtPoint(winner, new Vector3(0, 0, 0));
            new WaitForSeconds(winner.length);
            sceneObj.SetActive(true);
            hc = GameObject.FindObjectOfType(typeof(hintControl)) as hintControl;
            hc.resetHintsCount();
        }
        if (score < 5 || getMap.getMapID() == 5)
        {
            StartCoroutine(new_qustion());
        }
    }

    public void wrong()
    {
        no += 1;
        Source_music.clip = sound_no;
        scoresText[1].text = no.ToString();
        Source_music.Play();
    }
    IEnumerator new_qustion()
    {
        hc = GameObject.FindObjectOfType(typeof(hintControl)) as hintControl;
       
        hc.resetHints();
        yield return new WaitForSeconds(1);
        calculatefunc(Var_oper);

    }

    public int  getRightAnswer()
    {
        return rightAnswer;
    }
}
