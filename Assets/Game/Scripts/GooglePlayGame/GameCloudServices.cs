using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameCloudServices : MonoBehaviour {


    public abstract bool IsSingIn { get; set; }
    public abstract void SingIn(UnityAction callback);
    public abstract void SingIn(UnityAction<string> callback,string idCallback);
    public abstract void ReportHighScore(int score);
    public abstract void ShowLeaderBoard();
    public abstract void ShowLeaderBoard(string id);
    public abstract void ShowAchievements();
    public abstract void UnlockAchievement(string id, bool unlock);
    public abstract void UnlockIncrementalAchievement(string id, int value);
}
