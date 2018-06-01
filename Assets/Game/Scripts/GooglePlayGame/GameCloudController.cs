using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCloudController : MonoBehaviour {
    private static GameCloudServices instance;
    public static GameCloudServices Instance
    {
        get {
                if (!instance) {
#if UNITY_ANDROID
                    instance = FindObjectOfType<GooglePlayGameController>();
                    if (!instance)
                        instance = new GameObject("GameCloudServices").AddComponent<GooglePlayGameController>();

                        instance.transform.parent = FindObjectOfType<GameCloudController>().transform;
#elif UNITY_IOS

                    instance = FindObjectOfType<GameCenterController>();
                if (!instance)
                {
                    instance = new GameObject("GameCloudServices").AddComponent<GameCenterController>();
                    instance.transform.parent = FindObjectOfType<GameCloudController>().transform;
                }
#endif
            }
            return instance;
        }
            
        
    }

    void Start()
    {
        instance = Instance;
    }
	
}
