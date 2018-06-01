using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonAttack : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    private int cocodriloTarget;
    [SerializeField]
    private string tecla;

    public void OnPointerDown(PointerEventData eventData)
    {
        Action();   
    }

    void Update ()
	{
		#if UNITY_EDITOR
		if (Input.GetKeyDown (tecla)) {
			Action ();
		}
		#endif
	}


    private void Action() {
        CocodrilloController.Instance.TouchCocodrilo(cocodriloTarget);

    }
}
