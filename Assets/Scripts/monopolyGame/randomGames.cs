using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGames : MonoBehaviour
{
    [SerializeField] GameObject games;
    [SerializeField] GameObject[] waypoints;
    private void Start()
    {
        if (sceneHopping.getLastIndex() == 0)
        {
            for (int i = 1; i < waypoints.Length; i++)
            {
                Instantiate(games, waypoints[i].transform.position, Quaternion.identity);
            }
        }
        else
        {
            for (int i = sceneHopping.getLastIndex()+1; i < waypoints.Length; i++)
            {
                Instantiate(games, waypoints[i].transform.position, Quaternion.identity);
            }
        }
    }

}
