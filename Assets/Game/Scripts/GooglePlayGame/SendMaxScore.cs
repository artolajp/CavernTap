using UnityEngine;
using System.Collections;

public class SendMaxScore : MonoBehaviour {

    IEnumerator Start() {
        yield return new WaitForSeconds(0.5f);
#if (UNITY_ANDROID || UNITY_IPHONE)
        	GameCloudController.Instance.ReportHighScore(ScoreController.MaxScore);
#endif
    }
}