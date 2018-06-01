using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonBuyPower : MonoBehaviour {
    [SerializeField]
    private int price=50;
    [SerializeField]
    private Sprite spritePopUp = null;
    [SerializeField]
    private Text textPrice=null,textFrozen = null, textBlock = null;
    private Button button;


    [SerializeField]
    private InternationalizedTextData msgPopUp;
    // Use this for initialization
    void Awake() {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenPopUp);
        textPrice.text = price.ToString();
        
    }
    void Start() {

        Refresh();
    }
    private void OpenPopUp()
    {
        if (price <= ScoreController.Instance.CantTickets)
        {
            PopUpUpgrates.Instance.OpenPopUp(msgPopUp.text, price, spritePopUp, BuyPower);
        }

    }
    private void BuyPower() {
        if (ScoreController.Instance.CantTickets >= price)
        {

            PowerController.AddPower(1);
            ScoreController.Instance.CantTickets -= price;
            Refresh();
            UpdateOthersButtons();
        }
    }
    public void Refresh() {
        if (ScoreController.Instance.CantTickets>=price) button.interactable=true;
        else button.interactable = false;
        textFrozen.text = PowerController.CantFrozen.ToString();
        textBlock.text = PowerController.CantBlocks.ToString();
        if (PowerController.IsFull()) button.gameObject.SetActive(false);
    }

    private void UpdateOthersButtons()
    {
        ButtonBuyLives btnLives = FindObjectOfType<ButtonBuyLives>();
        if (btnLives != null) btnLives.RefreshData();
        ButtonBuySwords btnSwords = FindObjectOfType<ButtonBuySwords>();
        if (btnSwords != null) btnSwords.RefreshData();
    }
}
