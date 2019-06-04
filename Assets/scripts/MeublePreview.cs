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

    public GameObject testPrefab;

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

        float ratio = 1 / (Mathf.Max(coll.bounds.size.x, coll.bounds.size.y));
        preview.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * ratio;

        preview.transform.localPosition = Vector3.zero - transform.forward;
        Vector3 offsetFromCenter = preview.transform.position - coll.bounds.center;
//        Debug.DrawLine(Vector3.zero, coll.bounds.center, Color.red, 1000.0f);
//        Debug.DrawLine(Vector3.zero, preview.transform.position, Color.green, 1000.0f);
//        Debug.Log("---");
//        Debug.Log(meublePrefab.name);
//        Debug.Log(preview.transform.position.x);
//        Debug.Log(coll.bounds.center.x);
//        Debug.Log(offsetFromCenter.x);
//        Debug.Log("---");
//        Instantiate(testPrefab, preview.transform.TransformPoint(offsetFromCenter), Quaternion.identity);
        preview.transform.position += offsetFromCenter;

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

        objectInstantiated.transform.position += offsetFromCenter;
    }

    public void OnUp()
    {
        holding = false;
        completion.fillAmount = 0;
    }

}
