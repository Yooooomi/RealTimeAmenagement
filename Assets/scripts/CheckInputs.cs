using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Events;

[System.Serializable]
public class OnHoldFrame : UnityEvent<float> { }

public class CheckInputs : MonoBehaviour, IInputHandler
{
    public float holdTime = 1.0f;
    private float timeClicked = 0.0f;
    private bool down = false;
    private bool signaledHold = false;

    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onHold = new UnityEvent();
    public OnHoldFrame onHoldFrame = new OnHoldFrame();
    public UnityEvent onDown = new UnityEvent();
    public UnityEvent onUp = new UnityEvent();

    public void OnInputDown(InputEventData eventData)
    {
        down = true;
        onDown.Invoke();
        timeClicked = Time.time;
    }

    private void Update()
    {
        if (down)
        {
            onHoldFrame.Invoke(Time.time - timeClicked);
            if (!signaledHold && Time.time - timeClicked > holdTime)
            {
                signaledHold = true;
                onHold.Invoke();
            }
        }
    }

    public void OnInputUp(InputEventData eventData)
    {
        signaledHold = false;
        down = false;
        if (Time.time - timeClicked < holdTime)
        {
            onClick.Invoke();
        }
        onUp.Invoke();
    }
}
