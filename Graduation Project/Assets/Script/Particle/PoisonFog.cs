using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFog : MonoBehaviour
{
    private BoxCollider col = null;
    private float poisoningTime = 5.0f;
    private float curPoisioningTime = 0.0f;
    private float damage = 2.0f;
    private float damageTime = 1.0f;

    private bool startPoisoning = false;
    public bool _startPoisoning { get { return startPoisoning; } }

    void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = startPoisoning;
    }

    public void StartPoisioning() { col.enabled = true; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        curPoisioningTime = poisoningTime;
        if (startPoisoning == false)
            StartCoroutine(PoisionDamageCouroutine(other.gameObject));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        curPoisioningTime = poisoningTime;
        if (startPoisoning == false)
            StartCoroutine(PoisionDamageCouroutine(other.gameObject));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        curPoisioningTime = poisoningTime;
    }

    private IEnumerator PoisionDamageCouroutine(GameObject obj)
    {
        startPoisoning = true;
        UiController.instance._playerStateUi.StartConditionState(Condition.Poision);

        while (curPoisioningTime > 0.0f)
        {
            obj.GetComponent<CharacterBase>().ReceiveDamage(damage);            
            curPoisioningTime -= damageTime;
            yield return new WaitForSeconds(damageTime);
        }

        UiController.instance._playerStateUi.EndConditionState(Condition.Poision);
        startPoisoning = false;
        col.enabled = false;
    }
}
