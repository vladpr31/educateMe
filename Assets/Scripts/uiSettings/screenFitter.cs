using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class screenFitter : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        if(SceneManager.GetActiveScene().name=="MainMenu")
        {
            getMap.setMapID(5);
        }
    }


}

