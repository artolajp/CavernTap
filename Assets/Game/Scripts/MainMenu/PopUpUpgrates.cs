using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class PopUpUpgrates : MonoBehaviour {

    [SerializeField]
    private Button buttonOk=null, buttonCancel=null;
    [SerializeField]
    private Image image=null;
    [SerializeField]
    private Text textTitle=null,textTickets=null;
    [SerializeField]
    private GameObject panelPopUp=null;
    [SerializeField]
    private InternationalizedTextData freeText;


    private static PopUpUpgrates instance;

    public static PopUpUpgrates Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<PopUpUpgrates>();
            return instance;
        }
    }

    public void OpenPopUp(string title, int cantTickets,Sprite sprite,UnityAction callBack) {
        buttonOk.onClick.RemoveAllListeners();
        buttonOk.onClick.AddListener(callBack);
        buttonOk.onClick.AddListener(ClosePopUp);
        buttonCancel.onClick.RemoveAllListeners();
        buttonCancel.onClick.AddListener(ClosePopUp);
        image.sprite = sprite;
        textTitle.text = title;
        textTickets.text = cantTickets>0? cantTickets.ToString():freeText.text;
        panelPopUp.SetActive(true);
    }

    private void ClosePopUp() {
        panelPopUp.SetActive(false);
    }
}
