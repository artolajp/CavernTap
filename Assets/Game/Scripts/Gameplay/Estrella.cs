using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Estrella : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    private float speed = 80, maxHorizontalSpeed = 100, timeToDestroy=10.0f;
    [SerializeField]
    private Text text=null;
    private float actualSpeed;
    private float speedMultiplicator;
    IEnumerator Start() {
        CalcSpeed();
        StartCoroutine(ChangeDirection());
        text.text = "";
        yield return new WaitForSeconds(timeToDestroy);
        DestoyThis();
    }
    void Update() {
        transform.position +=(Vector3.up*speed*speedMultiplicator+Vector3.left*actualSpeed) * Time.deltaTime;
    }

    private IEnumerator ChangeDirection() {
        actualSpeed = Random.Range(-maxHorizontalSpeed, maxHorizontalSpeed);
        yield return new WaitForSeconds(1f);
        yield return ChangeDirection();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        GetComponent<Image>().enabled = false;
        SoundController.Instance.PlaySound(SoundController.Sounds.STAR);
        text.text = ScoreController.Instance.AddWithoutRacha(1) + "pts";
        text.DOColor(new Color(255,255,255,0),0.6f);
        
    }
    private void DestoyThis() {
        Destroy(gameObject);
    }
    private void CalcSpeed() {
        speedMultiplicator = Screen.height/1600.0f;
    }
}
