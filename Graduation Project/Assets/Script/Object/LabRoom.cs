using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabRoom : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossStageController.instance.SummonBoss();
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
