using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1NextStepArea : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (Stage1Controller.instance.CanGoToNextStep() == true && other.tag == "Player")
        {
            if (gameObject.name == (Stage1Controller.instance._enemySummonIndex + 1).ToString())
                Stage1Controller.instance.GoNextStep();
        }
    }
}
