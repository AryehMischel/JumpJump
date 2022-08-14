using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplasyInputKeys : MonoBehaviour
{

    public Image UpArrow;
    public Image DownArrow;
    public Image LeftArrow;
    public Image RightArrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckLeft();
        CheckUp();
        CheckDown();
        CheckRight();

    }

    void CheckUp()
    {
        if (Input.GetButtonDown("Jump"))
        {
            UpArrow.color = Color.red;
                
        } else if (Input.GetButtonUp("Jump"))
        {
            UpArrow.color = Color.white;
        }
    }

    void CheckDown()
    {
        if (Input.GetButtonDown("Down"))
        {
            DownArrow.color = Color.red;
            
        } else if (Input.GetButtonUp("Down"))
        {
            DownArrow.color = Color.white;
        }
    }

    void CheckLeft()
    {  
        if (Input.GetButtonDown("Left"))
        {
            LeftArrow.color = Color.red;
            
        } else if (Input.GetButtonUp("Left"))
        {
            LeftArrow.color = Color.white;
        }
        
    }

    void CheckRight()
    {
        if (Input.GetButtonDown("Right"))
        {
            RightArrow.color = Color.red;
        } else if (Input.GetButtonUp("Right"))
        {
            RightArrow.color = Color.white;
        }
        
    }
}
