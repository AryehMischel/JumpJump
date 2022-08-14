using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;
public  class PlayerTutorial : MonoBehaviour
{

    public PlayerData PlayerData;
    public float MovementSpeed;
    public float JumpStrength;
    private Vector3 JumpForce;
    public float DiveStrength;
    public float SlowDownSpeed;
    public float FlyingSpeed;
    public Vector3 SpawnPosition;
    public bool Slowing;    
    private bool controlled;
    private bool JumpCanceled;
    private float movement;
    public float slowdownspeed;
    private Rigidbody rb;
    private PlayerControls controls;
    public bool isGrounded;
    private bool diving;
    private bool firstJump = true;
    private bool secondJump = true;
    private bool intersecting = false;
    private bool ghosting;
    public float AddForceXAxis;
    public float divisor;
    public bool windoff = false;

    private Coroutine SlowRoutine;
    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        controls = new PlayerControls();
        controls.Player.Movement.performed += ctx => movement = ctx.ReadValue<float>();
        controls.Player.Movement.performed += _ => controlled = true;
        controls.Player.Movement.canceled += _ => ReleasePlayer();
        controls.Player.Jump.started += _ => jump();
        controls.Player.Jump.canceled += _ => JumpCanceled = true; 
        controls.Player.Dive.started  += _ => diving = true;
        controls.Player.Dive.canceled += _ => diving = false;
        JumpStrength = PlayerData.Jump;
        JumpForce = new Vector3(0,  JumpStrength);

        //bad logic but proof of concept

        controls.Player.Movement.performed += _ => controlled = true;

        if (PlayerData.GhostMode == true)
        {
            controls.Player.Boost.started += _ => GhostMode(); 
            controls.Player.Boost.canceled += _ => NormalMode();
            if (SceneManager.GetActiveScene().name == "scene1")
            {
                GameObject.FindGameObjectWithTag("GhostUI").GetComponent<Canvas>().enabled = true;
            }
        }
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        intersecting = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        intersecting = false;
        if(ghosting is false & intersecting is false)
            this.transform.gameObject.layer = 8;
    }
    

    public void GhostMode()
    {
        this.transform.gameObject.layer = 9;
        ghosting = true;
    }

    public void NormalMode()
    {
        ghosting = false;
        if(ghosting is false & intersecting is false)
            this.transform.gameObject.layer = 8;

    }
    
    
    private void ReleasePlayer()
    {
        controlled = false;
        movement = 0;
    }
    

    private void FixedUpdate()
    {

        if (movement < 0 & !intersecting & windoff ) 
        {
            Slowing = false;
            AddForceXAxis = 0;
        }
        if (Slowing)
        {
          
            if (Mathf.Abs(AddForceXAxis) > 0.2)
            {
                AddForceXAxis = Mathf.Lerp(AddForceXAxis, 0, Time.deltaTime + (0.005f * rb.velocity.x));
            }
            else
            {
                AddForceXAxis = 0;
                Slowing = false;
            }
        }
        
        
        if(JumpCanceled & rb.velocity.y > 0)
        {
            Debug.Log("slow down jump");
            rb.velocity = new Vector3(rb.velocity.x + (AddForceXAxis / divisor), rb.velocity.y * 0.80f);
            JumpCanceled = false;
        }
        
        if (controlled)
        {
            if (!isGrounded)
            {
                rb.velocity = new Vector3(movement * FlyingSpeed + AddForceXAxis, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector3((movement * MovementSpeed) + AddForceXAxis, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x + (AddForceXAxis / divisor), rb.velocity.y, rb.velocity.z);
        }

        if (diving)
        {
            dive();
        }

    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
    
    private void jump()
    {
        
        if ( isGrounded | secondJump)
        { 
           rb.AddForce(JumpForce, ForceMode.Impulse);
           if (firstJump)
            {
                firstJump = false; 
            }
            else
            {
                secondJump = false;
            }
        }
    }

    private void dive()
    {
        Debug.Log("diving");
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0);
        }
        Vector3 force = new Vector3(0, -3, 0);
        rb.AddForce(force, ForceMode.Impulse);
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        isGrounded = true;
        firstJump = true;
        secondJump = true;
        
        if (collision.gameObject.CompareTag("sticky"))
        {
            rb.velocity = Vector3.zero;
            JumpStrength = JumpStrength * 3;
            this.gameObject.GetComponent<Rigidbody>().drag = 20;
        } 
        else if (collision.gameObject.CompareTag("lava"))
        {
            respawn();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("sticky"))
        {
            JumpStrength /= 3;
            this.gameObject.GetComponent<Rigidbody>().drag = 0.5f;
            isGrounded = false;
        }

    }
    [ContextMenu("RespawnPlayer")]
    private void respawn()
    {
        StartCoroutine(respawnco());
 
    }

    IEnumerator respawnco()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponentInChildren<ParticleSystem>().Play();
        rb.isKinematic = true;
        var distance = Mathf.Abs(this.transform.position.x - SpawnPosition.x);
        yield return new WaitForSeconds(1f);
        
        this.transform.DOMove(SpawnPosition, 0.3f + (0.03f * distance) ).OnComplete(() =>
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            rb.isKinematic = false;

        });
    }
}
