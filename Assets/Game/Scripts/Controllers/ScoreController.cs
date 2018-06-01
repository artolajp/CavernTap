using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    [SerializeField]
    private Text scoreText=null,scoreTextGameOver=null,waveText=null,maxScoreText=null,maxWaveText=null,ticketsText=null,ticketsWinText=null;
    [SerializeField]
    private GameObject newHighScore=null,panelLvlUpCocodrile=null;
    [SerializeField]
    private int cantX2=10, cantX3=20, cantX4=30;
    [SerializeField]
    private GameObject mulX2=null, mulX3=null, mulX4=null,barraMultiplicador=null,barraPorcentaje=null;
    private int score,wave,racha,multiplicador;
    private static int cantTickets,maxScore,maxWave;
    private Vector2 sizeBarraMultiplicador;
    private static ScoreController instance;
    

    public static ScoreController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<ScoreController>();
            return instance;
        }
    }

    public int CantTickets
    {
        get
        {
            cantTickets = PlayerPrefs.GetInt("CantTickets",0);
            return cantTickets;
        }

         set
        {
            cantTickets = value;
            PlayerPrefs.SetInt("CantTickets", cantTickets);
            PlayerPrefs.Save();

        }
    }

    public static int MaxScore
    {
        get
        {
            maxScore = PlayerPrefs.GetInt("MaxScore", 0);
            return maxScore;
        }

        set
        {
            maxScore = value;
            PlayerPrefs.SetInt("MaxScore", maxScore);
            PlayerPrefs.Save();
#if UNITY_ANDROID ||UNITY_IPHONE
            GameCloudController.Instance.ReportHighScore(maxScore);
            if (maxScore >= 500)
            {
                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_500_pts, true);
                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_1000_pts, false);
                if (maxScore >= 1000)
                {
                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_1000_pts, true);
                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_2000_pts, false);
                    if (maxScore >= 2000)
                    {
                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_2000_pts, true);
                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_5000_pts, false);
                        if (maxScore >= 5000)
                        {
                            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_5000_pts, true);
                            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_10000_pts, false);
                            if (maxScore >= 10000)
                            {
                                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_10000_pts, true);
                                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_20000_pts, false);
                                if (maxScore >= 20000)
                                {
                                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_20000_pts, true);
                                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_30000_pts, false);
                                    if (maxScore >= 30000)
                                    {
                                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_30000_pts, true);
                                    }
                                }
                            }
                        }
                    }

                }
            }
