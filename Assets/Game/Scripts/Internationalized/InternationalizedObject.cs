using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InternationalizedObject : MonoBehaviour {
    protected enum InternationalizedType {IMAGE,TEXT }
	// Use this for initialization
	void Start () {
        Refresh();
	}

    public abstract void Refresh();
    
}
