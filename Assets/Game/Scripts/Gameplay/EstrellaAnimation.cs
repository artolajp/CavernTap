using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EstrellaAnimation : MonoBehaviour {

    
	// Use this for initialization
	void OnEnable () {
        StopAllCoroutines();
        StartCoroutine(Dance());
	}


    private IEnumerator Dance() {
        yield return new WaitForSeconds(1.0f);
        transform.DOShakeRotation(0.5f,40);
        yield return Dance();
    }
}
