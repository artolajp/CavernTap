using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemSkinStore : MonoBehaviour
{
    public enum TicketType {Normal,Gold,Puntos}
    [SerializeField]
    private TicketType type;
    [SerializeField]
    private int price;
    [SerializeField]
    private SkinController.Skins skin;
    [SerializeField]
    private string textPopUp;
    [SerializeField]
    private Image imgSkin = null;
    [SerializeField]
    private Sprite owned=null;
    [SerializeField] 
    Button buttonOn = null, buttonOff = null, buttonBuy = null;
    [SerializeField]
    Text textPrice = null;

    public TicketType Type
    {
        get
        {
            return type;
        }

        private set
        {
            type = value;
        }
    }

    void Start()
    {
        Refresh();
        buttonBuy.onClick.RemoveAllListeners();
        if (Type == TicketType.Puntos && ScoreController.MaxScore >= price) BuySkin();
        else buttonBuy.onClick.AddListener(ButtonBuy);
        buttonOff.onClick.RemoveAllListeners();
        buttonOff.onClick.AddListener(SelectSkin);
        buttonOn.onClick.RemoveAllListeners();
        
    }

    private void ButtonBuy() {
        if (Type == TicketType.Normal&& ScoreController.Instance.CantTickets >= price)
        {
            PopUpUpgrates.Instance.OpenPopUp("BUY\n<color=white>" + textPopUp + "</color>\nFOR?", price, imgSkin.sprite, BuySkin);
        }
        
    }
    private void BuySkin() {
        if (ScoreController.Instance.CantTickets >= price) {
            SkinController.BuySkin(skin);
            ScoreController.Instance.CantTickets -= price;
#if (UNITY_ANDROID || UNITY_IOS)
            if (Type != TicketType.Puntos)
            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_new_skin, true);
#endif
        }
        Refresh();
    }

    private void Refresh() {
        textPrice.text = price.ToString("000");
        if (SkinController.IsOwned(skin))
        {
            GetComponent<Image>().sprite = owned;
            buttonBuy.gameObject.SetActive(false);
            if (skin == SkinController.ActualSkin)
            {
                buttonOff.gameObject.SetActive(false);
                buttonOn.gameObject.SetActive(true);
            }
            else
            {
                buttonOff.gameObject.SetActive(true);
                buttonOn.gameObject.SetActive(false);
            }
        }
        else
        {
            textPrice.text = price.ToString("000");
            buttonBuy.gameObject.SetActive(true);
            buttonOff.gameObject.SetActive(false);
            buttonOn.gameObject.SetActive(false);
        }
    }
    private void SelectSkin()
    {
        SkinController.ActualSkin = skin;
        ItemSkinStore[] items = FindObjectsOfType<ItemSkinStore>();
        foreach(ItemSkinStore i in items)i.Refresh();
    }
}
