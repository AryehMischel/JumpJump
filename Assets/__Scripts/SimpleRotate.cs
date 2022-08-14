using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float degrees;
    void Start()
    {
        this.transform.DORotate(new Vector3(0, 0, degrees), 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
