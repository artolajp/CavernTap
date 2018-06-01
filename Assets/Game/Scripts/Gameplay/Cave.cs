using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Cave : MonoBehaviour {
    public void Shake(){
        transform.DOShakeRotation(0.5f, 30,20);
        transform.DOShakeScale(0.5f,0.1f,20);
    }
}
