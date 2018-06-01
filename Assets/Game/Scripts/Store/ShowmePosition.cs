using UnityEngine;
using System.Collections;

public class ShowmePosition : MonoBehaviour
{
    [SerializeField]
    private int position;
    private static float maxDistanceZ = 200;
    private float distance = 200.0f,initialPositionX;
    private RectTransform rect;
    float auxPosX;
    private bool isLeftCorner, isRightCorner;
    void Awake() {
        rect = GetComponent<RectTransform>();
        initialPositionX = transform.localPosition.x;
        isRightCorner = position > ScrollController.Instance.TotalItems - 2;
        isLeftCorner = position < 2;
        
    }
    void Start() {
        //Reposition(true);
    }
    void Update()
    {
        //Reposition();
        //rect.localPosition = new Vector3(auxPosX, transform.localPosition.y,
        rect.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 
            Mathf.Lerp( maxDistanceZ,0, Mathf.Clamp01(transform.position.x > 1 ? (distance - transform.position.x+20) / distance : (distance + transform.position.x+20) / distance)));
        if (transform.position.x < 100 && transform.position.x > -100) { ScrollController.Instance.PositionItemCentro = transform.position.x;
            ScrollController.Instance.ActualPosition = position;
        }
        
    }
    void  Reposition() {
        if (transform.position.x > 500 || transform.position.x < -500)
        {
            /*if (isRightCorner && ScrollController.Instance.ActualPosition < 2)
            {
                auxPosX = initialPositionX + (ScrollController.Instance.Displacement * (ScrollController.Instance.Lap - 1));
            }
            else if (isLeftCorner && ScrollController.Instance.ActualPosition > ScrollController.Instance.TotalItems - 2)
            {
                auxPosX = initialPositionX + (ScrollController.Instance.Displacement * (ScrollController.Instance.Lap + 1));
            }
            else
            {*/
                auxPosX = initialPositionX + (ScrollController.Instance.Displacement * ScrollController.Instance.Lap);
            //}
        }
    }
    void Reposition(bool force)
    {
        if (force ||transform.position.x > 500 || transform.position.x < -500)
        {
           /* if (isRightCorner && ScrollController.Instance.ActualPosition < 2)
            {
                auxPosX = initialPositionX + (ScrollController.Instance.Displacement * (ScrollController.Instance.Lap - 1));
            }
            else if (isLeftCorner && ScrollController.Instance.ActualPosition > ScrollController.Instance.TotalItems - 2)
            {
                auxPosX = initialPositionX + (ScrollController.Instance.Displacement * (ScrollController.Instance.Lap + 1));
            }
            else
            {*/
                auxPosX = initialPositionX + (ScrollController.Instance.Displacement * ScrollController.Instance.Lap);
            //}
        }
    }
}