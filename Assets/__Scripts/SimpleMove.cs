using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class SimpleMove : MonoBehaviour
{

    public Vector3 Target;

    public Ease ease;

    public float Duration;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOMove(Target, Duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
