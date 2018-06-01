using UnityEngine;
using System.Collections;

public class ResetMaxScore : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("ResetedMaxScore", 0) != 2) {
            ScoreController.MaxScore = 0;
            PlayerPrefs.SetInt("ResetedMaxScore", 2);
        }
    }
}
