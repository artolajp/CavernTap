using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneChangeButton : MonoBehaviour {

    private Button button;
    [SerializeField]
    SceneController.Scenes scena;
    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        if (button != null) {            
            button.onClick.AddListener(Click);
        }
	}

    private void Click() {
        SceneController.Instance.GoToScene(scena);
    }
}
