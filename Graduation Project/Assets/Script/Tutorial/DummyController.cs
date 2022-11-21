using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField] private List<GameObject> dummys = new List<GameObject>();

    private float revivalTime = 5.0f;

    void Update()
    {
        foreach (GameObject dummy in dummys)
            if (dummy.GetComponent<Dummy>().NeedRevivaling() == true)
                StartCoroutine(RevivalCoroutine(dummy));
    }

    IEnumerator RevivalCoroutine(GameObject dummy)
    {
        dummy.GetComponent<Dummy>().StartRevival();
        dummy.SetActive(false);        
        yield return new WaitForSeconds(revivalTime);

        dummy.SetActive(true);
        dummy.GetComponent<Dummy>().Revival();
    }
}
