using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour {
    private Button button;
    [SerializeField]
    private float force=0.4f;
	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
	}

    void OnEnable() {
        StartCoroutine(Anim());

    }

    // Update is called once per frame
    IEnumerator Anim () {
        yield return new WaitForSeconds(3.0f);
        if(button.interactable)transform.DOShakeScale(force,0.3f);
        yield return Anim();
	}
}
