using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for when game is done, for now it is not used.
/// </summary>
public class puzzleController : MonoBehaviour
{
    [SerializeField] GameObject sceneTransition;
    public AudioClip winSound; //wining sound.
    private bool isPlayingSound,isReload=false;

    void Start()
    {
        isReload=true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animalsPuzzle.lockedPieces == 7 && isReload)
        {

            isPlayingSound = true;
            isReload=false;
            
            
        }
        if(isPlayingSound && !isReload)
        {
            
            isPlayingSound = false;
            animalsPuzzle.lockedPieces = 0;
            StartCoroutine(playWinSound());
        }
    }
    IEnumerator playWinSound()
    {
        AudioSource.PlayClipAtPoint(winSound, new Vector3(0, 0, 0));
        yield return new WaitForSeconds(winSound.length-3.5f);
        sceneTransition.SetActive(true);
    }
}


//Not much to explain in this code mostly to activate/deactivate some gameobjects and to play sound on demand.