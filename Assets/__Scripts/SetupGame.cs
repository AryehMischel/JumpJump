using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerData PlayerData;
    public PhysicMaterial PlayerMaterial; 
    void Start()
    {
        
    }

    private void Awake()
    {
        PlayerData.Jump = 50F;
        PlayerMaterial.bounciness = 0.4f;
        PlayerData.GhostMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
