using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonPause : MonoBehaviour {
    [SerializeField]
    private GameObject fondoPausa;
    private static bool isPause;
    [SerializeField]
    private bool restartFlag;
    //private static float lastTimeScale;

    public static bool IsPause
    {
        get
        {
            return isPause;
        }

        private set
        {
            isPause = value;
        }
    }

    void Start () {
        if (restartFlag) IsPause = false;
        GetComponent<Button>().onClick.AddListener(ClickButton);

    }

    private void Pausa() {
        DOTween.PauseAll();
        IsPause = true;
        Time.timeScale = 0.0f;
        SoundController.Instance.PlaySound(SoundController.Sounds.PAUSA);
        
        fondoPausa.SetActive(true);
    }
    private void Play() {
        Time.timeScale = 1.0f;
        IsPause = false;
        fondoPausa.SetActive(false);
        DOTween.PlayAll();

    }

    private void ClickButton()
    {
        if (!IsPause)
        {
            
            Pausa();
            
        }
        else {
            
            Play();
            
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!IsPause&&pauseStatus) {
            Pausa();
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!IsPause && !hasFocus)
        {
            Pausa();
        }
    }
}
