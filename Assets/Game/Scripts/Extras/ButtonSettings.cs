using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSettings : MonoBehaviour {

    [SerializeField]
    private SettingsController.Options option;
    [SerializeField]
    private Button buttonOn = null, buttonOff = null;

    void Start() {
        buttonOn.onClick.AddListener(ChangeOption);
        buttonOn.onClick.AddListener(Refresh);
        buttonOff.onClick.AddListener(ChangeOption);
        buttonOff.onClick.AddListener(Refresh);
    }
	void OnEnable () {
        Refresh();
	}

    private void Refresh() {
        switch (option) {
            case SettingsController.Options.Music:
                buttonOn.gameObject.SetActive(SettingsController.Music);
                buttonOff.gameObject.SetActive(!SettingsController.Music);
                break;
            case SettingsController.Options.Sfx:
                buttonOn.gameObject.SetActive(SettingsController.Sfx);
                buttonOff.gameObject.SetActive(!SettingsController.Sfx);
                break;
        }
    }

    private void ChangeOption() {
        switch (option)
        {
            case SettingsController.Options.Music:
                SettingsController.Music=!SettingsController.Music;
                if (SoundController.Instance) SoundController.Instance.PlayMusic();
                break;
            case SettingsController.Options.Sfx:
                SettingsController.Sfx = !SettingsController.Sfx;
                break;
        }
    }
}
