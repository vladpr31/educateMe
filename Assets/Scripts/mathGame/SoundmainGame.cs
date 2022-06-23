using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundmainGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started.");
    }
    private static SoundmainGame instance = null;
    public static SoundmainGame Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
