using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Cocodrilo : MonoBehaviour {

    [SerializeField]
    private float speed = 0.02f, timeCycle = 2, timeWait = 1, timeDelay = 0;
    private float auxTimeCycle, auxMove;
    [SerializeField]
    private bool isMoving, isOut;
    private bool isWaiting;
    [SerializeField]
    private GameObject targetPoint = null;
    [SerializeField]
    private MonsterBar bar=null;
    
    private int live;
    private int startLive, codReturn, actualCodReturn,type=1;

    private static int cantCocodrilesAttack = 0;
    private Button button;

    private Vector3 startPosition, targetPosition;
    private Vector2 sizeBarraDeVida,sizeBarraDeTiempo;
    [SerializeField]
    private Rocks rocas;
    [SerializeField]
    private Cave cave;
    private Sprite normal, defeat, open;
    private Image image;

    public static int CantCocodrilesAttack
    {
        get
        {
            return cantCocodrilesAttack;
        }

        set
        {
            cantCocodrilesAttack = value;
        }
    }

    public int Live
    {
        get
        {
            return live;
        }

        set
        {
            live = Mathf.Clamp(value, 0, startLive);
            if (live > 0)
            {
                button.interactable = true;
                bar.SetActive(true);
                bar.Lives = live;

            }
            else {
                bar.SetActive(false);
                bar.Lives = live;
                button.interactable = false;
                
            }

        }
    }

    

    // Use this for initialization
    void Start() {
        isMoving = isOut = false;
        bar.SetActive(false);
        startPosition = transform.position;
        auxTimeCycle = timeCycle + timeDelay;
        targetPosition = targetPoint.transform.position;
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.interactable = false;
        CantCocodrilesAttack = 0;
        Live=0;
        actualCodReturn = 0;
        codReturn = 1;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!isMoving && !isOut)
        {
            
                auxTimeCycle -= Time.fixedDeltaTime;

            if (!rocas.IsBlock&&PowerController.Instance.IsBlocked)
            {
                auxTimeCycle += PowerController.Instance.TimeBloked;
                rocas.Block();
            }

            if (auxTimeCycle <= 0)
                {
                    Salir();
                }
            
        }
        else if (isMoving && !isOut)
        {
            auxMove += speed;
            Salir(auxMove);
            if (auxMove >= 1)
            {
                isOut = true;
                isMoving = false;
            }
        }
        else if (!isMoving && isOut)
        {
            if (!isWaiting)
            {
                auxMove = 0;
                
                StartCoroutine(WaitCocodrileisOut(timeWait));
            }
        }
        else if (isMoving && isOut) {
            auxMove += speed / 2;
            Volver(auxMove);
            if (auxMove >= 1)
            {
                Reposar();
            }

        }
    }

    private void Salir(float delta) {
        transform.position = Vector3.Lerp(startPosition, targetPosition, delta);


    }
    private void Volver(float delta) {
        transform.position = Vector3.Lerp(targetPosition, startPosition, delta);

    }
    private IEnumerator WaitCocodrileisOut(float t) {

        
        if (Live > 0)
        {
            image.sprite = open;
        }
        isWaiting = true;
        int auxCod = actualCodReturn;
        float auxTime;
        int subTime = 4;


        for (int i = 0; i < subTime||Live==0; ++i)
        {
            
            if (PowerController.Instance.IsFrozen) auxTime = t * 8;
            else auxTime = t;
            float waitTime = auxTime / subTime + (0.20f * type / subTime);
            if (auxCod == codReturn)
            {
                
                bar.SetTime( (subTime - i - 1) * 1.0f / subTime,waitTime);
            }
            yield return new WaitForSeconds(waitTime);
        }
        Back(auxCod);
    }
    

    public int Damage(int lives) {
        StartCoroutine(AnimationDamage());
        if (Live > 0)
        {
            int lastLive = Live;
            Live -= lives;


            if (Live == 0)
            {
                SoundController.Instance.PlaySound(SoundController.Sounds.TAP);
                CantCocodrilesAttack += 1;
                image.sprite = defeat;
                transform.DOShakeScale(0.4f);
                Back(actualCodReturn);
            }
            else {
                SoundController.Instance.PlaySound(SoundController.Sounds.TAP2);
            }
            return lastLive-Live;
        }
        else  return 0;


    }
    private IEnumerator AnimationDamage() {
        image.DOColor(Color.red,0.05f);
        yield return new WaitForSeconds(0.05f);
        image.DOColor(Color.white, 0.05f);

    }
    private void Back(int cod) {
        
        if (cod== codReturn)
        {
            codReturn++;
            isMoving = true;
            isOut = true;
            isWaiting = false;
            if (Live > 0) {
                image.sprite = normal;
            }

        }

    }
    
    private void Salir() {

        if (ScoreController.Instance.Wave < 3) {
            SetLives(1);
        }
        else if (ScoreController.Instance.Wave < 5) SetLives((int)Random.Range(1, 2.1f));
        else if (ScoreController.Instance.Wave < 7) SetLives((int)Random.Range(1, 3.1f));
        else SetLives((int)Random.Range(1, 3.9f));
        SoundController.Instance.PlaySound(SoundController.Sounds.EXITCAVE);
        image.sprite = normal;
        isMoving = true;
        auxMove = 0;
        actualCodReturn=++codReturn;
        if (rocas.IsBlock)
        {
            rocas.DestroyBlock();
        }




        }
    private void Reposar() {
        if (Live > 0)
        {

            LifeController.Instance.RemoveLife(1);
            cave.Shake();
        }
        auxTimeCycle = timeCycle;
        isOut = false;
        isMoving = false;
        if (Random.Range(0, 100) > 80) auxTimeCycle = auxTimeCycle * 0.1f;
        auxMove = 0;
        timeWait = timeWait * 0.96f;
        Live = 0;


    }

    private void SetLives(int lives) {
        type =Mathf.Clamp( lives,1,1000);
        SetSprites();
        startLive = type;
        if (ScoreController.Instance.Wave < 10) {
        }
        else if (ScoreController.Instance.Wave < 30)
        {
            startLive += 1+(ScoreController.Instance.Wave-10) / 5;
            ScoreController.Instance.ActualLiveCocodrile = 1 + (ScoreController.Instance.Wave - 10) / 5;
        }
        else {
            startLive += 5 + (ScoreController.Instance.Wave - 30) / 2;
            ScoreController.Instance.ActualLiveCocodrile = 5 + (ScoreController.Instance.Wave - 30) / 2;

        }

        Live = startLive;

    }

    private void SetSprites() {
        if (!PowerController.Instance.IsFrozen)
        {
            normal = CocodrilloController.Instance.getSprite(CocodrilloController.TypeSprite.NORMAL, type);
            defeat = CocodrilloController.Instance.getSprite(CocodrilloController.TypeSprite.DEFEAT, type);
            open = CocodrilloController.Instance.getSprite(CocodrilloController.TypeSprite.OPEN, type);
        }else
        {
            normal = CocodrilloController.Instance.getFrozenSprite(CocodrilloController.TypeSprite.NORMAL, type);
            defeat = CocodrilloController.Instance.getFrozenSprite(CocodrilloController.TypeSprite.DEFEAT, type);
            open = CocodrilloController.Instance.getFrozenSprite(CocodrilloController.TypeSprite.OPEN, type);

        }
    }
    public void RefreshSprites() {
        SetSprites();
        if (isWaiting) image.sprite = open;
    }
}
