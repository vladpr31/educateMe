using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mapSelector : MonoBehaviour
{
    private int randMap;
    [SerializeField] string[] mapName;
    private void Start()
    {
        randMap = Random.Range(0, mapName.Length);
        getMap.setMapID(randMap);
        SceneManager.LoadScene(mapName[getMap.getMapID()]);
    }
}
