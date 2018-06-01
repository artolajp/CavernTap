using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternationalizedImage : InternationalizedObject
{
    [SerializeField]
    private InternationalizedImageData imageData;

    public override void Refresh() {
                GetComponent<Image>().sprite = imageData.sprite;

    }


}
