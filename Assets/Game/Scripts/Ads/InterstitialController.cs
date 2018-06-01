using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using GoogleMobileAds.Api;

public class InterstitialController : MonoBehaviour {

    InterstitialAd interstitial;
    bool isInterstitialLoad = false,isShowed = false;
    private static InterstitialController instance;


    public static InterstitialController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<InterstitialController>();
            return instance;
        }
    }
    

    public void RequestInterstitial()
    {
        if(!isInterstitialLoad){
#if UNITY_ANDROID
            //string adUnitId = "ca-app-pub-9784670807916691/4463619164";
            string adUnitId = AdMobIds.Insteresticial;
#elif UNITY_IOS
        string adUnitId = AdMobIds.Insteresticial;
#else
        string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            //AdRequest request = new AdRequest.Builder().AddTestDevice("0F33319A15EAEF2F9EFDC37652C036E0").Build();
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
            isInterstitialLoad = true;
        }
    }

    public void Show() {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            isShowed = true;
        }
        else {
        }
    }

    public void DestroyInterstitial() {
        if (isShowed) { 
            interstitial.Destroy();
            isInterstitialLoad = false;
            isShowed = false;
        }
        
    }


}
