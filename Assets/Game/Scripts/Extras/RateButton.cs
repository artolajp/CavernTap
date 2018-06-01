using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RateButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Rate);
    }
	
	// Update is called once per frame
	void Rate () {
        ShareAndRate.Instance.RateUs();
        PlayerPrefs.SetInt(PanelRate.CountShowedKey, PlayerPrefs.GetInt(PanelRate.CountShowedKey,0));
    }
}
