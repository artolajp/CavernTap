using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonLeaderboard : MonoBehaviour {
    [SerializeField]
    private bool openAllLeaderboard;
    private Button button;
	
	void Start () {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenLeaderboard);
	}

    private void OpenLeaderboard() {
        #if (UNITY_ANDROID || UNITY_IPHONE)
		    if (openAllLeaderboard)
		        GameCloudController.Instance.ShowLeaderBoard();
		    else GameCloudController.Instance.ShowLeaderBoard(GPGSlds.leaderboard_high_score);

#endif
    }
}
