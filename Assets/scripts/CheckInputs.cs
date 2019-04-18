using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class CheckInputs : MonoBehaviour, IInputHandler
{
    public float timeToLongPress = 1.0f;
    private float timeClicked = 0.0f;

    public virtual void OnSimpleClick() { }
    public virtual void OnLongClick() { }

    public void OnInputDown(InputEventData eventData)
    {
        timeClicked = Time.time;
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (Time.time - timeClicked < timeToLongPress)
        {
            Debug.Log("Short");
            OnSimpleClick();
        }
        else
        {
            Debug.Log("Long");
            OnLongClick();
        }
    }

    
}
