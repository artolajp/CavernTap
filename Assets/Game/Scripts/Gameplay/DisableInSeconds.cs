using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInSeconds : MonoBehaviour {

    private float timeToDisable = 2.0f; 
	void OnEnable () {
        StartCoroutine(DisableGameObject());
	}
    private IEnumerator DisableGameObject() {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }

}
