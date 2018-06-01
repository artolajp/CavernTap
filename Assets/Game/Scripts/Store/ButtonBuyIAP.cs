using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonBuyIAP : MonoBehaviour {

	private Button button;

	// Use this for initialization
	void Start () {
		button =GetComponent<Button> ();
		button.onClick.AddListener (IAP.Instance.Buy1000Tickets);
	}
}
