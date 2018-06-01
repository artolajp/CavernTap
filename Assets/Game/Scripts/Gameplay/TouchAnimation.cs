using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAnimation : MonoBehaviour {

    private Animator anim;
    void Awake() {
        anim =GetComponent<Animator>();
        
    }
   
    public void Play(Vector3 pos) {
        gameObject.SetActive(true);
        isAnim = true;
        anim.Play("Touch");
        transform.position = pos;
    }

    public void Desactivate() {
        gameObject.SetActive(false);
        isAnim = false;
    }

    private bool isAnim = false;
    public bool IsAnim {
        get { return isAnim; }
    }
}
