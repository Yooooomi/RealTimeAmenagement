using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.UI;

public class Shelf : MonoBehaviour {

    private bool showing;

    [SerializeField]
    private Animator animator;

    public void OnClick()
    {
        Debug.Log("On Click!");
        showing = !showing;
        animator.SetBool("isShowing", showing);
    }

    public void OnHold()
    {
        Debug.Log("On Hold!");
    }

}
