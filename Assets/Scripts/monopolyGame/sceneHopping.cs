using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneHopping : MonoBehaviour
{
    [SerializeField] Animator transitionAnime;
    [SerializeField] string[] sceneNames;
    [SerializeField] GameObject[] objects; //Mainly for the dialogues.
    private static int lasIndexSave = 0;
    private static int sceneIndex;
    private string sceneName;
    private static List<int> removedScenes;
    public static bool continued = false;
    private static bool initiated = false;
    private string currScene;
    [System.Obsolete]
    void Start()
    {
        
        if (getMap.getMapID() != 5) //5 is just a random number the indicates the "Map" if its 5 it means it's a free play.
        {
            if (!initiated) { removedScenes = new List<int>(); initiated = true; }
            if(sceneNames[0] == "MainMenu") { SceneManager.LoadScene("MainMenu"); }
            sceneIndex = Random.Range(0, sceneNames.Length);
            sceneName = sceneNames[sceneIndex];
            if (sceneName == "noScene") //if no scene used then just activate dialogues or deactivate objects.
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
            else //if there is a scene then start courtine.
            {
                StartCoroutine(LoadScene());
            }
        }
        else //this else statement is for "free play" mode so the player could play the game as much as he wants.
        {
            currScene = SceneManager.GetActiveScene().name;
            if (currScene != "monopolyGame" && currScene != "monopolyGame2")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloads same Scene of the game.
            }
        }
    }
    
    IEnumerator LoadScene()
    {
        lasIndexSave = moveChar.getWaypointIndex() - 1; //the last index the player was on.
        transitionAnime.SetTrigger("End"); //ends "Transition" animation.
        //sceneIndex += 1;
        yield return new WaitForSeconds(1.5f);
        if (sceneNames.Length == 2) //Monopoly maps are chosen randomly between them, this gets the "Map ID" and loads the map we played.
        {
            continued = true; //if game already started.
            SceneManager.LoadScene(sceneNames[getMap.getMapID()]); //loadscene we've loaded.
        }
        else
        {
            //removedScenes is a list of games we already player, they are added into the list and we randomly generated a new game
            //which is not contained inside the list, once we played all the games and still didnt reach "School" then we clear the list
            // and start over again.
            if (removedScenes.Contains(sceneIndex))
            {
                if (removedScenes.Count == sceneNames.Length) { removedScenes.Clear(); sceneIndex = Random.Range(0, sceneNames.Length);}
                while (removedScenes.Contains(sceneIndex))
                {
                    sceneIndex = Random.Range(0, sceneNames.Length);
                }
                removedScenes.Add(sceneIndex);
            }
            else { removedScenes.Add(sceneIndex); }
           
            //here it loads the games in random.
            SceneManager.LoadScene(sceneNames[sceneIndex]);
        }
    }

    public static int getLastIndex() //saves the last index the player was on before entering a game.
    {
        return lasIndexSave;
    }
}
