using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelGameplay : MonoBehaviour {

	private static PanelGameplay instance;

    private GameObject panel;
    private float timeLastShake;

    public static PanelGameplay Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<PanelGameplay>();

            return instance;
        }
    }

    private GameObject Panel
    {
        get
        {
            if (!panel) panel = Instance.gameObject;
            return panel;
        }
    }

    public void ShakePanel() {

        if(Time.timeSinceLevelLoad-timeLastShake>0.5f)
        {
            Panel.transform.localPosition = new Vector3() ;
            Panel.transform.DOShakePosition(0.5f, 30, 20);
            timeLastShake = Time.timeSinceLevelLoad;
        }

    }

    
}
