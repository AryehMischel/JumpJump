using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaverScript : MonoBehaviour
{
    public Image RedLever;
    public Image GreenLever;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("switch Imatges");
        RedLever.enabled = true;
        GreenLever.enabled = false;
    }
}
