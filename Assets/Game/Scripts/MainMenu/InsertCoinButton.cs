using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InsertCoinButton : MonoBehaviour {

    private Button button;
    [SerializeField]
    private int cost =1;
    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
        if (button == null) Debug.Log("button dont exist");
        else
        {
            
            Refresh();
        }
    }

    private void Click()
    {
        CoinsController.Instance.Remove(cost);
        SceneController.Instance.GoToScene(SceneController.Scenes.Gameplay);
    }
    public void BlockButton() {
        if (button == null) Debug.Log("button dont exist");
        else
        {
            button.onClick.RemoveAllListeners();
            if (SceneController.ActualScene == SceneController.Scenes.Menu)
            {
                button.onClick.AddListener(OpenPopUpGetCoins);
            }
            else
            {
                button.interactable = false;
            }
            
        }
    }
    public void ActiveButton() {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            
            button.onClick.AddListener(Click);
            button.interactable = true;
        }
    }
    private void OpenPopUpGetCoins() {
        TutorialPanel.Instance.OpenExtraCoins();
        PopUpGetCoins.Instance.OpenPopUp();
    }

    private void Refresh() {
        if (CoinsController.Instance.CantCoins > 0) ActiveButton();
        else BlockButton();
    }

}
