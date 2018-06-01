using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Skin/Gameplay", order = 0)]
public class SkinGameplay : ScriptableObject {
    [System.Serializable]
    public class Monster
    {
        [SerializeField]
        private string  normal, open, deafeat, frozenNormal, frozenOpen, frozenDeafeat;

        public Sprite Normal
        {
            get
            {
                return Resources.Load<Sprite>(normal);
            }
        }

        public Sprite Open
        {
            get
            {
                return Resources.Load<Sprite>(open);
            }
        }

        public Sprite Deafeat
        {
            get
            {
                return Resources.Load<Sprite>(deafeat);
            }
        }

        public Sprite FrozenNormal
        {
            get
            {
                return Resources.Load<Sprite>(frozenNormal);
            }
        }

        public Sprite FrozenOpen
        {
            get
            {
                return Resources.Load<Sprite>(frozenOpen);
            }
        }

        public Sprite FrozenDeafeat
        {
            get
            {
                return Resources.Load<Sprite>(frozenDeafeat);
            }
        }

    }
    [SerializeField]
    private List<Monster> monsters=new List<Monster>(3);
    [SerializeField]
    private string backgroundNormal=null, backgroundFrozen=null;
    [SerializeField]
    private string mountainNormal = null, mountainFrozen = null;
    [SerializeField]
    private string caveNormal = null, caveFrozen = null;
    [SerializeField]
    private string shadow = null;
    [SerializeField]
    private GameObject prefabStar = null,prefabHeart = null;

    public List<Monster> Monsters
    {
        get
        {
            return monsters;
        }
    }

    public Sprite BackgroundNormal
    {
        get
        {
            return Resources.Load<Sprite>(backgroundNormal);
        }
    }

    public Sprite BackgroundFrozen
    {
        get
        {
            return Resources.Load<Sprite>(backgroundFrozen);
        }
    }

    public Sprite MountainNormal
    {
        get
        {
            return Resources.Load<Sprite>(mountainNormal);
        }
    }

    public Sprite MountainFrozen
    {
        get
        {
            return Resources.Load<Sprite>( mountainFrozen);
        }
    }

    public Sprite CaveNormal
    {
        get
        {
            return Resources.Load<Sprite>( caveNormal);
        }
    }

    public Sprite CaveFrozen
    {
        get
        {
            return Resources.Load<Sprite>( caveFrozen);
        }
    }

    public Sprite Shadow
    {
        get
        {
            return Resources.Load<Sprite>( shadow);
        }
    }

    public GameObject PrefabStar
    {
        get
        {
            return prefabStar;
        }
    }

    public GameObject PrefabHeart
    {
        get
        {
            return prefabHeart;
        }
    }
}