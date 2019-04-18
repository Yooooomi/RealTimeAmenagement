using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Events;

public class GestureHandler : MonoBehaviour, IFocusable, IInputClickHandler, IInputHandler
{

    private bool isActive = false;
    private GestureRecognizer recognizer;

    private void Start()
    {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += GestureRecognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    private void Update()
    {
        
    }

    public void OnFocusEnter()
    {
        Debug.Log("FOCUS");
    }

    public void OnFocusExit()
    {
        Debug.Log("NOT FOCUS");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        transform.localScale *= 1.05f;
    }

    void OnAirTapped()
    {

        isActive = !isActive;
    }

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Debug.DrawLine(headRay.origin, headRay.direction * 1000, Color.green);
        OnAirTapped();
    }

    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("PRIS");
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("LACHE");
    }
}
 