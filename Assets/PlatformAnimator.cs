using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] Shapes;
    void Start()
    {
        /*Shapes[0].DOMoveY(10f, 3f).OnComplete(() =>
        {
            Shapes[1].DOMoveY(10, 3f).OnComplete(() =>
            {
                Debug.Log("finished");
            });
        });*/

        var sequence = DOTween.Sequence();
        
        foreach(var shape in Shapes)
        {
            sequence.Append(shape.DOMoveY(10f, 3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
