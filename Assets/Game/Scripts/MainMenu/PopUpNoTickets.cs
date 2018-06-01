using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class PopUpNoTickets : MonoBehaviour {

    [SerializeField]
    private Button buttonOk = null;
    [SerializeField]
    private Image image = null;
    [SerializeField]
    private Text  textTickets = null;
    [SerializeField]
    private GameObject panelPopUp = null;

    private static PopUpNoTickets instance;

    public static PopUpNoTickets Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<PopUpNoTickets>();
            return instance;
        }
    }

    public void OpenPopUp( int cantTickets, Sprite sprite)
    {
        buttonOk.onClick.RemoveAllListeners();
        buttonOk.onClick.AddListener(ClosePopUp);
        image.sprite = sprite;
        textTickets.text = cantTickets.ToString();
        panelPopUp.SetActive(true);
    }

    private void ClosePopUp()
    {
        panelPopUp.SetActive(false);
    }

}
