using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFeedback : MonoBehaviour {

    private List<TouchAnimation> touches;
    [SerializeField]
    private TouchAnimation prefab;

    void Start() {
        touches = new List<TouchAnimation>();
        for (int i = 0; i < 5; ++i) {
            InstanceNewTouch();
        }
    }
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began) {
                    GetFreeTouch().Play(t.position) ;
                }
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
           GetFreeTouch().Play(Input.mousePosition); 
        }
#endif
    }

    private TouchAnimation GetFreeTouch() {

        for(int i=0; i<touches.Count;++i) {
            if (!touches[i].IsAnim) return touches[i];
        }

        return InstanceNewTouch();
    }

    private TouchAnimation InstanceNewTouch() {
        TouchAnimation ta = Instantiate(prefab,transform,false);
        ta.gameObject.SetActive(false);
        touches.Add(ta);
        return ta;
    }

}
