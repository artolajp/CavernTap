using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour {

    private const string MENU = "MainMenu";
    private const string STORE= "Store AllInOne";
    private const string LOADING= "Loading";
    private const string GAMEPLAY = "Gameplay";
    private const string START = "Start";
    private static Scenes actualScene;
    private static Scenes lastScene = Scenes.NoDefined;
    public enum Scenes {NoDefined,Menu,Tienda,Gameplay,Start}
    private static SceneController instance;

	//private static bool isCacheCleaned;
    public static SceneController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<SceneController>();
            if (!instance)
            {
                instance = new GameObject("Scene Controller").AddComponent<SceneController>();
                DontDestroyOnLoad(instance.gameObject);

            }
            return instance;
        }
    }

	//public void Start(){
	//	if (!isCacheCleaned) {
	//		isCacheCleaned = true;
	//		Caching.ClearCache ();
	//	}
	//}

    public static Scenes ActualScene
    {
        get
        {
            return actualScene;
        }

        set
        {
            actualScene = value;
        }
    }

    public void GoToScene(Scenes scene, bool loadingScreen = true) {
        if (scene != Scenes.NoDefined)
        {
            lastScene = actualScene;
			ActualScene = scene;
            if (loadingScreen) SceneManager.LoadScene(LOADING);
            else GoToScene();
        }
    }
    public void GoToScene()
    {
        if (ActualScene != Scenes.NoDefined)
        {
            SceneManager.LoadScene(GetNameScene(ActualScene));
        }
    }
    private string GetNameScene(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                return MENU;
            case Scenes.Gameplay: return GAMEPLAY;
            case Scenes.Tienda: return STORE;
            case Scenes.Start: return START;
            default: return "";
        }

    }
    public bool CanShowInterstitial() {
        if (ActualScene == Scenes.Menu && lastScene != Scenes.Tienda&& lastScene!= Scenes.NoDefined)
            return true;
        else return false;
    }

    public bool CanShowRatePanel() {
        return lastScene != Scenes.NoDefined;
    }
}
