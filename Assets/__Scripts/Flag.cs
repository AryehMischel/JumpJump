using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameManager GameManager;
    //public PlayerController PlayerController;

    void Awake()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        //  PlayerController.Win();

        if (coll.gameObject.layer == 8 || coll.gameObject.layer == 9)
        {
          GameManager.Win();
      }
      else
      {
          Debug.Log(coll.gameObject.name);
      }

    }
    
}