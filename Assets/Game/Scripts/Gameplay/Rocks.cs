using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Rocks : MonoBehaviour {

    [SerializeField]
    private Sprite[] anim = new Sprite[3];
    [SerializeField]
    private Sprite idle;
    private bool isBlock;

    private Image image;

    public bool IsBlock
    {
        get
        {
            return isBlock;
        }

        private set
        {
            isBlock = value;
        }
    }

    void Start() {
        image = GetComponent<Image>();
        IsBlock = false;
        image.enabled=false;
    }
    public void Block()
    {
        image.enabled=true;
        image.color = Color.white;
        image.sprite = idle;
        isBlock = true;
    }
    public void DestroyBlock() {
        StartCoroutine(AnimationDestroy());
        isBlock = false;
    }
    private IEnumerator AnimationDestroy() {
        yield return new WaitForSeconds(0.2f);
        foreach (Sprite s in anim)
        {
            image.sprite = s;
            yield return new WaitForSeconds(0.2f);
        }
        image.DOColor(new Color(1,1,1,0),1.0f);
        yield return new WaitForSeconds(1);
        image.enabled = false;
    }

}
