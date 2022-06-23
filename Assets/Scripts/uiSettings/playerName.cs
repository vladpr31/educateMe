using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pName;
    private static string plyName;

    private void Start()
    {
        plyName = pName.text;
    }

    public string getPlayerName()
    {
        return plyName;
    }
}
