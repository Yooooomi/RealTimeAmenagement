using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Events;

[System.Serializable]
public class OnHoldFrame : UnityEvent<InputEventData, float> { }
[System.Serializable]
public class OnBasicInput : UnityEvent<InputEventData> { }

public class CheckInputs : MonoBehaviour, IInputHandler, IFocusable
{
    public float holdTime = 1.0f;
    private float timeClicked = 0.0f;
    private bool down = false;
    private bool signaledHold = false;

    public OnBasicInput onClick = new OnBasicInput();
    public OnBasicInput onHold = new OnBasicInput();
    public OnHoldFrame onHoldFrame = new OnHoldFrame();
    public OnBasicInput onDown = new OnBasicInput();
    public OnBasicInput onUp = new OnBasicInput();
    public OnBasicInput onPhysicUp = new OnBasicInput();

    private InputEventData lastEventData;

    public void OnInputDown(InputEventData eventData)
    {
        lastEventData = eventData;
        down = true;
        onDown.Invoke(eventData);
        timeClicked = Time.time;
    }

    private void Update()
    {
        if (down)
        {
            onHoldFrame.Invoke(lastEventData, Time.time - timeClicked);
            if (!signaledHold && Time.time - timeClicked > holdTime)
            {
                signaledHold = true;
                onHold.Invoke(lastEventData);
            }
        }
    }

    public void up(InputEventData eventData)
    {
        signaledHold = false;
        down = false;
        if (Time.time - timeClicked < holdTime)
        {
            onClick.Invoke(eventData);
        }
        onUp.Invoke(eventData);
    }

    public void OnInputUp(InputEventData eventData)
    {
        onPhysicUp.Invoke(eventData);
        if (down) this.up(eventData);
    }

    public void OnFocusEnter()
    {
    }

    public void OnFocusExit()
    {
        if (down) this.up(lastEventData);
    }
}
