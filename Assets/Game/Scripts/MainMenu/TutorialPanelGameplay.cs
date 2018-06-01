using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelGameplay : MonoBehaviour {

    public const string TAP_MONSTER="tut_showed_tapMonster", MULTIPLICATOR = "tut_showed_multiplicator", POWERS = "tut_showed_powers";

    [SerializeField]
    private GameObject panelTutorial = null;
    [SerializeField]
    private TutorialSubPanel panelTutorialTapMonsters=null,panelTutorialMultiplicator=null,panelTutorialPowers=null;
    private static TutorialPanelGameplay instance;

    private bool isShowedTapMonster, isShowedMultiplicator, isShowedPowers;
    private float lastTimeScale;
    public static TutorialPanelGameplay Instance
    {
        get        {
            if (!instance) instance = FindObjectOfType<TutorialPanelGameplay>(); 
            return instance;
        }
        set        {instance = value;}
    }

    private float timeOpenPopUp;
    

    public void ClosePanel() {

        if (Time.realtimeSinceStartup - timeOpenPopUp > 2)
        {
            panelTutorialTapMonsters.gameObject.SetActive(false);
            panelTutorialMultiplicator.gameObject.SetActive(false);
            panelTutorialPowers.gameObject.SetActive(false);
            panelTutorial.SetActive(false);
            Time.timeScale = lastTimeScale;
        }

    }
    public void OpenTapMonster() {
        if (!isShowedTapMonster)
        {
            isShowedTapMonster = true;
            if (!PlayerPrefs.HasKey(TAP_MONSTER))
            {
                Pause();   
                PlayerPrefs.SetInt(TAP_MONSTER, 0);
                panelTutorial.SetActive(true);
                panelTutorialTapMonsters.gameObject.SetActive(true);
            }
        }
        
    }

    public void OpenMultiplicator() {

        if (!isShowedMultiplicator)
        {
            isShowedMultiplicator = true;
            if (!PlayerPrefs.HasKey(MULTIPLICATOR))
            {
                Pause();

                PlayerPrefs.SetInt(MULTIPLICATOR, 0);
                panelTutorial.SetActive(true);
                panelTutorialMultiplicator.gameObject.SetActive(true);
            }
        }
    }

    public void OpenPowers() {
        if (!isShowedPowers)
        {
            isShowedPowers = true;
            if (!PlayerPrefs.HasKey(POWERS))
            {
                Pause();

                PlayerPrefs.SetInt(POWERS, 0);
                panelTutorial.SetActive(true);
                panelTutorialPowers.gameObject.SetActive(true);
            }
        }
    }

    private void Pause() {
        timeOpenPopUp = Time.realtimeSinceStartup;
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }
}
