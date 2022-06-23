using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the "Game Controller" in this script we have 3 main function. 
/// onMouseDown() -> when mouse button is clicked.
/// OnMouseDrag() -> when we hold the click and drag the gameobject(puzzle piece).
/// OnMouseUp() -> when we realease the mouseclick.
/// each function works as the function name sounds.
/// </summary>


public class animalsPuzzle : MonoBehaviour
{
    [SerializeField]
    private Transform piecePlace;
    private Vector2 initialPosition; //initial placement of all sprites.
    private Vector2 mousePos; //mouse position.
    private float posX,posY;
    public static int lockedPieces = 0; //counts how many animals got locked.
    [SerializeField] AudioClip pieceSound;
    private bool locked = false;
    void Start()
    {
        lockedPieces = 0;
    }

    void OnMouseDown() //when mouse clicked follows the mouse position.
    {
        if (this.locked != true)
        {
            posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            initialPosition = transform.position; //saves the piece start position.
        }

    }
    private void OnMouseDrag() //when mouse draggs the sprite its follows mouse and sprite position.
    {
        if (this.locked != true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePos.x - posX, mousePos.y - posY);
        }
    }
    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - piecePlace.position.x) <= 0.5f &&
            Mathf.Abs(transform.position.y - piecePlace.position.y) <= 0.5f && this.locked!=true) //if animal fits, then fit into piece. fits when close by 0.5f.
        {
            this.locked = true;
            transform.position = new Vector2(piecePlace.position.x, piecePlace.position.y);
            lockedPieces += 1; //increases when animal is locked on it's piece.
            if (pieceSound != null)
            {
                AudioSource.PlayClipAtPoint(pieceSound, new Vector3(0,0,0), 1f);
            }

        }
        else
        {
            // if the animal doesnt fit the piece return the animal to initial position.
            transform.position = new Vector2(initialPosition.x, initialPosition.y); 
        }
    }
    public int getLocked()
    {
        return lockedPieces;
    }
    public void resetLock()
    {
        lockedPieces = 0;
    }
}
