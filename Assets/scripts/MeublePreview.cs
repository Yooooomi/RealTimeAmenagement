using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class MeublePreview : MonoBehaviour {

    public GameObject meubleContainer;
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

    public void Init(GameObject meubleContainer, GameObject meublePrefab)
    {
        this.meubleContainer = meubleContainer;
        this.meublePrefab = meublePrefab;
        GameObject preview = Instantiate(meublePrefab, transform);
        Collider coll = preview.GetComponent<Collider>();
        Vector3 offsetFromCenter = preview.transform.position - coll.bounds.center;
        preview.transform.localPosition = -transform.forward + offsetFromCenter;
        preview.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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
        GameObject objectInstantiated = GameObject.Instantiate(meubleContainer, transform.position - transform.forward * spawnOffset, Quaternion.identity);
        GameObject meubleInstantiated = Instantiate(meublePrefab, objectInstantiated.transform);
        meubleInstantiated.transform.localPosition = Vector3.zero;
        Vector3 size = meubleInstantiated.GetComponentInChildren<Renderer>().bounds.size;
        objectInstantiated.transform.position -= new Vector3(0, size.y / 2, 0);

        Collider coll = meubleInstantiated.GetComponent<Collider>();
        Vector3 offsetFromCenter = meubleInstantiated.transform.position - coll.bounds.center;

        objectInstantiated.transform.localPosition += offsetFromCenter;
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
