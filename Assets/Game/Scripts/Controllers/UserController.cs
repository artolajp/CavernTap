using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

    [SerializeField]
    private GameObject []vidas = new GameObject[6], espadas = new GameObject[6];

    private int cantLives, cantDamage;

    private static UserController instance;

    public int CantDamage
    {
        get
        {
            
            return cantDamage;
        }

        set
        {
            cantDamage = Mathf.Clamp(value, 1, 6);
            PlayerPrefs.SetInt("cantDamage", cantDamage);
            PlayerPrefs.Save();
            RefreshDamage();
        }
    }

    public int CantLives
    {
        get
        {
            
            return cantLives;
        }

        set
        {
            cantLives = Mathf.Clamp( value,1,6);
            PlayerPrefs.SetInt("cantLives", cantLives);
            PlayerPrefs.Save();
            RefreshLives();
        }
    }

    public static UserController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<UserController>();
            return instance;
        }
    }
    void Awake() {
        cantDamage = PlayerPrefs.GetInt("cantDamage", 1);
        cantLives = PlayerPrefs.GetInt("cantLives", 2);
    }

    void Start () {
        
        if(espadas[0]!=null)RefreshDamage();
        if(vidas[0] != null) RefreshLives();
    }

    private void RefreshLives() {
        for (int i = 0; i < vidas.Length; ++i)

        {
            if (i < cantLives) vidas[i].SetActive(true);
            else vidas[i].SetActive(false);
        }
    }

    private void RefreshDamage()
    {
        for (int i = 0; i < espadas.Length; ++i)

        {
            if (i < cantDamage) espadas[i].SetActive(true);
            else espadas[i].SetActive(false);
        }
    }
}
