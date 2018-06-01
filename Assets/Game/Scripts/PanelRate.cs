using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRate : MonoBehaviour {
    [SerializeField]
    private GameObject panel;

    public const string RateKey = "rateScore",CountShowedKey = "rateCountShowed";
    public void OpenPanel() {
        int countShowed = PlayerPrefs.GetInt(CountShowedKey,0);
        if (PlayerPrefs.GetInt(RateKey, 200) < ScoreController.MaxScore&&SceneController.Instance.CanShowRatePanel()&&countShowed<2)
        {

            panel.SetActive(true);
            PlayerPrefs.SetInt(RateKey, ScoreController.MaxScore);
            PlayerPrefs.Save();
        }
    }
    public void ClosePanel() {
        panel.SetActive(false);
    }

}
