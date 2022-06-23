using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextMapScene : MonoBehaviour
{
    public GameObject scenery; //Scene transition animation holder.
    public Collider2D triggerWall; // Invisible wall Holder.
    public static bool triggered = false; //Static false, player cannot proceed until he collected 3 items. scoreControl changes that.

    private void Update()
    {
        if (triggered) //Only when player collects all of the 3 items he can proceed to next stage.
        {
            triggerWall.isTrigger = true; //sets invislbe wall trigger to true.
        }
    }
    private void OnTriggerEnter2D(Collider2D other) //function for player to walk left and collide with invisible wall to load next stage.
    {

        if (other.tag == "Player" && triggered)
        {
            scenery.SetActive(true); //sets active the SceneLoad() for transition animation.
        }
    }
}
