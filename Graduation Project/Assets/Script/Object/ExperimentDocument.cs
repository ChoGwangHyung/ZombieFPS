using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentDocument : MonoBehaviour
{
    [SerializeField] private ObjectText objectText = null;
    [SerializeField] private Slider progressBar = null;

    private bool canGet = false;

    private bool isCouroutineActive = false;

    void Start()
    {
        progressBar.value = 0.0f;
        progressBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && canGet == true) return;
        objectText.ColObject();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player" && canGet == true) return;

        if (Input.GetKey(KeyCode.E) && isCouroutineActive == false)
        {
            isCouroutineActive = true;
            StartCoroutine(GetCouroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player" && canGet == true) return;
        objectText.UnColObject();
    }

    private IEnumerator GetCouroutine()
    {
        GameController.instance.StopGame();
        objectText.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(true);

        while (progressBar.value < 1.0f)
        {
            progressBar.value += Time.deltaTime / 2.0f;
            yield return null;
        }

        objectText.gameObject.SetActive(false);
        canGet = false;
        Stage2Controller.instance.GoNextStep();
        GameController.instance.PlayGame();
    }

    public void CanGetExperimentDocumentStart()
    {
        canGet = true;
        objectText.gameObject.SetActive(true);
    }
}
