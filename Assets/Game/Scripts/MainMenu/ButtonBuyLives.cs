using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonBuyLives : MonoBehaviour {
    [SerializeField]
    public int []cost = new int [6];
    [SerializeField]
    private Text text=null;
    [SerializeField]
    private Sprite spritePopUp=null;
    private Button button;

    [SerializeField]
    private InternationalizedTextData msgPopUp;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenPopUp);
        RefreshData();
#if (UNITY_ANDROID || UNITY_IPHONE)
        if (UserController.Instance.CantLives == 6) GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_full_endurance, true); 
#endif

    }

    private void OpenPopUp() {
        if (cost[UserController.Instance.CantLives] <= ScoreController.Instance.CantTickets)
        {
            PopUpUpgrates.Instance.OpenPopUp(msgPopUp.text, cost[UserController.Instance.CantLives],spritePopUp,BuyLives);
        }
        
    }

    private void BuyLives()
    {
        if (cost[UserController.Instance.CantLives ] <= ScoreController.Instance.CantTickets) {
            ScoreController.Instance.CantTickets -= cost[UserController.Instance.CantLives];
            UserController.Instance.CantLives++;
            PlayerPrefs.SetInt(TutorialPanel.EXTRALIFE, 0);
#if (UNITY_ANDROID || UNITY_IPHONE)
            if(UserController.Instance.CantLives==6) GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_full_endurance, true);
            else GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_full_endurance, false);
#endif
            RefreshData();
            UpdateOthersButtons();
        }

    }
    public void RefreshData() {
        if (UserController.Instance.CantLives < 6)
        {
            text.text = cost[UserController.Instance.CantLives].ToString();
            if (cost[UserController.Instance.CantLives] <= ScoreController.Instance.CantTickets)
            {
                button.interactable = true;

            }
            else {
                button.interactable = false;
            }
        }
        else
            gameObject.SetActive(false);
    }

    private void UpdateOthersButtons()
    {
        ButtonBuySwords btnSwords = FindObjectOfType<ButtonBuySwords>();
        if (btnSwords != null) btnSwords.RefreshData();
        ButtonBuyPower btnPower = FindObjectOfType<ButtonBuyPower>();
        if (btnPower != null) btnPower.Refresh();
    }
}
