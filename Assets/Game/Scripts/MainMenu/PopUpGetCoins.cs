using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpGetCoins : MonoBehaviour {
    [SerializeField]
    private GameObject panel=null;
    [SerializeField]
    private Text  cantCoins=null;
    [SerializeField]
    private Button buttonAdd = null, buttonClose = null;

    private static PopUpGetCoins instance;

    public static PopUpGetCoins Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<PopUpGetCoins>();
            return instance;
        }
    }


    void Start() {
        cantCoins.text = CoinsController.Instance.CoinForAds.ToString();
        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(Close);
        buttonAdd.onClick.RemoveAllListeners();
        buttonAdd.onClick.AddListener(ClickAddCoins);
    }
    public void OpenPopUp() {
        panel.SetActive(true);
    }
    public void Close() {
        panel.SetActive(false);

    }


    private void ClickAddCoins()
    {

        //InterstitialController.Instance.Show(AddCoinsAds);
        //UnityAdsController.Instance.ShowRewardedAd(AddCoinsAds);
        RewardedVideoController.Instance.Show(AddCoinsAds);

    }

    private void AddCoinsAds(bool show)
    {
        if (show)
        {
            CoinsController.Instance.Add(CoinsController.Instance.CoinForAds);
            Close();
        }
        else {
        }
    }

}
