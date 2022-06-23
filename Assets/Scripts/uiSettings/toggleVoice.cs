using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggleVoice : MonoBehaviour
{
    public static bool voiceStatus=true;
    [SerializeField] Toggle voiceButton;
    private void Start()
    {
        if(voiceStatus==false)
        {
            voiceButton.isOn=false;
        }
        else
        {
            voiceButton.isOn=true;
        }
    }
    public void OnMouseDown()
    {
        if(voiceButton.isOn)
        {
            voiceButton.isOn = true;
            voiceStatus = true;
        }
        else
        {
            voiceButton.isOn = false;
            voiceStatus = false;
        }
    }
}
