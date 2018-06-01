using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCollection : MonoBehaviour {
    
	[SerializeField]
    private SkinGameplay crocodiles = null,aliens = null, topos = null, panchos = null, retro = null, dragones = null, fingers = null, jelly = null;

    [SerializeField]
    private SkinController.Skins editorSkin;

    public SkinGameplay ActualSkin {
        get {
            
#if UNITY_EDITOR
            SkinController.ActualSkin = editorSkin;
#endif
            switch (SkinController.ActualSkin)
            {
                case SkinController.Skins.Cocodrilos: return crocodiles;
                case SkinController.Skins.Aliens: return aliens;
                case SkinController.Skins.Dragones: return dragones;
                case SkinController.Skins.Retro: return retro;
                case SkinController.Skins.Panchos: return panchos;
                case SkinController.Skins.Topos: return topos;
                case SkinController.Skins.Fingers: return fingers;
                case SkinController.Skins.Jelly: return jelly;
                default: return null;
            }

        }
    }

    private static SkinCollection instance;
    
    public static SkinCollection Instance { get
        {
            
            if (!instance) instance=FindObjectOfType<SkinCollection>();
            return instance;
        }
    }


}
