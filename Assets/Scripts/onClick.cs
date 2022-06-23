using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class onClick : MonoBehaviour
{
    [SerializeField] GameObject giveUpPop = null;
    [SerializeField] string gameName;
    [SerializeField] AudioClip soundFX;
    public void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(soundFX, new Vector3(0, 0, 0));
        if (giveUpPop.active)
        {
            giveUpPop.SetActive(false);
        }
        else
        {
            giveUpPop.SetActive(true);
        }
    }
    public void loadGame()
    {
        leaderBoard.gameDone = true;
        SceneManager.LoadScene(gameName);
    }
    public void exitGame()
    {
        Debug.Log("Terminating Game..");
        Application.Quit();
    }

}
