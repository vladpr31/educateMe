using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiControl : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    private void OnMouseDown()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

}
