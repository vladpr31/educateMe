using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatControl : MonoBehaviour
{
    [SerializeField] GameObject dialogueControl, dialogueIMG, dialogueCanv;
    void Update()
    {
        dialgogueDone();
    }

    void dialgogueDone()
    {
        if (objectControl.count==4)
        {
            dialogueControl.SetActive(true);
            dialogueIMG.SetActive(true);
            dialogueCanv.SetActive(true);
        }
    }
}
