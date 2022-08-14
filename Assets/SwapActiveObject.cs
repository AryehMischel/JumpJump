
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapActiveObject : MonoBehaviour
{
    public GameObject ObjectA;
    public GameObject ObjectB;

  

    public void swap()
    {
        ObjectA.SetActive(false);
        ObjectB.SetActive(true);
    }
}
