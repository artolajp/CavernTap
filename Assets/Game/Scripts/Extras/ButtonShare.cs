using UnityEngine;
using UnityEngine.UI;

public class ButtonShare : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Share);
    }

    private void Share() {
        ShareAndRate.Instance.OnAndroidTextSharingClick();

    }
}
