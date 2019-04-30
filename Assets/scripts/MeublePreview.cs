using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class MeublePreview : MonoBehaviour {

    public Image completion;
    private CheckInputs checkInputs;

    private void Start()
    {
        checkInputs = GetComponent<CheckInputs>();
    }

    public void OnHoldFrame(float time)
    {
        completion.fillAmount = time / checkInputs.holdTime;
    }

    public void OnHold()
    {
        Debug.Log("J'ai mon canapé");
        // TODO Instantiate object
    }

    public void OnUp()
    {
        completion.fillAmount = 0;
    }

}
