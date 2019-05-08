using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.UI;

public class Shelf : MonoBehaviour {

    public Transform previewContainer;
    public GameObject meubleContainer;
    public GameObject previewPrefab;
    public List<GameObject> availableMeubles;
    public int meublesPerLine;
    public float spacing;
    private bool showing;

    [SerializeField]
    private Animator animator;

    private void Start()
    {

        for (int i = 0; i < availableMeubles.Count; i++)
        {
            int currentLine = i / meublesPerLine;
            int ndxInLine = i % meublesPerLine;
            GameObject instantiated = Instantiate(previewPrefab, previewContainer);
            MeublePreview mp = instantiated.GetComponent<MeublePreview>();
            mp.Init(meubleContainer, availableMeubles[i]);
            instantiated.transform.localPosition = new Vector3(spacing * ndxInLine - (spacing * (meublesPerLine - 1)) / 2, currentLine * spacing, 0);
        }
    }

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