#endif 
        }
    }

    public static int MaxWave
    {
        get
        {
            maxWave = PlayerPrefs.GetInt("MaxWave", 0);
            return maxWave;
        }

        set
        {
            maxWave = value;
            PlayerPrefs.SetInt("MaxWave", maxWave);
            PlayerPrefs.Save();
#if UNITY_ANDROID || UNITY_IPHONE
            if (maxWave >= 10)
            {
                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_10_waves, true);
                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_20_waves, false);
                if (maxWave >= 20)
                {
                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_20_waves, true);
                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_30_waves, false);
                    if (maxWave >= 30)
                    {
                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_30_waves, true);
                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_40_waves, false);
                        if (maxWave >= 40)
                        {
                            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_40_waves, true);
                            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_50_waves, false);
                            if (maxWave >= 50)
                            {
                                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_50_waves, true);
                                GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_60_waves, false);
                                if (maxWave >= 60)
                                {
                                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_60_waves, true);
                                    GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_70_waves, false);
                                    if (maxWave >= 70)
                                    {
                                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_70_waves, true);
                                        GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_80_waves, false);
                                        if (maxScore >= 80)
                                        {
                                            GameCloudController.Instance.UnlockAchievement(GPGSlds.achievement_80_waves, true);

                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
#endif 
        }
    }

    public int Wave
    {
        get
        {
            return wave;
        }

        set
        {
            wave = value;
            if (wave == 10) TutorialPanelGameplay.Instance.OpenPowers();
        }
    }

    public int Racha
    {
        get
        {
            return racha;
        }

        set
        {
            racha = value;
            if (racha < cantX2)
            {
                Multiplicador = 1;
                if (mulX2) mulX2.SetActive(false);
                if (mulX3) mulX3.SetActive(false);
                if (mulX4) mulX4.SetActive(false);
            }
            else if (racha < cantX3)
            {
                TutorialPanelGameplay.Instance.OpenMultiplicator();
                Multiplicador = 2;
                mulX2.SetActive(true);
            }
            else if (racha < cantX4)
            {
                Multiplicador = 3;
                mulX3.SetActive(true);
            }
            else if (racha > 0)
            {
                Multiplicador = 4;
                mulX4.SetActive(true);
            }
            else {
                Multiplicador = 1;
                if(mulX2)mulX2.SetActive(false);
                if (mulX3) mulX3.SetActive(false);
                if (mulX4) mulX4.SetActive(false);
            }

        }
    }

    public int Multiplicador
    {
        get
        {
            return multiplicador;
        }

        private set
        {
            multiplicador = value;
        }
    }


    // Use this for initialization
    void Start() {
        cantTickets = CantTickets;
        maxScore = MaxScore;
        maxWave = MaxWave;
        Racha = 0;
        score = 0;
        wave = 1;
        if (newHighScore) newHighScore.SetActive(false);
        if (barraMultiplicador) sizeBarraMultiplicador = barraMultiplicador.GetComponent<RectTransform>().sizeDelta;
        if (barraPorcentaje) barraPorcentaje.GetComponent<RectTransform>().sizeDelta = new Vector2(0, sizeBarraMultiplicador.y);
        Racha = 0;
        



    }
	
	// Update is called once per frame
	void Update () {
        if(scoreText)scoreText.text = score.ToString("000000");
        if(scoreTextGameOver)scoreTextGameOver.text = score.ToString("0000");
        if (waveText)waveText.text = "WAVE "+wave.ToString("00");
        if (ticketsText) ticketsText.text = cantTickets.ToString("00000");
        if (maxScoreText) maxScoreText.text = maxScore.ToString("0000");
        if (maxWaveText) maxWaveText.text = maxWave.ToString("000"); ;
    }

    public void Add(int addScore) {
        if (addScore > 0)
        {
            Racha += 1;
            score += addScore * Multiplicador;

        }
        else {
            Racha = 0;
            SoundController.Instance.PlaySound(SoundController.Sounds.FAIL);
            PanelGameplay.Instance.ShakePanel();
        }
        SetBarra();
        Wave = (Cocodrilo.CantCocodrilesAttack / 10) + 1;
    }
    public int AddWithoutRacha(int addScore)
    {
        int pts = addScore * Multiplicador;
        score += pts;
        return pts;
    }

    public void DeliverTickets() {
        int ticketsWin = Wave+score/50;
        CantTickets += ticketsWin ;
        if (ticketsWinText) ticketsWinText.text = ticketsWin.ToString();
        if (score > MaxScore)
        {
            SoundController.Instance.PlaySound(SoundController.Sounds.GAMEOVERRECORD);
            MaxScore = score;
            if (newHighScore) newHighScore.SetActive(true);
        }
        else {
            SoundController.Instance.PlaySound(SoundController.Sounds.GAMEOVER);
        }
        if (wave > MaxWave) MaxWave = wave;
        
    }

    private void SetBarra() {
        if (Multiplicador==1)
        {
            barraPorcentaje.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBarraMultiplicador.x * (racha * 1.0f / cantX2), sizeBarraMultiplicador.y);
        }
        else if (Multiplicador == 2)
        {
            barraPorcentaje.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBarraMultiplicador.x * ((racha- cantX2) * 1.0f / (cantX3-cantX2)), sizeBarraMultiplicador.y);

        }
        else if (Multiplicador == 3)
        {
            barraPorcentaje.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBarraMultiplicador.x * ((racha- cantX3) * 1.0f / (cantX4-cantX3)), sizeBarraMultiplicador.y);

        }
        else
        {
            barraPorcentaje.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBarraMultiplicador.x, sizeBarraMultiplicador.y);
        }
    }

    private int actualLiveCocodrile=0,lastLiveCocodrile=0;

    public int ActualLiveCocodrile
    {
        get
        {
            return actualLiveCocodrile;
        }

        set
        {
            actualLiveCocodrile = value;
            if (actualLiveCocodrile > lastLiveCocodrile) {
                panelLvlUpCocodrile.SetActive(true);
                lastLiveCocodrile = actualLiveCocodrile;
            }

        }
    }

}
