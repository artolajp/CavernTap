using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonBuySwords : MonoBehaviour {
    [SerializeField]
    public int []cost = new int [6];
    [SerializeField]
    private Text text;
    [SerializeField]
    private Sprite spritePopUp = null;
    private Button button;
    
    [SerializeField]
    private InternationalizedTextData msgPopUp;
    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenPopUp);
        RefreshData();
#if (UNITY_ANDROID || UNITY_IPHONE)
        if (UserController.Instance.CantDamage == 6) GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_full_damage, true);
#endif

    }
    private void OpenPopUp()
    {
        if (cost[UserController.Instance.CantDamage] <= ScoreController.Instance.CantTickets)
        {
            PopUpUpgrates.Instance.OpenPopUp(msgPopUp.text, cost[UserController.Instance.CantDamage], spritePopUp, BuySword);
        }

    }

    private void BuySword()
    {
        if (cost[UserController.Instance.CantDamage ] <= ScoreController.Instance.CantTickets) {
            
            ScoreController.Instance.CantTickets -= cost[UserController.Instance.CantDamage];
            UserController.Instance.CantDamage++;
            PlayerPrefs.SetInt(TutorialPanel.EXTRADAMAGE, 0);
#if (UNITY_ANDROID || UNITY_IPHONE)
            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_sword_upgrade, true);
            if(UserController.Instance.CantDamage ==6) GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_full_damage,true);
            else GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_full_damage, false);
#endif
            RefreshData();
            UpdateOthersButtons();
        }

    }
    public void RefreshData() {
        if (UserController.Instance.CantDamage < 6)
        {
            text.text = cost[UserController.Instance.CantDamage].ToString();

            if (cost[UserController.Instance.CantDamage] <= ScoreController.Instance.CantTickets)
            {
                button.interactable = true;

            }
            else
            {
                button.interactable = false;
            }
        }
        else
            gameObject.SetActive(false);
    }

    private void UpdateOthersButtons()
    {
        ButtonBuyLives btnLives = FindObjectOfType<ButtonBuyLives>();
        if (btnLives != null) btnLives.RefreshData();
        ButtonBuyPower btnPower = FindObjectOfType<ButtonBuyPower>();
        if (btnPower != null) btnPower.Refresh();
    }
}
