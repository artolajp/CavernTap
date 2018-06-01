using UnityEngine;
using System.Collections;

public class EstrellaController : MonoBehaviour {
    
    public Transform left = null, right = null;

    void Start() {
        StartCoroutine(EstrellaX2());
        StartCoroutine(EstrellaX3());
        StartCoroutine(EstrellaX4());

    }

    private void Fire(bool fireLife) {
        
            Vector3 pos = new Vector3(Random.Range(left.position.x, right.position.x), left.position.y, 0);
        if (!fireLife)
        {
            Instantiate(SkinCollection.Instance.ActualSkin.PrefabStar, pos, transform.rotation, transform);
        }
        else { 
            if (Random.Range(1, 1000) < 990)
            {
                Instantiate(SkinCollection.Instance.ActualSkin.PrefabStar, pos, transform.rotation, transform);
            }
            else
            {
                Instantiate(SkinCollection.Instance.ActualSkin.PrefabHeart, pos, transform.rotation, transform);
            }
        }
    }
    private IEnumerator EstrellaX2() {
        yield return new WaitForSeconds(2);
        if(ScoreController.Instance.Multiplicador==2) Fire(false);
        yield return EstrellaX2();
    }
    private IEnumerator EstrellaX3()
    {
        yield return new WaitForSeconds(1);
        if (ScoreController.Instance.Multiplicador == 3) Fire(false);
        yield return EstrellaX3();
    }

    private IEnumerator EstrellaX4()
    {
        yield return new WaitForSeconds(0.7f);
        if (ScoreController.Instance.Multiplicador == 4) Fire(true);
        yield return EstrellaX4();
    }

}
