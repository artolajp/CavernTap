using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonAchievements : MonoBehaviour {
   #if (UNITY_ANDROID || (UNITY_IPHONE ))
    private Button button;
	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenAchievements);
	}

    private void OpenAchievements() {
        
        GameCloudController.Instance.ShowAchievements();
    }

#endif
}
