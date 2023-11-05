using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Login()
    {
      var request = new LoginWithCustomIDRequest
      {
         CustomId = SystemInfo.deviceUniqueIdentifier,
         CreateAccount = true
      };
      PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess (LoginResult result) 
    {
        Debug.Log("Login Successful");
    }
    void OnError (PlayFabError error) 
    {
        Debug.Log("Login Failed");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> 
            {
                new StatisticUpdate
                {
                    StatisticName = "PlatformScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate (UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Scores sucessfully sent");
    }
}
