using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CocodrilloController : MonoBehaviour {
    public enum TypeSprite {NORMAL,DEFEAT,OPEN }
    
    [SerializeField]
    private Cocodrilo[] cocodrilos = new Cocodrilo[5];
    [SerializeField]
    private Image background=null,mountain=null;
    [SerializeField]
    private Image[] caves = new Image[5], shadows = new Image[5];

    private float timeToStart, timeBetweenWaves;

    private static CocodrilloController instance;

    public static CocodrilloController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<CocodrilloController>();
            return instance;
        }

    }

    public Sprite getSprite(TypeSprite type,int num) {
        num = Mathf.Clamp(num,1,4);
        switch (type) {
            case TypeSprite.NORMAL: return SkinCollection.Instance.ActualSkin.Monsters[num-1].Normal;
            case TypeSprite.DEFEAT: return SkinCollection.Instance.ActualSkin.Monsters[num - 1].Deafeat;
            case TypeSprite.OPEN :  return SkinCollection.Instance.ActualSkin.Monsters[num - 1].Open;
            default: return null;
        }
        
    }
    public Sprite getFrozenSprite(TypeSprite type,int num)
    {
        switch (type)
        {
            case TypeSprite.NORMAL: return SkinCollection.Instance.ActualSkin.Monsters[num - 1].FrozenNormal;
            case TypeSprite.DEFEAT: return SkinCollection.Instance.ActualSkin.Monsters[num - 1].FrozenDeafeat;
            case TypeSprite.OPEN: return SkinCollection.Instance.ActualSkin.Monsters[num - 1].FrozenOpen;
            default: return null;
        }

    }

    public void TouchCocodrilo(int numCocodrilo) {
        if (numCocodrilo >= 0 && numCocodrilo < cocodrilos.Length)
        {
            ScoreController.Instance.Add(cocodrilos[numCocodrilo].Damage(UserController.Instance.CantDamage));
        }
        else {
            Debug.LogWarning("Error: numero de cocodrilo");
        }
    }
    public void RefreshAllSprite() {
        foreach (Cocodrilo c in cocodrilos) {
            c.RefreshSprites();
        }
        if (PowerController.Instance.IsFrozen)
        {
            background.sprite = SkinCollection.Instance.ActualSkin.BackgroundFrozen;
            mountain.sprite = SkinCollection.Instance.ActualSkin.MountainFrozen;
            foreach (Image i in caves)
            {
                i.sprite = SkinCollection.Instance.ActualSkin.CaveFrozen;
            }
        }
        else {
            background.sprite = SkinCollection.Instance.ActualSkin.BackgroundNormal;
            mountain.sprite = SkinCollection.Instance.ActualSkin.MountainNormal;

            foreach (Image i in caves)
            {
                i.sprite = SkinCollection.Instance.ActualSkin.CaveNormal;
            }

        }
        foreach (Image i in shadows)
        {
            i.sprite = SkinCollection.Instance.ActualSkin.Shadow;
        }
    }

    void Start() {
        RefreshAllSprite();
    }

}
