using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternationalizedController:MonoBehaviour {

    public enum Language { EN,ES }
    private static Language currentLanguage;

    private bool isUpdate;
    private const string LANGUAGE_KEY = "language";

    public Language CurrentLenguage
    {
        get {
            if (!isUpdate) {
                string lan="";
                if (!PlayerPrefs.HasKey(LANGUAGE_KEY))
                {
                    switch (Application.systemLanguage)
                    {
                        case SystemLanguage.Spanish:
                            lan = Language.ES.ToString();
                            break;
                        case SystemLanguage.English:
                            lan = Language.EN.ToString();
                            break;
                        default:
                            lan = Language.EN.ToString();
                            break;
                    }
                    PlayerPrefs.SetString(LANGUAGE_KEY, lan);

                }
                else
                {
                    lan = PlayerPrefs.GetString(LANGUAGE_KEY, Language.EN.ToString());
                }
                if (lan.Equals(Language.EN.ToString())) currentLanguage = Language.EN;
                else if (lan.Equals(Language.ES.ToString())) currentLanguage = Language.ES;
                isUpdate = true;
            }
            return currentLanguage;

        }
        set{
            PlayerPrefs.SetString(LANGUAGE_KEY, value.ToString());
            isUpdate = false;
        }

    }

    private static InternationalizedController instance;

    public static InternationalizedController Instance{
        get {
            if (!instance) instance = FindObjectOfType<InternationalizedController>();
            if (!instance) {
                instance = new GameObject("Internationalize Controller").AddComponent<InternationalizedController>();
                DontDestroyOnLoad(instance.gameObject);

            }

            return instance;
        }
    }

    public void RefreshCurrentScene() {

         InternationalizedObject []  internationalizedObjects =  FindObjectsOfType<InternationalizedObject>();
        foreach (InternationalizedObject i in internationalizedObjects) {
            i.Refresh();
        }
    }
	
}
