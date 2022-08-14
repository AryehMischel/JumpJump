using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapColors : MonoBehaviour
{
    
    [ContextMenu("swap")]
    public void Swap()
    {
        var mat = this.transform.gameObject.GetComponent<Renderer>().materials[1];
        this.transform.gameObject.GetComponent<Renderer>().material = mat;
    }

    [ContextMenu("unswap")]
    public void unswap()
    {
        var mat = this.transform.gameObject.GetComponent<Renderer>().materials[0];
        this.transform.gameObject.GetComponent<Renderer>().material = mat;

    }
}
