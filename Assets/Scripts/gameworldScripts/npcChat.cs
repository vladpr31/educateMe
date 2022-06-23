using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class npcChat : MonoBehaviour
{
    public GameObject[] chatBubbles;
    public float timer = 10.0f;
    public string[] randomSentences;
    public TextMeshPro[] randWords;
    private void Start()
    {

        InvokeRepeating("createBubble", 2.0f, 10.0f);
    }
    private void Update()
    {
        
    }
    void createBubble()
    {
        if(chatBubbles[0].active)
        {
            chatBubbles[0].SetActive(false);
        }
        else
        {
            chatBubbles[1].SetActive(false);
        }
        int index = Random.Range(0, randomSentences.Length);
        int indexBubble = Random.Range(0, chatBubbles.Length);
        randWords[indexBubble].text = randomSentences[index];
        chatBubbles[indexBubble].SetActive(true);
    }
}

//simply make "talking" animations between npcs.
