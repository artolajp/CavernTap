using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    void Start()
    {
        SceneController.Instance.GoToScene();
    }
}
