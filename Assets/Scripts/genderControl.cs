using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genderControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer currentGender;
    [SerializeField] Sprite Gender;
    private bool girl = false;
    void Start()
    {
        currentGender = GetComponent<SpriteRenderer>();
        if (sceneTransition.getGender() == "Girl")
        {
            girl = true;
            currentGender.sprite = Gender;
        }
    }
    public bool isGirl()
    {
        return girl;
    }

}
