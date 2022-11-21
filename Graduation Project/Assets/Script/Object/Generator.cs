using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private ObjectText objectText = null;

    [SerializeField] private GameObject door = null;
    [SerializeField] private Camera cam = null;

    private bool generatorStart = false;
    private bool operateDone = false;
    public bool _operateDone { get { return operateDone; } }

    private bool isCouroutineActive = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && operateDone == false && generatorStart == true) return;
        objectText.ColObject();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && operateDone == false && generatorStart == true) return;

        if (Input.GetKey(KeyCode.E)&& isCouroutineActive == false)
        {
            isCouroutineActive = true;
            cam.gameObject.SetActive(true);
            StartCoroutine(OpenDoorCouroutine());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && operateDone == false && generatorStart == true) return;
        objectText.UnColObject();
    }

    private IEnumerator OpenDoorCouroutine()
    {
        GameController.instance.StopGame();
        objectText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        Vector3 doorPos = door.transform.localPosition;

        while (door.transform.localPosition.y < 5.0f)
        {
            doorPos.y += Time.deltaTime;
            door.transform.localPosition = doorPos;
            yield return null;
        }

        objectText.gameObject.SetActive(false);
        cam.gameObject.SetActive(false);
        operateDone = true;
        Stage1Controller.instance.GoNextStep();
        GameController.instance.PlayGame();
    }

    public void GeneratorStart()
    {
        generatorStart = true;
        objectText.gameObject.SetActive(true);
    }
}
