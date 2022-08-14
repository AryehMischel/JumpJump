using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Prefab;
    public float interval;
    public float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interval)
        {
            Instantiate(Prefab, this.transform);
            time = 0;
        }

    }
}
