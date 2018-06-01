using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BrokenShieldAnimation : MonoBehaviour {

    private Image image;
    void Awake() {
        image = GetComponent<Image>();
    }
	void OnEnable () {
        image.color = Color.white;
        transform.DOShakeScale(0.5f);
        image.DOFade(0,1.0f);
        
	}
}
