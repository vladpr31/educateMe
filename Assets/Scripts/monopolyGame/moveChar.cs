using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveChar : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float moveSpeed = 1f;
    [HideInInspector] public static int waypointIndex = 0;
    [SerializeField] GameObject p;
    [SerializeField] GameObject[] disableObjects;
    [SerializeField] GameObject[] enabledObjects;
    //[SerializeField] AudioClip walking;
    private bool moveAllowed = false;

    private void Start()
    {
        if (sceneHopping.continued == true) //just a "Flag" for scene Diallogue to play once on the beggining of the game.
                                            // if game already runs dont play the scene anymore.
        {
            for(int i=0; i < disableObjects.Length; i++) //deactivate objects as explained above.
            {
                disableObjects[i].SetActive(false);
            }
        }
        if (sceneHopping.getLastIndex() == 0 || sceneHopping.getLastIndex() == -1) 
        {
            transform.position = wayPoints[waypointIndex].transform.position; //initial index of player at the start of the game.
        }
        else
        {
            //Gets the last index the player was one before jumping
            //to a different game.
            waypointIndex = sceneHopping.getLastIndex(); 
            transform.position = wayPoints[waypointIndex].transform.position; //puts player at the last index he was before changing scene.
            gControl.setDiceValue(waypointIndex);
        }
    }
    private void Update()
    {
        if(moveAllowed) //checks if player is allowed to move or not. Update functions works every frame.
        {
            moveCharacter(); //calls moveCharacter function which moves the player on the board.
        }
    }
  
    public bool getAllowance()
    {
        return moveAllowed;
    }
    public void setAllowance(bool allow)
    {
        moveAllowed = allow;
    }
    public int getWayPointsLength() //return the length of our waypoints.
    {
        return wayPoints.Length;
    }
    private void moveCharacter() //this function move the charater.
    {
        if (waypointIndex != wayPoints.Length) //if not reached "School"
        {
            if (waypointIndex <= wayPoints.Length - 1)
            {
                if (waypointIndex <= gControl.getDiceValue()) //if current player Index is not as current + dice value then move.
                {
                    {
                        p.GetComponent<Animator>().SetBool("isWalking", true); //Walking Animation.
                        transform.position = Vector2.MoveTowards(transform.position, wayPoints[waypointIndex].transform.position,
                               moveSpeed * Time.deltaTime); //Move Player as the number of the dice.
                    }
                }
            }
            if (transform.position == wayPoints[waypointIndex].transform.position) //if player reached the current+dice.
            {
                waypointIndex++; //update our current position.
                p.GetComponent<Animator>().SetBool("isWalking", false); //set animation to idle animation.
            }
            if (getMap.getMapID() == 0) //change character direction for gameMap 0
            {
                if (waypointIndex >= 9 && waypointIndex < 17) //flip Player Sprite facing left or right.
                {
                    p.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    p.GetComponent<SpriteRenderer>().flipX = false;

                }
            }
            if (getMap.getMapID() == 1) //change character direction for gameMap 1
            {
                if (waypointIndex >= 7 && waypointIndex < 13) //flip Player Sprite facing left or right.
                {
                    p.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    p.GetComponent<SpriteRenderer>().flipX = false;

                }
            }
        }
        else //if player reached "school" then play dialogue.
        {
            for(int i=0;i<enabledObjects.Length;i++) //loop to activate dialogue objects.
            {
                enabledObjects[i].SetActive(true);
            }
        }
    }
    public static int getWaypointIndex() //return the waypoint index of the player.
    {
        return waypointIndex;
    }
}
