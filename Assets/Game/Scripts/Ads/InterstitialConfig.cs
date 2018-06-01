using UnityEngine;
using System.Collections;

public class InterstitialConfig : MonoBehaviour {

    void Start()
    {
        if (InterstitialController.Instance)
        {
            InterstitialController.Instance.RequestInterstitial();
            if (SceneController.Instance.CanShowInterstitial())
            {
                InterstitialController.Instance.Show();
            }
        }
    }

    void OnDestroy() {
        if(InterstitialController.Instance) InterstitialController.Instance.DestroyInterstitial();
    }
}
