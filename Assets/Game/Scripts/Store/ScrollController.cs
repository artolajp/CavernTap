using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class ScrollController : MonoBehaviour,/*IScrollHandler,*/IEndDragHandler,IBeginDragHandler{

    ScrollRect sr;
    [SerializeField]
    private Sprite fondoGold=null,fondoPuntos = null,fondoTickets = null;
    [SerializeField]
    private ItemSkinStore[] skins;
    [SerializeField]
    private Image background;
    [SerializeField]
    private float[] normalPositions;
    private int actualPosition,lastPosition;

    private float positionItemCentro;

    private static ScrollController instance;

    public int ActualPosition
    {
        get
        {
            return actualPosition;
        }

        set
        {
            
            actualPosition = value;
        }
    }
    public int TotalItems { get {
            return skins.Length - 1 ;
        }
    }
    public int Displacement
    {
        get
        {
            return skins.Length *430;
        }
    }
    public int Lap
    {
        get
        {
            return sr.horizontalNormalizedPosition>=0?(int)sr.horizontalNormalizedPosition: (int)sr.horizontalNormalizedPosition-1;
        }
    }


    public static ScrollController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<ScrollController>();
            return instance;
        }
    }

    public float PositionItemCentro
    {
        get
        {
            return positionItemCentro;
        }

        set
        {
            positionItemCentro = value;
        }
    }

    // Use this for initialization
    void Awake () {
        sr = GetComponent<ScrollRect>();
        Reposition();
        lastPosition = 0;
    }

	
	// Update is called once per frame
	void Refresh () {
        /*if (sr.horizontalNormalizedPosition >= 0)
        {
            ActualPosition = (int)Mathf.Clamp((sr.horizontalNormalizedPosition - ((int)sr.horizontalNormalizedPosition)) * (skins.Length + 1), 0, skins.Length );
        }
        else {
            ActualPosition = (int)Mathf.Clamp((sr.horizontalNormalizedPosition+1 - ((int)sr.horizontalNormalizedPosition)) * (skins.Length + 1), 0, skins.Length);
        }
        auxiliarPosition = ActualPosition == skins.Length?0: ActualPosition;*/
        if (ActualPosition != lastPosition)
        {
            //Debug.Log(lastPosition);
            lastPosition = ActualPosition;
            switch (ActualPosition > 0 ? skins[ActualPosition].Type : ItemSkinStore.TicketType.Gold)
            {
                case ItemSkinStore.TicketType.Gold:
                    background.sprite = fondoGold;
                    break;
                case ItemSkinStore.TicketType.Normal:
                    background.sprite = fondoTickets;
                    break;
                case ItemSkinStore.TicketType.Puntos:
                    background.sprite = fondoPuntos;
                    break;
            }
        }
        
	}
    private IEnumerator Reposition() {
        while (Mathf.Abs(sr.velocity.x) > 150) yield return new  WaitForEndOfFrame() ;
        bool isReposition = true;
        while (isReposition)
        {
            if (PositionItemCentro > 5)
            {
                sr.velocity = new Vector2(-150  , 0);
            }
            else if (PositionItemCentro < -5)
            {
                sr.velocity = new Vector2(150, 0);
            }
            else { sr.velocity = new Vector2(0, 0);
                isReposition=false;
            }
            yield return new WaitForEndOfFrame();
            //sr.horizontalNormalizedPosition = ((int)(sr.horizontalNormalizedPosition * skins.Length)/(skins.Length*1.0f));
            //Debug.Log(((int)(sr.horizontalNormalizedPosition * actualPosition)) / (skins.Length * 1.0f));
        }
     
    }

    
   void Update()
    {
        
        Refresh();
        
    }

    /*public void OnScroll(PointerEventData eventData)
    {
        Refresh();

    }*/

    public void OnEndDrag(PointerEventData eventData)
    {
        
        StartCoroutine( Reposition());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
        //StopCoroutine(Reposition());
    }
}
