using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject FastParticles;
    public GameObject SlowParticles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FastParticles.SetActive(false);
        SlowParticles.SetActive(true);
    }
}
