using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CuentaRegresiva : MonoBehaviour {
    [SerializeField]
    private Text text;
    [SerializeField]
    private int time=3;
	// Use this for initialization
	IEnumerator Start () {
        while (time > 0)
        {
            text.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }

        TutorialPanelGameplay.Instance.OpenTapMonster();
        gameObject.SetActive(false);
	}
	
}
