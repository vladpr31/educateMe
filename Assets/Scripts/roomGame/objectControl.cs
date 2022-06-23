using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class objectControl : MonoBehaviour
{
    Animator anim;
    [SerializeField] AudioClip clickSound;
    [SerializeField] string buttonName;
    public static int count = 0;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
   
    public void buttonClick() //clicking on objects in "Room Game" to play animation.
    {
        if (buttonName == "backpack")
        {
            anim.SetTrigger("clicked");
            AudioSource.PlayClipAtPoint(clickSound, new Vector3(0,0,10), 1f);
            count += 1;
        }
        if (buttonName == "lunchbox")
        {
            anim.SetTrigger("boxClicked");
            AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 10), 1f);

            count += 1;
            
        }
        if (buttonName == "books")
        {
            anim.SetTrigger("booksClicked");
            AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 10), 1f);
            count += 1;

        }
        if (buttonName == "kalmar")
        {
            anim.SetTrigger("kalmarClicked");
            AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 10), 1f);
            count += 1;

        }
    }

}


//in short this script is just for "Animation" of the objects in the room game and play sound, the "Count" initially was for score
// but then changed to just count if the player clicked on all the objects in order to proceed.