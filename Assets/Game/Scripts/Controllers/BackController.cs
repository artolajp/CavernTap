using UnityEngine;
using System.Collections;

public class BackController : MonoBehaviour {
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            Application.Quit();
        }
	}
}
