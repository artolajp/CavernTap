using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerLeft : MonoBehaviour {
    Text text;

    void Start() {
        text = GetComponent<Text>();
    }
	void Update () {
        text.text = TimerController.Instance.TimeLeftForCoins();
	}
}
