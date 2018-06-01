using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CocodriloLvlUpText : MonoBehaviour {
    private Text text;
    private Color startColor = Color.white;
    private Color targetColor = new Color(255, 255, 255, 0);


    void Awake() {
        text = GetComponent<Text>();
    }
	void OnEnable() {
        
        StartCoroutine(EffectText());
    }

    private IEnumerator EffectText() {

        text.color = targetColor;
        text.DOColor(startColor, 1f);
        yield return new WaitForSeconds(1);
        text.DOColor(targetColor, 1f);

    }

}
