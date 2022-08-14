using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;
public  class Player2 : MonoBehaviour
//player class
{

    public PlayerData PlayerData;
    public float MovementSpeed;
    public float JumpStrength;
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

    private Coroutine SlowRoutine;
    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        controls = new PlayerControls();
        controls.Player.Movement.performed += ctx => movement = ctx.ReadValue<float>();
        controls.Player.Movement.performed += _ => controlled = true;
        controls.Player.Movement.canceled += _ => StopObject();
        controls.Player.Jump.started += _ => jump();
        controls.Player.Jump.canceled += _ => JumpCanceled = true; 
        controls.Player.Dive.started  += _ => diving = true;
        controls.Player.Dive.canceled += _ => diving = false;
        
  
        
        
        JumpStrength = PlayerData.Jump;
        //bad logic but proof of concept

        controls.Player.Movement.performed += _ => controlled = true;

        if (PlayerData.GhostMode == true)
        {
            controls.Player.Boost.started += _ => GhostMode(); 
            controls.Player.Boost.canceled += _ => OffLeap();
            if (SceneManager.GetActiveScene().name == "scene1")
            {
                GameObject.FindGameObjectWithTag("GhostUI").GetComponent<Canvas>().enabled = true;
            }
        }

        
       // controls.Player.Jump.performed += _ => controlled = true;

    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered!");
        intersecting = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        intersecting = false;
        Debug.Log("exited trigger");
        if(ghosting is false & intersecting is false)
            this.transform.gameObject.layer = 8;
            Debug.Log("offcollider");
    }
    
    
    [ContextMenu("Leap Frog")]
    public void LeapFrog()
    {
      //  controls.Player.Boost.started += _ => Debug.Log("boosting");
  
      PlayerData.GhostMode = true;

    }

    public void GhostMode()
    {
        this.transform.gameObject.layer = 9;
        ghosting = true;
    }

    public void OffLeap()
    {
        ghosting = false;
        if(ghosting is false & intersecting is false)
            this.transform.gameObject.layer = 8;

    }
    private void StopObject()
    {
        //SlowRoutine =  StartCoroutine(SlowDown());
        ///movement = 0;
        controlled = false;
        movement = 0;
    }

  

    private void StopStoppingObject()
    { 
       //Debug.Log("stopping...");
       // StopCoroutine(SlowRoutine);
    }

    /*IEnumerator SlowDown()
    {
        float timeToStart = Time.time;
        float Speed = SlowDownSpeed;
        if (!isGrounded)
        {
            Speed /= 2;
        }
        float initm = movement;
        
        while(Mathf.Abs(movement) > 0.0f)
        {
            movement = Mathf.Lerp(initm, 0, (Time.time - timeToStart ) * Speed ); 
         
            yield return null;
        }

        controlled = false;

    }*/

    private void FixedUpdate()
    {

        if (movement < 0 & !intersecting) //and not intersecting with 
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
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.80f);
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
            
            /*
            if (rb.velocity.x > 0.1 || AddForceXAxis > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x + (AddForceXAxis / divisor), rb.velocity.y, rb.velocity.z);

            }*/
        }

        if (diving)
        {
            dive();
        }


     
     



    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();


[ContextMenu("showv")]
    private void checkv()
    {
        Debug.Log(rb.velocity.x);
    }

    private void jump()
    {
        
        if ( isGrounded | secondJump)
        {
            // rb.velocity += new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            // rb.AddForce( new Vector3(x * 30, 0 , 0), ForceMode.Impulse);
            // rb.velocity = new Vector2(rb.velocity.x, JumpStrength);
            // var x = rb.velocity.x;
            // rb.AddForce(force, ForceMode.Impulse);
            //rb.AddForce(force, ForceMode.Impulse);
           Vector3 force = new Vector3(0,  JumpStrength);
           rb.AddForce(force, ForceMode.Impulse);
           Debug.Log(rb.velocity);
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
        
        /*if (collision.gameObject.CompareTag("floor"))
        {
           SetStateGround();
        }*/

        if (collision.gameObject.CompareTag("sticky"))
        {
            isGrounded = true;
            firstJump = true;
            secondJump = true;
            rb.velocity = Vector3.zero;
            JumpStrength = JumpStrength * 3;
            this.gameObject.GetComponent<Rigidbody>().drag = 20;
        } else if (collision.gameObject.CompareTag("lava"))
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

        /*if (collision.gameObject.CompareTag("floor"))
        {
        }*/
        
        if (collision.gameObject.CompareTag("sticky"))
        {
            JumpStrength /= 3;
            this.gameObject.GetComponent<Rigidbody>().drag = 0.5f;
            isGrounded = false;
        }

    }


    private void SetStateAir()
    {
        this.transform.gameObject.GetComponent<Rigidbody>().drag = 0.5f;
        MovementSpeed = 7;
        JumpStrength = JumpStrength / 3;
    }

    private void SetStateGround()
    {
        this.transform.gameObject.GetComponent<Rigidbody>().drag = 6f;
        MovementSpeed = 14;
        JumpStrength = JumpStrength * 3;
    }

    [ContextMenu("RespawnPlayer")]
    private void respawn()
    {
        StartCoroutine(respawnco());
        /*this.GetComponent<MeshRenderer>().enabled = false;
        
        this.GetComponent<ParticleSystem>().Play();
        this.transform.DOMove(SpawnPosition, 0.3f).OnComplete(() =>
        {
            this.GetComponent<MeshRenderer>().enabled = true;            
        });*/
    }

    IEnumerator respawnco()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponentInChildren<ParticleSystem>().Play();
        rb.isKinematic = true;
        
        yield return new WaitForSeconds(1f);
        
        
        this.transform.DOMove(SpawnPosition, 0.3f).OnComplete(() =>
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            rb.isKinematic = false;

        });
    }
}
