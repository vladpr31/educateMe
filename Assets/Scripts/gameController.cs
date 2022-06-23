using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    [SerializeField]
    private Transform numberPlace;
    private Vector2 initialPosition; //initial placement of all sprites.
    private Vector2 mousePos; //mouse position.
    private float posX, posY; //for mouse movments
    private static float newPosX = -8f; //for the sprites after answered right.
    private static float newPosY =-3f; //for the sprites after answered right.
    [SerializeField] private AudioClip succsess,winner; //audioclip for success and win.
    private static int lockedNumbers = 0; //counts how many numbers got correct.
    private static bool isPlayingSound = false; //sync the winning sound.
    [SerializeField] GameObject numbers,blanks,panel,button1,button2,text;
    [SerializeField]
    private int id;
    private void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        winGame();
    }

    void OnMouseDown() //when mouse clicked follows the mouse position.
    {

        posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

    }
    private void OnMouseDrag() //when mouse draggs the sprite its follows mouse and sprite position.
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x - posX, mousePos.y - posY);

    }
    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - numberPlace.position.x) <= 0.5f &&
        Mathf.Abs(transform.position.y - numberPlace.position.y) <= 0.5f) //if animal fits, then fit into piece. fits when close by 0.5f.
        {
            transform.position = new Vector2(numberPlace.position.x, numberPlace.position.y);
            transform.position = new Vector2(newPosX + (1.5f * id), newPosY);
            numberPlace.position = new Vector2(newPosX + (1.5f * id), newPosY);
            AudioSource.PlayClipAtPoint(succsess, transform.position);
            lockedNumbers += 1;
            if(lockedNumbers + 1 == 11)
            {
                isPlayingSound = true;
            }
        }
        else
        {
            // if the animal doesnt fit the piece return the animal to initial position.
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }

    //when player wins game, plays sound and asks if player wants to play again\continue\exit to main menu.
    void winGame()
    {
        if(lockedNumbers == 11 && isPlayingSound)
        {
            isPlayingSound = false;
            if (!isPlayingSound)
            {
                numbers.SetActive(false);
                blanks.SetActive(false);
                AudioSource.PlayClipAtPoint(winner, transform.position);
                panel.SetActive(true);
                new WaitForSeconds(2.0f);
                button1.SetActive(true);
                button2.SetActive(true);
                text.SetActive(true);
                lockedNumbers = 0;
                isPlayingSound = true;
            }
        }
    }
}
