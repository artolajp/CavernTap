using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSubPanel : MonoBehaviour {
    [SerializeField]
    private GameObject originalPanel=null;
    [SerializeField]
    private Button buttonClose=null;
    private Transform lastTransform=null;

    void Start() {
        
        buttonClose.onClick.AddListener(ClosePanel);
        if (originalPanel)
        {
            lastTransform = originalPanel.transform.parent;
            originalPanel.transform.SetParent(gameObject.transform);
        }

    }

    private void ClosePanel() {
        if (originalPanel)
        {
            originalPanel.transform.SetParent(lastTransform);
        }
        if(TutorialPanel.Instance)
        TutorialPanel.Instance.ClosePanel();
        else if (TutorialPanelGameplay.Instance)
            TutorialPanelGameplay.Instance.ClosePanel();

    }
}
