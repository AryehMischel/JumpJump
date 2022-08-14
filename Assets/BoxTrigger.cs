using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var Box = other.gameObject;
  
        if (other.gameObject.tag == "Falling")
        {

            Box.GetComponent<Renderer>().material = Box.GetComponent<Renderer>().materials[1];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var Box = other.gameObject;

        if (other.gameObject.tag == "Falling")
        {
            Box.GetComponent<Renderer>().material = Box.GetComponent<Renderer>().materials[0];
        }
    }
}
