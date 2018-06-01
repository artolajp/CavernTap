using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ButtonPower : MonoBehaviour {

    private Button button;
    [SerializeField]
    private PowerController.Power power;
    [SerializeField]
    private Image image=null;
    [SerializeField]
    private Text cantPowerText=null;
    private float cooldown = 90;
    void Start() {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ActivePower);
        image.fillAmount = 0;
        Check();
        SetCantPowerText();
    }

    private void ActivePower() {
        if (Time.timeSinceLevelLoad > 5)
        {
            SoundController.Instance.PlaySound(SoundController.Sounds.POWER);
            if (PowerController.Instance.ActivePower(power))
            {
                button.interactable = false;
                StartCoroutine(WaitCooldown());
            }
            else
            {
                button.interactable = false;
            }
        }
        SetCantPowerText();

    }

    private IEnumerator WaitCooldown() {
        image.DOFillAmount(1,cooldown);
        yield return new WaitForSeconds(cooldown);
        image.fillAmount = 0;
        button.interactable = true;
    }
    private void Check() {
        button.interactable = PowerController.Instance.HasPower(power);
    }

    private void SetCantPowerText() {
        if (PowerController.Instance.HasPower(power))
            cantPowerText.text = PowerController.Instance.CantPower(power).ToString();
        else {
            cantPowerText.gameObject.SetActive(false);
        }
    }
}
