using UnityEngine;
using System.Collections;

public class GoToSceneInSeconds : MonoBehaviour {

    [SerializeField]
    private float seconds=0;
    [SerializeField]
    private GameObject[] splashs;
    [SerializeField]
    private SceneController.Scenes nextScene;
    [SerializeField]
    private bool loadingScreen = true;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitToChange(seconds));
	}

    IEnumerator WaitToChange(float s) {
        for (int i = 0; i<splashs.Length;++i) {
            foreach (GameObject g in splashs) g.SetActive(false) ;
            splashs[i].SetActive(true);
            yield return new WaitForSecondsRealtime(s);
        }
        
        SceneController.Instance.GoToScene(nextScene,loadingScreen);
    }
	
}
