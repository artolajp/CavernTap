using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class BannerController : MonoBehaviour {

    BannerView bannerView;

    private static BannerController instance;

    public static BannerController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<BannerController>();
            return instance;
        }
    }

    void Start() {
        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        //string adUnitId = "ca-app-pub-9784670807916691/6079953166";
        string adUnitId = AdMobIds.Banner;
#elif UNITY_IOS
        string adUnitId = AdMobIds.Banner;
#else
        string adUnitId = "unexpected_platform";
#endif

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        //AdRequest request = new AdRequest.Builder().AddTestDevice("0F33319A15EAEF2F9EFDC37652C036E0").Build() ;
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        Hide();
    }

    public void Show() {
        bannerView.Show();
    }
    public void Hide() {
        bannerView.Hide();
    }

}
