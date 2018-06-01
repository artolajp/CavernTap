using UnityEngine;
using System.Collections;
#if (UNITY_ANDROID || (UNITY_IPHONE && !NO_GPGS))
//using UnityEngine.Advertisements;
#endif
using UnityEngine.Events;

public class UnityAdsController : MonoBehaviour {

    private static UnityAdsController instance;
    private UnityAction<bool> lastCallback;
    public static UnityAdsController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<UnityAdsController>();
            return instance;
        }
    }
    /*
    public void ShowRewardedAd(UnityAction<bool> callback)
    {
        lastCallback = callback;
#if (UNITY_ANDROID || (UNITY_IPHONE && !NO_GPGS))
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
#endif
    }
#if (UNITY_ANDROID || (UNITY_IPHONE && !NO_GPGS))
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                lastCallback(true);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                lastCallback(false);
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
#endif
*/
    }
