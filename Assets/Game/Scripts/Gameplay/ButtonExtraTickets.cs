using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonExtraTickets : MonoBehaviour {
    private Button button;
    [SerializeField]
    private int cantTickets = 10;
    [SerializeField]
    private Text textConfirmTickets = null, textCantTickets=null;

	void Start () {
        button = GetComponent<Button>();
        button.interactable =true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ClickAddTickets);
        textCantTickets.text = cantTickets.ToString();
        
    }
    private void ClickAddTickets()
    {

        //InterstitialController.Instance.Show(AddTicketsAds);
        //UnityAdsController.Instance.ShowRewardedAd(AddTicketsAds);
        RewardedVideoController.Instance.Show(AddTicketsAds);

    }

    private void AddTicketsAds(bool show)
    {
        if (show)
        {
            ScoreController.Instance.CantTickets+=cantTickets;
            button.interactable = false;
            textConfirmTickets.text = (int.Parse(textConfirmTickets.text) +cantTickets).ToString();
            Color c = textCantTickets.color;
            c.a = 0.2f;
            textCantTickets.color = c ; 
        }
    }

}
