using UnityEngine;
using System.Collections;
using System;

public class TimerController : MonoBehaviour {

    private static TimerController instance;

    [SerializeField]
    private int MaxCoinsTimer = 5;
    [SerializeField]
    private int waitTimeForOneCoin = 600;
    
    private static DateTime last;
    TimeSpan dif;

    public static TimerController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<TimerController>();
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
        last = DateTime.Parse( PlayerPrefs.GetString("LastTime", DateTime.Now.ToString()));
        Deliver();
    }
	
	// Update is called once per frame
	private void Deliver () {
        int aux = (int)(DateTime.Now - last).TotalSeconds;
        while (CoinsController.Instance.CantCoins < MaxCoinsTimer && aux >= waitTimeForOneCoin)
        {
            CoinsController.Instance.Add(1);
            aux -= waitTimeForOneCoin;
        }
        if (PopUpGetCoins.Instance) PopUpGetCoins.Instance.Close();

    }
    public string TimeLeftForCoins() {
        dif = (last.AddMinutes(waitTimeForOneCoin/60) - DateTime.Now);
        if (dif.TotalSeconds <= 0) Deliver();
        return (
                    dif.Hours.ToString("00") 
            + ":" + dif.Minutes.ToString("00") 
            + ":" + dif.Seconds.ToString("00"));
    }
    public static void ResetTimer() {
        last = DateTime.Now;
        PlayerPrefs.SetString("LastTime", last.ToString());
        PlayerPrefs.Save();
    }
}
