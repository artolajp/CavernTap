using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class GameCenterController : GameCloudServices
{
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

	void Start(){

#if UNITY_IOS

		isSingIn = Social.localUser.authenticated;
#endif
	}

    public override void ReportHighScore(int score)
    {
#if UNITY_IOS
		if(IsSingIn){
			Social.ReportScore (score,GPGSlds.leaderboard_high_score,succes=>{
			
			});
		}
#endif
    }

    public override void ShowAchievements()
    {
#if UNITY_IOS
        Debug.Log("Achivements");
#endif

    }

    public override void ShowLeaderBoard()
    {
#if UNITY_IOS
		if (!IsSingIn) SingIn(ShowLeaderBoard);
		else{
			ReportHighScore(ScoreController.MaxScore);
			Social.ShowLeaderboardUI ();
		}
#endif
    }

    public override void SingIn(UnityAction callback)
    {
#if UNITY_IOS
		if(IsSingIn){
			Social.localUser.Authenticate ((bool succes)=>{
				IsSingIn=succes;
				if(succes) {
					
                    //PlayerPrefs.SetInt ("GameCloudSingIn",0);
                    //PlayerPrefs.Save();

					callback();
				}
			});
		}
#endif
    }

    public override void SingIn(UnityAction<string> callback, string idCallback)
    {
#if UNITY_IOS
		Social.localUser.Authenticate ((bool succes)=>{
			IsSingIn=succes;
			if(succes) {
				//PlayerPrefs.SetInt ("GameCloudSingIn",0);
                //PlayerPrefs.Save();

				callback(idCallback);
			
			}
		});
#endif
    }

    public override void UnlockAchievement(string id, bool unlock)
    {
    }

    public override void UnlockIncrementalAchievement(string id, int value)
    {
    }

    public override void ShowLeaderBoard(string id)
    {
#if UNITY_IOS
		if (!IsSingIn) SingIn(ShowLeaderBoard,id);
		else{
			ReportHighScore(ScoreController.MaxScore);
			Social.ShowLeaderboardUI ();
		}
#endif
    }
}
