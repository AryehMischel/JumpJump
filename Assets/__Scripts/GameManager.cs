using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas WinCanvas;
    public Canvas UI;
    public PlayerData PlayerData;
    public PhysicMaterial PhysicMaterial;
//    private PlayerController PlayerController;
    public Vector3 respawnPosition;

    public GameObject Player;
    public Animator fade;
    private void Awake()
    {
  //      PlayerController = GameObject.FindObjectOfType<PlayerController>();
  
    }

    /*
    public void Win()
    {
        //PlayerController.Win();
        //DisplayUI();
        Win();
    }
    */
    
    public void Win()
    {
        Player.GetComponent<Rigidbody>().isKinematic = true;
        Player.transform.DOMove(respawnPosition, 0.72f).SetEase(Ease.InFlash);
        Player.transform.position = respawnPosition;
        Player.GetComponent<TrailRenderer>().enabled = false;
        Player.GetComponent<MeshRenderer>().enabled = false;
        Player.GetComponent<SphereCollider>().enabled = false;
        if ( SceneManager.GetActiveScene().name == "scene3")
        {
            StartCoroutine(EndGame());
        }
        else
        {
            UI.enabled = true;
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.7f);
        fade.enabled = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("WinScene");
    }

    [ContextMenu("ShowUI")]
    public void DisplayUI()
    {
        UI.enabled = true;

    }
    
    public void SuperJump()
    {
        PlayerData.Jump = PlayerData.Jump + 10;
        EndLevel();
    }

    public void GhostMode()
    {
        PlayerData.GhostMode = true;
        EndLevel();
    }

    public void ExtraBounce()
    {

        PhysicMaterial.bounciness += 0.2f;
        EndLevel();
    }


    void EndLevel()
    {
        StartCoroutine(SwitchScene());
    }
    
    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(0.7f);
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "scene1")
        {
            SceneManager.LoadScene("scene2");
        }
        else if(currentScene == "scene2")
        {
            SceneManager.LoadScene("scene3");
        }  
        else
        {
            SceneManager.LoadScene("scene1");
        }
      

    }
  
}
