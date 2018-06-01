using UnityEngine;
using System.Collections;

public class ResetPlayerPrefs : MonoBehaviour { 
    [SerializeField]
    private int cantTickets = 0;

	void Start () {
        PlayerPrefs.DeleteAll();
        ScoreController.Instance.CantTickets = cantTickets;

    }

}
