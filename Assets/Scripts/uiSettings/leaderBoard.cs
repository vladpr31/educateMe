using UnityEngine.UI;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using System;

public class leaderBoard : MonoBehaviour
{
    [SerializeField] int gameID;
    [SerializeField] TextMeshProUGUI[] entries;
    [SerializeField] Text[] rank;
    [SerializeField] Text[] score;
    int maxScores = 6;
    public static int playerScore;
    public static bool gameDone = false;
    playerName pName;
    private string plyerName;
    private void Start()
    {
        pName = new playerName();
        plyerName = pName.getPlayerName();
        LootLockerSDKManager.StartSession(plyerName, (response) =>
         {
             
             if(response.success)
             {
                 Debug.Log("Success");
             }
             else
             {
                 Debug.Log("Connection Failed.");
             }
         });
    }

    public void getScore()
    {
        if (pName != null) { plyerName = pName.getPlayerName(); }
        LootLockerSDKManager.GetScoreList(gameID, maxScores, (response) =>
          {
              if (response.success)
              {
                  LootLockerLeaderboardMember[] scores = response.items;
                  for(int i=0;i<scores.Length;i++)
                  {
                      rank[i].text = scores[i].rank.ToString() + "-";
                      entries[i].text = "-"+scores[i].member_id;
                      score[i].text = scores[i].score.ToString();
                      
                  }
                  if (scores.Length < maxScores)
                  {
                      for (int i = scores.Length; i < maxScores; i++)
                      {
                          entries[i].text = ((i + 1).ToString() + ".");
                      }
                  }
              }
              else
              {
                  Debug.Log("Connection Failed.");
              }
          });
    }
    void Update()
    {
        if(gameDone)
        {
            submitScore();
        }
    }

    public void submitScore()
    {
        if (pName != null) { plyerName = pName.getPlayerName(); }
        LootLockerSDKManager.SubmitScore(plyerName, playerScore, gameID, (response) =>
          {
              if (response.success)
              {
                  Debug.Log("Success");
                  gameDone = false;
              }
              else
              {
                  Debug.Log("Connection Failed.");
              }
          });
    }
    private string generateUniqueID()
    {
        return Guid.NewGuid().ToString("N");
    }
}
