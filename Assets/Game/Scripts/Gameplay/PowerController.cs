using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PowerController : MonoBehaviour {

    [SerializeField]
    private float timeSlow = 10, timeBloked = 8;
    [SerializeField]
    private int cantBlocks = 2;
    private int actualCantBlocks = 0;

    private bool isFrozen;
    [SerializeField]
    private float heroTime = 2.0f;
    [SerializeField]
    private GameObject panelPowers, powerSlow, powerBlocked;

    private static PowerController instance;

    public static PowerController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<PowerController>();
            return instance;
        }
    }


    public bool IsFrozen
    {
        get
        {
            return isFrozen;
        }

        set
        {
            isFrozen = value;
        }
    }

    public bool IsBlocked
    {
        get
        {
            if (actualCantBlocks > 0)
            {
                actualCantBlocks--;
                return true;
            }
            else return false;
        }

    }

    public float TimeBloked
    {
        get
        {
            return timeBloked;
        }
    }

    public enum Power { FREEZE, BLOCK, BURN, POISON, INVENSIBLE }

    public static void AddPower(int cant) {
        CantFrozen += cant;
        CantBlocks += cant;
    }
    public bool HasPower(Power power) {
        switch (power) {
            case Power.FREEZE: return CantFrozen > 0;
            case Power.BLOCK: return CantBlocks > 0;
            default: return false;
        }
    }
    public int CantPower(Power power) {
        switch (power) {
            case Power.FREEZE:return   CantFrozen ;
            case Power.BLOCK: return CantBlocks;
            default: return 0;
        }
    }
    public static bool IsFull() {
        return (CantFrozen>=CANTMAXPOWERS&&CantBlocks>=CANTMAXPOWERS);
    }
        private const int CANTMAXPOWERS = 50;
    private const int CANTINICIALPOWERS = 10;
    public static int CantFrozen {
        get { return PlayerPrefs.GetInt("CantFrozenPower",CANTINICIALPOWERS); }
        private set { PlayerPrefs.SetInt("CantFrozenPower",Mathf.Clamp(value,0,CANTMAXPOWERS)); }
    }
    public static int CantBlocks
    {
        get { return PlayerPrefs.GetInt("CantBlockPower", CANTINICIALPOWERS); }
        private set { PlayerPrefs.SetInt("CantBlockPower", Mathf.Clamp(value, 0, CANTMAXPOWERS)); }
    }

    void Start() {

        isFrozen=false;
    }

    public bool ActivePower(Power power) {
            switch (power)
            {
                case Power.FREEZE:
                    StartCoroutine(Slow());
                    CantFrozen--;
                    break;
                case Power.BLOCK:
                    BlockCaves();
                    CantBlocks--;
                    break;

            }
            return HasPower(power);
    }

    private IEnumerator Slow() {
        StartCoroutine( ShowIntroPowers());
        powerSlow.SetActive(true);

        IsFrozen = true;
        CocodrilloController.Instance.RefreshAllSprite();
        yield return new WaitForSeconds(timeSlow);
        IsFrozen = false;
        CocodrilloController.Instance.RefreshAllSprite();
    }
    private void BlockCaves() {
        StartCoroutine(ShowIntroPowers());
        powerBlocked.SetActive(true);

        actualCantBlocks += cantBlocks;
    }

    private IEnumerator ShowIntroPowers() {
        DOTween.PauseAll();
        panelPowers.SetActive(true);
        float aux = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(heroTime);
        panelPowers.SetActive(false);
        powerBlocked.SetActive(false);
        powerSlow.SetActive(false);
        Time.timeScale = aux;
        DOTween.PlayAll();

    }
}
