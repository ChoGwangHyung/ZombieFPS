using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : MonoBehaviour
{
    [SerializeField] private float yPosDistance = 0.0f;

    [SerializeField] private GameObject colUi = null;
    [SerializeField] private GameObject unColUi = null;
    [SerializeField] private GameObject rootObject = null;

    private Vector3 originPos = new Vector3(205.0f, 0.0f, 0.0f);

    void Start()
    {
        colUi.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (colUi.activeSelf == true || rootObject.GetComponent<CheckObjectVisible>().isVisible == false ) return;
        Vector3 screenPos = MainCamera.instance.GetComponent<Camera>().WorldToScreenPoint(rootObject.transform.position + new Vector3(0.0f, yPosDistance, 0.0f));
        transform.position = new Vector3(screenPos.x, screenPos.y, 0.0f);
    }

    public void ColObject() { transform.localPosition = originPos; colUi.SetActive(true); unColUi.SetActive(false); }
    public void UnColObject() { colUi.SetActive(false); unColUi.SetActive(true); }
}
