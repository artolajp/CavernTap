using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadResources : MonoBehaviour {

	void Start () {
        Resources.UnloadUnusedAssets();
	}
	
}
