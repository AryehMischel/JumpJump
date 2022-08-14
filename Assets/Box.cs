using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public Material Mat1;
    public Material Mat2;

    
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material = Mat2;

    }

    
    [ContextMenu("switchmat")]
    public void switchmat()
    {
       // var mat  = this.gameObject.GetComponent<Renderer>().materials[1];
        //this.gameObject.GetComponent<Renderer>().material = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material = Mat1;

    }
}
