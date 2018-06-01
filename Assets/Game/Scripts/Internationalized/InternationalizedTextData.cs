using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ImageData-", menuName = "Internationalized/Text", order = 1)]

public class InternationalizedTextData : ScriptableObject {
    [SerializeField,TextArea]
    private string text_EN="",text_ES="";

    public string text {
        get {
            switch (InternationalizedController.Instance.CurrentLenguage)
            {
                case InternationalizedController.Language.EN:
                    return text_EN;
                case InternationalizedController.Language.ES:
                    return text_ES;

            }
            return "";
        }
    }

}
