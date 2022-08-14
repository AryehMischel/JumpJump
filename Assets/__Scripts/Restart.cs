using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("respawn")]
    public void RestartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    
    [ContextMenu("respawn")]
    public void Exit()
    {
        Application.Quit();
    }
    
    
}
