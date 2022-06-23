using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public Animator transitionAnime;
    public string sceneName;
    public GameObject[] objects; //Mainly for the dialogues.
    [SerializeField] private static string gender;
    void Start()
    {
        if (sceneName == "noScene") //if no scene used then just activate dialogues.
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].active)
                {
                    objects[i].SetActive(false);
                }
                else
                {
                     new WaitForSeconds(3f);
                    objects[i].SetActive(true);
                }
            }
        }
        else
        { 
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        transitionAnime.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public static string getGender() //returns the gender we chose in the first menu.
    {
        return gender;
    }
    public void setGender(string gndr) //set the gender when we chose it.
    {
        gender = gndr;
    }
}
//Basically this code just manages the scenes loadings, it is using an Fade in\out animation just to look a bit more smoothly.