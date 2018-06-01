using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Skin/Info", order = 0)]
public class Skin : ScriptableObject {
    [SerializeField]
    private SkinController.Skins skinReference;
    [SerializeField]
    private int price;
    [SerializeField]
    private string imageNormal, imageNotOwned;
    [SerializeField]
    private InternationalizedTextData textDataPopUp;

    public int Price
    {
        get
        {
            if (SkinController.SkinFree)
                return 0;
            else
                return price;
        }
    }

    public Sprite ImageNormal
    {
        get
        {
            return Resources.Load<Sprite>( imageNormal);
        }
    }

    public Sprite ImageNotOwned
    {
        get
        {
            return Resources.Load<Sprite>(imageNotOwned);
        }
    }

    public string TextPopUp
    {
        get
        {
            return textDataPopUp.text;
        }
    }

    public SkinController.Skins SkinReference
    {
        get
        {
            return skinReference;
        }
    }
}
