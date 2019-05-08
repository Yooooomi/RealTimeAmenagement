using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class MeublePreview : MonoBehaviour {

    public GameObject meublePrefab;

    public float spawnOffset;
    public Image completion;
    private CheckInputs checkInputs;
    private bool holding;
    private Camera cam;

    private void Start()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        checkInputs = GetComponent<CheckInputs>();
    }

    public void OnDown()
    {
        holding = true;
    }

    public void OnHoldFrame(InputEventData eventData, float time)
    {
        if (holding)
        {
            completion.fillAmount = time / checkInputs.holdTime;
        }
    }

    public void OnHold(InputEventData eventData)
    {
        holding = false;
        completion.fillAmount = 0;
        Debug.Log("J'ai mon canapé");
        GameObject objectInstantiated = GameObject.Instantiate(meublePrefab, transform.position - transform.forward * spawnOffset, Quaternion.identity);
        Vector3 size = objectInstantiated.GetComponentInChildren<Renderer>().bounds.size;
        objectInstantiated.transform.position -= new Vector3(0, size.y / 2, 0);
        HandDraggable hd = objectInstantiated.GetComponent<HandDraggable>();
        hd.HostTransform = objectInstantiated.transform;
        hd.OnInputDown(eventData);
    }

    public void OnUp()
    {
        holding = false;
        completion.fillAmount = 0;
    }

}
