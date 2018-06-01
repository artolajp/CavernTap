using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterBar : MonoBehaviour {

    [SerializeField]
    private Text textCountLives;
    [SerializeField]
    private Image timeBar;
    [SerializeField]
    private GameObject shield, shieldBroken;

    private bool isNotFirstTime=true;
    
    public void SetTime(float value,float timeAnim) {
            //DOTween.defaultEaseType = Ease.Linear;
            timeBar.DOFillAmount(Mathf.Clamp01(value),timeAnim);
    }
    public int Lives { set {
            textCountLives.text = ((value+ UserController.Instance.CantDamage-1) /UserController.Instance.CantDamage).ToString();
            shield.transform.localScale = Vector3.one;
            shield.transform.DOShakeScale(0.2f,0.2f);
            if (isNotFirstTime) {
                isNotFirstTime = false;
            }else if(value <= 0)
                shieldBroken.SetActive(true);

        }
    }
    public void SetActive(bool value) {
        if (value)
        {
            shieldBroken.SetActive(false);
        }
        else {

            timeBar.DOKill();
            timeBar.fillAmount = 1;
        }
        shield.SetActive(value);
    }
}
