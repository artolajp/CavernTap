using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemSkinStoreAllInOne : MonoBehaviour
{
    public enum TicketType {Normal,Gold,Puntos}
    [SerializeField]
    private TicketType type;
    [SerializeField]
    private Skin skin;
    [SerializeField]
    private Image imgSkin = null;
    [SerializeField] 
    Button buttonOn = null, buttonOff = null, buttonBuy = null;
    [SerializeField]
    Text textPrice = null;
    [SerializeField]
    private InternationalizedTextData textFree;

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
        if (Type == TicketType.Puntos && ScoreController.MaxScore >= skin.Price) BuySkin();
        else buttonBuy.onClick.AddListener(ButtonBuy);
        buttonOff.onClick.RemoveAllListeners();
        buttonOff.onClick.AddListener(SelectSkin);
        buttonOn.onClick.RemoveAllListeners();
        
        
    }

    private void ButtonBuy() {
        if (Type == TicketType.Normal && ScoreController.Instance.CantTickets >= skin.Price)
        {
            PopUpUpgrates.Instance.OpenPopUp(skin.TextPopUp, skin.Price, skin.ImageNormal, BuySkin);
        }
        else {
            PopUpNoTickets.Instance.OpenPopUp( skin.Price, skin.ImageNormal);

        }

    }
    private void BuySkin() {
        if (ScoreController.Instance.CantTickets >= skin.Price) {
            ScoreController.Instance.CantTickets -= skin.Price;
            SkinController.BuySkin(skin.SkinReference);
#if (UNITY_ANDROID || UNITY_IPHONE)
            if(Type != TicketType.Puntos)
                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_new_skin, true);
#endif
        }
        SelectSkin();
    }

    private void Refresh() {
        //textPrice.text = price.ToString("000");
        if (SkinController.IsOwned(skin.SkinReference))
        {
            //GetComponent<Image>().sprite = owned;
            imgSkin.sprite = skin.ImageNormal;
            textPrice.gameObject.SetActive(false);
            buttonBuy.gameObject.SetActive(false);
            if (skin.SkinReference == SkinController.ActualSkin)
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
            imgSkin.sprite = skin.ImageNotOwned;

            textPrice.gameObject.SetActive(true);
            if(skin.Price>0)
                textPrice.text = skin.Price.ToString("000");
            else
                textPrice.text = textFree.text;

            buttonBuy.gameObject.SetActive(true);
            buttonOff.gameObject.SetActive(false);
            buttonOn.gameObject.SetActive(false);
        }
    }
    private void SelectSkin()
    {
        SkinController.ActualSkin = skin.SkinReference;
        ItemSkinStoreAllInOne[] items = FindObjectsOfType<ItemSkinStoreAllInOne>();
        foreach(ItemSkinStoreAllInOne i in items)i.Refresh();
    }
}
