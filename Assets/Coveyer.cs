using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coveyer : MonoBehaviour
{
    // Start is called before the first frame update

    public float Force;
    public List<GameObject> Objects; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
    }
    
    void OnTriggerExit(Collider collision)
    {
        collision.gameObject.GetComponent<PlayerTutorial>().Slowing = true;
        

    }
    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<PlayerTutorial>().Slowing = false;
        collision.gameObject.GetComponent<PlayerTutorial>().AddForceXAxis = Force;
        collision.gameObject.GetComponent<PlayerTutorial>().divisor = 20;

    }
    
    
}
