using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class LifeController : MonoBehaviour {

    [SerializeField]
    private GameObject[] vidas = new GameObject[6];

    [SerializeField]
    private GameObject popUpGameOver;

    private int lives;
    private bool isDelivered;
    private static LifeController instance;

    public static LifeController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<LifeController>();
            return instance;
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = Mathf.Clamp(value,0,6);
            RefreshLives();
        }
    }

    // Use this for initialization
    void Start () {
        Lives = UserController.Instance.CantLives;
        isDelivered = false;
        popUpGameOver.SetActive(false);
        Time.timeScale = 1.0f;
    }
	
    public void RemoveLife(int cant) {
        Lives -= cant;
        ScoreController.Instance.Add(0);
        if (Lives < 1)
        {
            if (!isDelivered)
            {
                Time.timeScale = 0.0f;
                isDelivered = true;
                ScoreController.Instance.DeliverTickets();
            }
            SoundController.Instance.IsLooping = false;
            popUpGameOver.SetActive(true);
            DOTween.PauseAll();

        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Sounds.DAMAGE);
        }
    }

    private void RefreshLives() {
        for(int i = 0; i<vidas.Length;i++) {
            if (i < lives) vidas[i].SetActive(true);
            else vidas[i].SetActive(false);

        }
        
    }
    public int AddVida() {
        if (Lives < 6)
        {
            Lives++;
            return 1;
        }
        else
            return 0;

    }
}
