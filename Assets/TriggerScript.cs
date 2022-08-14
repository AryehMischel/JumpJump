using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    public List<UnityEvent> actions;
    public List<UnityEvent> ExitActions;


    private void OnTriggerEnter(Collider other)
    {
        foreach (var e in actions)
        {
            e.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var e in ExitActions)
        {
            e.Invoke();
        }
    }
}
