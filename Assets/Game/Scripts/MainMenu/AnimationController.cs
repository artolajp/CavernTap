using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {
    [SerializeField]
    Button button = null;
    [SerializeField]
    Image[] fadeInImages;
    [SerializeField]
    Text[] fadeInText;
    [SerializeField]
    float timeToAppear = 3;
    private Color textColor;
    [SerializeField]
    Image curita = null;
    [SerializeField]
    GameObject leftPanel = null, rightPanel=null, head=null;
    private Vector3 lpPosition, rpPosition, hPosition;
    private bool isFinishedImages, isFinishedCurita;

    private  static bool isAnim = false;

    public static bool IsAnim
    {
        get
        {
            return isAnim;
        }

        set
        {
            isAnim = value;
        }
    }


    // Use this for initialization
    IEnumerator Start() {
        isAnim=true;
        button.onClick.AddListener(finishAll);
        hPosition = head.transform.position;
        head.transform.position = hPosition + Vector3.up * 700;
        
        lpPosition = leftPanel.transform.position;
        leftPanel.transform.position = lpPosition + Vector3.left * 500;
        rpPosition = rightPanel.transform.position;
        rightPanel.transform.position = rpPosition + Vector3.right * 500;
        curita.color = new Color(1, 1, 1, 0);

        foreach (Image o in fadeInImages)
        {
            o.color = new Color(1, 1, 1, 0);

        }
        textColor = fadeInText[0].color;
        foreach (Text t in fadeInText)
        {
            t.color = new Color(1, 1, 1, 0);

        }
        Time.timeScale = 1;

        yield return new WaitForEndOfFrame();

        leftPanel.transform.DOMoveX(lpPosition.x, timeToAppear);
        head.transform.DOMoveY(hPosition.y, timeToAppear - 1.1f);
        rightPanel.transform.DOMoveX(rpPosition.x, timeToAppear);
       

       
        yield return new WaitForSeconds(timeToAppear - 1);
        FinishCurita();
        yield return new WaitForSeconds(1);
        FinishImageAndText();
        button.gameObject.SetActive(false);
        IsAnim = false;
    }

    private void FinishImageAndText() {
        if (!isFinishedImages)
        {
            isFinishedImages = true;
            foreach (Image o in fadeInImages)
            {
                o.DOColor(new Color(1, 1, 1, 1), 0.5f);

            }
            foreach (Text t in fadeInText)
            {
                t.DOColor(textColor, 0.5f);

            }
            TutorialPanel.Instance.ShowTutorial();
        }

        
    }
    private void FinishCurita() {
        if(!isFinishedCurita){
            isFinishedCurita = true;
            curita.DOColor(new Color(1, 1, 1, 1), 0.5f);
        }
    }
    private void FinishTranslates() {
        head.transform.DOKill();
        head.transform.position = hPosition;
        leftPanel.transform.DOKill();
        leftPanel.transform.position = lpPosition ;
        rightPanel.transform.DOKill();
        rightPanel.transform.position = rpPosition;
    }
    private void finishAll()
    {
        FinishImageAndText(); FinishCurita();
        FinishTranslates();
        button.gameObject.SetActive(false);
    }
}
