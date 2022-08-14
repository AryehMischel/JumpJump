using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;
public  class PlayerController : MonoBehaviour
{


    public PlayerData PlayerData; //Scriptable Object 
    public Renderer ErrorUI; 
        
    private GameObject Player;
    private Rigidbody PlayerRB;
    private TrailRenderer PlayerTrail;
    private ParticleSystem ParticleSystem;

    private Vector3 jumpForce = new Vector3(0, 0, 0);
    
    
    private bool firstJump = true;
    private bool jumpCharged = true;


    public float resetdur;
    public Ease endease;


    private Vector3 respawnPosition;

    public GameObject Flag;
    
    
    
    private void Awake()
    {
        Player = this.gameObject;
        //jumpForce = PlayerData.Jump;
        respawnPosition = Flag.transform.position;
        PlayerRB = this.gameObject.GetComponent<Rigidbody>();
        PlayerTrail = this.gameObject.GetComponent<TrailRenderer>();
        ParticleSystem = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }
    
    private void Update()
    {
        CheckUp();
    }
    void FixedUpdate()
    {
        CheckLeft();
        CheckRight();
        CheckDown();
        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (Player.transform.position.z != 0)
        {
            ErrorUI.enabled = true;
        } 
    }



    [ContextMenu("Restart")]
    public void Restart()
    { 
        Player.transform.position = respawnPosition;
    }


    void CheckLeft()
    {
        if (Input.GetButton("Left"))
        {
            GoLeft();
        }
    }
    
    void CheckRight()
    {
        if (Input.GetButton("Right"))
        {
            GoRight();
        }
    }

    void CheckUp()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCharged)
            { 
                Jump();
            }
        }
    }

    void CheckDown()
    {
        if (Input.GetButton("Down"))
        {
            GoDown();
        }
    }
    

    void GoRight()
    {
      Vector3 force = new Vector3(0.27f, 0, 0);
      PlayerRB.AddForce(force, ForceMode.Impulse);
    }
    void GoLeft()
    {
        Vector3 force = new Vector3(-0.27f, 0, 0);
        PlayerRB.AddForce(force, ForceMode.Impulse);
    }

    public void GoDown()
    {
        Vector3 force = new Vector3(0, -3, 0);
        PlayerRB.AddForce(force, ForceMode.Impulse);
    }
    
    private void Jump()
    {
        if (firstJump)
        {
            firstJump = false;
        }
        else
        {
            jumpCharged = false;
        }
        
        PlayerRB.AddForce(jumpForce, ForceMode.Impulse);
    }
    
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag == "lava")
        {
            StartCoroutine(Irespawn());
        }
        else
        {
            RechargeJump();
        }
    }
    
    void RechargeJump()
    {
        firstJump = true;
        jumpCharged = true;
    }
    
    IEnumerator Irespawn()
    {
        
        PlayerTrail.enabled = false;
        yield return new WaitForSeconds(0.03f);
        PlayerRB.velocity = new Vector3(0, 0, 0);
        //Player.transform.position = respawnPosition;
        Player.transform.position = new Vector3(0, 0, 0);
        PlayerTrail.enabled = true;
    }


    [ContextMenu("PlayParticles")]
    void PlayParticles()
    {
        ParticleSystem.Play();
    }
    
    public void Win()
    {
        PlayerRB.isKinematic = true;
        Player.transform.DOMove(respawnPosition, resetdur).SetEase(endease);
        Player.transform.position = respawnPosition;
        
        PlayerTrail.enabled = false;
        //Player.transform.DOMove(new Vector3(-2.27f, 34.52f, 0), 1f).SetEase(Ease.Linear);
        Player.GetComponent<MeshRenderer>().enabled = false;
        Player.GetComponent<SphereCollider>().enabled = false;
       // PlayParticles();
    } 
    
}
