using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelExit : MonoBehaviour {

    [SerializeField]
    private GameObject panel;
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!AnimationController.IsAnim)
            {
            
                panel.gameObject.SetActive(true);
            }
        }
#endif
    }

    public void Exit()
    {

        Application.Quit();
    }
    public void ClosePanel()
    {
        panel.gameObject.SetActive(false);

    }
}
