using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternationalizedText : InternationalizedObject
{
    [SerializeField]
    private InternationalizedTextData textData;

    public override void Refresh()
    {
                GetComponent<Text>().text = textData.text;
    }
}
