using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class DragOnHold : MonoBehaviour {

    public HandDraggable handDraggable;
    public CheckInputs inputManager;

    private void Start()
    {
        inputManager.onPhysicUp.AddListener(this.Up);
        inputManager.onHold.AddListener(this.Hold);
        handDraggable.SetDragging(false);
    }

    private void Hold(InputEventData data)
    {
        handDraggable.SetDragging(true);
        handDraggable.OnInputDown(data);
    }

    private void Up(InputEventData data)
    {
        handDraggable.SetDragging(false);
    }
}
