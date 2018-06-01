using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinsController : MonoBehaviour {

    private const int CANT_INITIAL_COINS = 5,CANT_MAX_COINS = 999; 
    [SerializeField]
    private Text textCoins=null;
    [SerializeField]
    private int coinForAds = 5;
    private int cantCoins;
    private static CoinsController instance;

    public int CantCoins
    {
        get
        {
            cantCoins = PlayerPrefs.GetInt("CantCoins", CANT_INITIAL_COINS);
            
            return cantCoins;
            
        }

        private set
        {
            
            cantCoins =Mathf.Clamp( value,0,CANT_MAX_COINS);
            PlayerPrefs.SetInt("CantCoins", cantCoins);
            
            PlayerPrefs.Save();
            RefreshButtonsInsertCoins();
            
        }
    }

    public static CoinsController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<CoinsController>();
            return instance;
        }
    }

    public int CoinForAds
    {
        get
        {
            return coinForAds;
        }

       private set
        {
            coinForAds = value;
        }
    }

    // Use this for initialization
    void Start () {
        cantCoins = CantCoins;

    }
	
	// Update is called once per frame
	void Update () {
        if (textCoins) textCoins.text = cantCoins.ToString("000");
	}
    public void Add(int coins)
    {
        CantCoins += coins;
        TimerController.ResetTimer();
    }
    public void Remove(int coins)
    {
        
#if  (UNITY_ANDROID || (UNITY_IPHONE))
        CantCoins -=coins;
        TimerController.ResetTimer();
#endif
    }


    private void BlockAllButtonInsertCoins() {
        InsertCoinButton[] list = FindObjectsOfType<InsertCoinButton>();
        foreach (InsertCoinButton b in list) {
            b.BlockButton();
        }
    }
    private void ActiveAllButtonInsertCoins() {
        InsertCoinButton[] list = FindObjectsOfType<InsertCoinButton>();
        foreach (InsertCoinButton b in list)
        {
            b.ActiveButton();
        }

    }

    private void RefreshButtonsInsertCoins()
    {
        if (cantCoins > 0 )
        {
            ActiveAllButtonInsertCoins();
        }
        else
        {
            BlockAllButtonInsertCoins();
        }

    }
}
