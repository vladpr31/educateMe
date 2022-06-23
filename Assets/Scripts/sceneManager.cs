using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public string sceneName;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            loadScene();
        }
    }
    public void loadScene()
    {
        SceneManager.LoadScene(sceneName);

    }
}
