using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ImageData-", menuName = "Internationalized/Image", order = 1)]

public class InternationalizedImageData : ScriptableObject {
    [SerializeField]
    private string image_EN = null, image_ES = null;

    public Sprite Image_EN
    {
        get
        {
            return Resources.Load<Sprite>(image_EN);
        }
    }

    public Sprite Image_ES
    {
        get
        {
            return Resources.Load<Sprite>(image_ES);
        }
    }

    public Sprite sprite {
        get {
            
                switch (InternationalizedController.Instance.CurrentLenguage)
                {
                    case InternationalizedController.Language.EN:
                        return Image_EN;
                    case InternationalizedController.Language.ES:
                        return Image_ES;

                }
                return null;
            
        }
    }

}
