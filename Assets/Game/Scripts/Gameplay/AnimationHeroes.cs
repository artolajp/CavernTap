using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationHeroes : MonoBehaviour {

    private Vector3 startPosition;
    private float dif=1000.0f;

        
	void Awake() {
        startPosition = transform.position;
	}

    void OnEnable() {
        transform.Translate(Vector3.left * dif);
        transform.DOMove(startPosition, 1.0f);
    }
    
}
