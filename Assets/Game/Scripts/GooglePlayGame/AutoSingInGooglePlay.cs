using UnityEngine;
using System.Collections;

public class AutoSingInGooglePlay : MonoBehaviour {

    private static bool trySingIn=false;
	void Start () {
#if (UNITY_ANDROID || UNITY_IOS)
        //if(PlayerPrefs.HasKey("GameCloudSingIn"))
        if (!trySingIn)
        {
            trySingIn = true;
            GameCloudController.Instance.SingIn(Callback);
        }
#endif
    }

    void Callback()
    {
        Debug.Log("SingIn");
    }
}
