using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class RewardedVideoController : MonoBehaviour {

    RewardBasedVideoAd rewarded;
    bool isLoaded = false;
    bool isRewarded = false;
    private static RewardedVideoController instance;

    private static bool handleLoad = false;

    private UnityAction<bool> callbackAction;
    string adUnitId;


    public static RewardedVideoController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<RewardedVideoController>();
            return instance;
        }
    }
    
    void Start()
    {


#if UNITY_ANDROID
        //string adUnitId = "ca-app-pub-9784670807916691/1510152760";
        adUnitId = AdMobIds.Rewarded;
#elif UNITY_IOS
         adUnitId =AdMobIds.Rewarded;
#else
         adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        rewarded = RewardBasedVideoAd.Instance;
        if (!handleLoad)
        {
            rewarded.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewarded.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

            rewarded.OnAdClosed += Ad_OnAdClosed;
            rewarded.OnAdLoaded += Ad_OnAdLoaded;
            handleLoad = true;
        }
        //RequestReward();
    }

    public void RequestReward () {
        
            // Create an empty ad request.
            //AdRequest request = new AdRequest.Builder().AddTestDevice("0F33319A15EAEF2F9EFDC37652C036E0").Build();
        if (!isLoaded)
        {
			if (Application.internetReachability != NetworkReachability.NotReachable){
				AdRequest request = new AdRequest.Builder().Build();

				rewarded.LoadAd(request, adUnitId);
				isLoaded = true;
				isRewarded = false;
			}
                
        }
    }
	public void Show(UnityAction<bool> Callback , int loop =0) {
        callbackAction = Callback;
        
        if (rewarded.IsLoaded()&&isLoaded) {
            isLoaded = false;
            rewarded.Show();
            //callbackAction(true);

        }
		else if(loop<1){
                RequestReward();
                Debug.Log("cargando");
                Show(callbackAction,1);
            }
        }
    


    public void HandleRewardBasedVideoRewarded(object sender,Reward args)
    {
        Debug.Log(args.Amount+ "  "+args.Type);
        if (!isRewarded)
        {
            callbackAction(true);
            isRewarded = true;

        }else
        {
            callbackAction(false);

        }
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs arg)
    {
        Debug.Log(arg.Message);
        callbackAction(false);
    }

    void Ad_OnAdLoaded(object sender, System.EventArgs e)
    {
        Debug.Log("Ad loaded.");
    }

    void Ad_OnAdClosed(object sender, System.EventArgs e)
    {
        Debug.Log("Ad was closed, proceeding to game without rewards...");
    }

}
