using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine.Events;

public class GooglePlayGameController : GameCloudServices {

    private bool isSingIn;

    public override bool IsSingIn
    {
        get
        {
            return isSingIn;
        }

        set
        {
            isSingIn = value;
        }
    }

    void  Start () {
#if UNITY_ANDROID
        PlayGamesPlatform.Activate();
        //IsSingIn = Social.localUser.authenticated;
        
#endif

    }
    public override void SingIn(UnityAction callback ) {
#if UNITY_ANDROID
        Social.localUser.Authenticate((bool success) => {
            IsSingIn = success;
            if (success) {

                //PlayerPrefs.SetInt("GameCloudSingIn", 0);
                //PlayerPrefs.Save();

                callback();
            }
        });
#endif
    }
    public override void SingIn(UnityAction<string> callback,string id)
    {
#if UNITY_ANDROID
        Social.localUser.Authenticate((bool success) => {
            IsSingIn = success;
            if (success)
            {
                //PlayerPrefs.SetInt("GameCloudSingIn", 0);
                //PlayerPrefs.Save();
                callback(id);
            }
        });
#endif
    }

    public override void ReportHighScore(int score) {

#if UNITY_ANDROID
        if (IsSingIn) Social.ReportScore(score, GPGSlds.leaderboard_high_score, (bool success) => {
            // handle success or failure
        });
#endif
    }

    public override void ShowLeaderBoard (){
#if UNITY_ANDROID
        if (!IsSingIn) SingIn(ShowLeaderBoard);
        else  {
            ReportHighScore(ScoreController.MaxScore);
            Social.ShowLeaderboardUI();

        }
#endif
    }
    public override void ShowLeaderBoard(string id) {
#if UNITY_ANDROID
        if (!IsSingIn) SingIn(ShowLeaderBoard,id);
        else {
            ReportHighScore(ScoreController.MaxScore);
            ((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(id);
            Social.ShowLeaderboardUI();
        }
#endif
    }

    public override void ShowAchievements() {
#if UNITY_ANDROID
        if (!IsSingIn) SingIn(ShowAchievements);
        else Social.ShowAchievementsUI();
#endif
    }

    public override void UnlockAchievement(string id,bool unlock ) {
#if UNITY_ANDROID
        if (IsSingIn)
        {
            Social.ReportProgress(id, unlock ? 100.0f : 0.0f, (bool success) =>
            {
                if (success) Debug.Log("ok");
                else Debug.Log("No se pudo");
            });
        }
#endif
    }
    public override void UnlockIncrementalAchievement(string id,int value)
    {
#if UNITY_ANDROID
        if (IsSingIn)
        {
            Debug.Log(value);
            PlayGamesPlatform.Instance.IncrementAchievement(       id, value, (bool success) => 
            {
                if (success) Debug.Log("ok");
                else Debug.Log("No se pudo");
            });
        }
#endif
    }
}
