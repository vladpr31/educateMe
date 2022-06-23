using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class button_anima : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void button_touch()
    {
        GetComponent<Animation>().Play("buttonT");

    }
}
