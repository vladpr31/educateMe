using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//This script's purpose is to "Rearrange" the puzzle pieces, it's randomly rearranges the array of gameobjects.
// it is then 'spawns' them in fixed coordinates to fit the background, the animals and the piecies are always in different order.
public class randomSpawns : MonoBehaviour
{
    //Array of objects to spawn (note I've removed the private goods variable)
    [SerializeField] GameObject[] theGoodiesPieces;
    [SerializeField] GameObject[] theGoodies;
    [SerializeField] AudioClip popSound;
    [Space(3)]

    //Coordinates for spawning.
    private float maxXPosSides = -8.5f;
    private float minYPos = 0;
    private float maxYPosSides = 5f;
    private int totalSpawns = 7;
    private int spawnCount = 0;
    private List<int> listIDs = new List<int>();

    //Start Initialization -> calls the spawning function.
    void Start()
    { 
        SpawnGoodies();
    }

    //Spawning function, positions the gameobjects at fixed locations to fit the screen.
    void SpawnGoodies()
    {
        int rand = Random.Range(0, theGoodies.Length);
        for(int i=0;i< totalSpawns; i++)
        {
            if (theGoodies[rand].activeSelf) { rand = Random.Range(0, theGoodies.Length); i--; } //if piece already active generate other piece.
            else
            {
                listIDs.Add(rand); //save the ID's of spaneed objects.
                theGoodies[rand].transform.position = new Vector3(maxXPosSides, minYPos); //position them.
                theGoodies[rand].SetActive(true);
                maxXPosSides += 3f; //the spacing between the gameobjects.
                AudioSource.PlayClipAtPoint(popSound, transform.position); //play pop sound when puzzle pops.
            }
        }
        maxXPosSides = -8.5f;
        rand = Random.Range(0, listIDs.Count);
        for (int i = 0; i < totalSpawns; i++)
        { 
            if (theGoodiesPieces[listIDs[rand]].activeSelf) { rand = Random.Range(0, listIDs.Count); i--; } //checks if pieces already active.
            else
            {
                theGoodiesPieces[listIDs[rand]].transform.position = new Vector3(maxXPosSides, maxYPosSides);
                theGoodiesPieces[listIDs[rand]].SetActive(true);
                maxXPosSides += 3f;
            }
        }
    }
    
}
/*
 * Randomly Spawn Puzzle Pieces on the screen.
 * We have 2 Arrays 1 for the puzzle piece and 1 for the blank piece.
 * To make the pieces fit, 2 loops needed 1 which spawns the Puzzle Piece and save its "ID" and the 2nd array runs on the ID list and spawns
 * them randomly so it will be different each time we play.
 */


