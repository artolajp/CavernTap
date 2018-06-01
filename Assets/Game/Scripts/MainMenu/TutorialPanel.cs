using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour {

    public const string EXTRACOINS="tut_showed_extraCoins", EXTRALIFE = "tut_showed_extraLife", ExTRAPOWERS = "tut_showed_extraPower", EXTRADAMAGE = "tut_showed_extraDamage",STORE="tut_showed_store";

    [SerializeField]
    private GameObject panelTutorial = null,panelTickets=null,panelCoins=null;
    private Transform originalTransformPanel;
    [SerializeField]
    private TutorialSubPanel panelTutorialExtraLife=null,panelTutorialExtraDamage=null,panelTutorialExtraPower=null,panelTutorialExtraCoins = null,panelTutorialStore = null;
    private bool isTicketsShow;
    private static TutorialPanel instance;

    [SerializeField]
    private int cantTicketShowExtraLife = 25, cantTicketShowExtraPower = 50, cantTicketShowExtraDamage = 300;
    [SerializeField]
    private PanelRate ratePanel=null;

    public static TutorialPanel Instance
    {
        get        {
            if (!instance) instance = FindObjectOfType<TutorialPanel>(); 
            return instance;
        }
        set        {instance = value;}
    }
    

    public void ClosePanel() {
        if(isTicketsShow) panelTickets.transform.SetParent(originalTransformPanel);
        else panelCoins.transform.SetParent(originalTransformPanel);
        panelTutorialExtraLife.gameObject.SetActive(false);
        panelTutorialExtraDamage.gameObject.SetActive(false);

        panelTutorial.SetActive(false);
       
    }

    private void HighlightPanelTickets() {
        originalTransformPanel = panelTickets.transform.parent;
        panelTickets.transform.SetParent(panelTutorial.transform);
        isTicketsShow = true;
    }
    private void HighlightPanelCoins()
    {
        originalTransformPanel = panelCoins.transform.parent;
        panelCoins.transform.SetParent(panelTutorial.transform);
        isTicketsShow = false;
    }
    public void OpenExtraLife() {

        panelTutorial.SetActive(true);
        panelTutorialExtraLife.gameObject.SetActive(true);
        HighlightPanelTickets();
    }

    public void OpenExtraDamage() {

        panelTutorial.SetActive(true);
        panelTutorialExtraDamage.gameObject.SetActive(true);
        HighlightPanelTickets();
    }

    public void OpenExtraPower() {
        panelTutorial.SetActive(true);
        panelTutorialExtraPower.gameObject.SetActive(true);
        HighlightPanelTickets();
    }

    public void OpenExtraCoins() {
        if (!PlayerPrefs.HasKey(EXTRACOINS))
        {
            PlayerPrefs.SetInt(EXTRACOINS, 0);
            PlayerPrefs.Save();

            panelTutorial.SetActive(true);
            panelTutorialExtraCoins.gameObject.SetActive(true);
            HighlightPanelCoins();
        }

    }
    public void OpenStore()
    {
        panelTutorial.SetActive(true);
        panelTutorialStore.gameObject.SetActive(true);
    }

    private bool isShowed;
    public void ShowTutorial() {
        isShowed = false;

        if (!PlayerPrefs.HasKey(EXTRALIFE))
        {
            if (ScoreController.Instance.CantTickets >= cantTicketShowExtraLife)
            {
                PlayerPrefs.SetInt(EXTRALIFE, 0);
                PlayerPrefs.Save();

                OpenExtraLife();
                isShowed = true;
            }
        }
        else if (!PlayerPrefs.HasKey(EXTRADAMAGE))
        {
            if (ScoreController.Instance.CantTickets >= cantTicketShowExtraDamage)
            {
                PlayerPrefs.SetInt(EXTRADAMAGE, 0);
                PlayerPrefs.Save();

                OpenExtraDamage();
                isShowed = true;

            }
        }
        else if (!PlayerPrefs.HasKey(ExTRAPOWERS))
        {
            if (ScoreController.Instance.CantTickets >= cantTicketShowExtraPower)
            {
                PlayerPrefs.SetInt(ExTRAPOWERS, 0);
                PlayerPrefs.Save();

                OpenExtraPower();
                isShowed = true;

            }
        }

        

        if(!isShowed) {

            if (!PlayerPrefs.HasKey(STORE)&&ScoreController.MaxScore>0)
            {
                PlayerPrefs.SetInt(STORE, 0);
                PlayerPrefs.Save();
                if (SkinController.SkinFree)
                {
                    OpenStore();
                    isShowed = true;
                }
            }
            else
            {
                ratePanel.OpenPanel();
                isShowed = true;
            }

        }
    }
}
