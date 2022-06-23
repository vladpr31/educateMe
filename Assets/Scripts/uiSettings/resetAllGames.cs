using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetAllGames : MonoBehaviour
{
    void Start()
    {
        moveChar.waypointIndex = 0; //Restarts the index of the character position on monopoly games.
        getMap.setMapID(5); //sets map ID to 5, 5 means "Free Game".
        objectControl.count = 0; //sets the object count in room game back to 0.
    }
}

/*
 * The Purpose of this script is to Reset the static variables each time a player goes back to the Main Menu Screen.
 */
