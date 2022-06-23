using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charachterController : MonoBehaviour
{
    public float MovementSpeed = 1; //movement speed of player.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //updates with character movement
    {
        var movement = Input.GetAxis("Horizontal"); //moves horizontal.
        transform.position = new Vector3(movement,-7.4f,-2.8f) * Time.deltaTime * MovementSpeed; //Positions of player.
    }
}
