using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberController : MonoBehaviour
{
    [SerializeField] GameObject Scene;
    [SerializeField] int pieces;
    [SerializeField] AudioClip winSound;
    private bool playOnce = true;
    private void Start()
    {
        animalsPuzzle.lockedPieces = 0;
    }
    private void Update()
    {
        StartCoroutine(gameWon());
    }
    IEnumerator gameWon()
    {
        //Debug.Log("Yes");
        if (animalsPuzzle.lockedPieces == pieces)
        {
            if (playOnce)
            {
                playOnce = false;
                AudioSource.PlayClipAtPoint(winSound, new Vector3(0, 0, 0));
                yield return new WaitForSeconds(winSound.length-1.5f);
                Scene.SetActive(true);
            }
        }
    }
}
