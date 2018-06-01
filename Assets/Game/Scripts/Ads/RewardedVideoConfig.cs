using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedVideoConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(RewardedVideoController.Instance)
        RewardedVideoController.Instance.RequestReward();
	}
}
