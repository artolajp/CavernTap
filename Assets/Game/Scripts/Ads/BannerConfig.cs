using UnityEngine;
using System.Collections;

public class BannerConfig : MonoBehaviour {

	// Use this for initialization
	void OnEnable() {
        if(BannerController.Instance)
            BannerController.Instance.Show();
	}
    
    void OnDisable() {
        if (BannerController.Instance)
            BannerController.Instance.Hide();
    }

}
