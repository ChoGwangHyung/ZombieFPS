using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhysicsDamageCol : MonoBehaviour
{
    [SerializeField] private BossEnemy boss = null;
    private bool giveDamaged = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && giveDamaged == false)
        {
            other.GetComponent<CharacterBase>().ReceiveDamage(boss._dashDamage);
            other.gameObject.GetComponent<CharacterBase>().ExplosionAction(500.0f, new Vector3(boss.transform.position.x, other.transform.position.y - 1.0f, boss.transform.position.z), 50.0f);
            giveDamaged = true;
            MainCamera.instance.ShakeCam();
            Invoke("Reset", 1.5f);
        }
    }

    private void Reset()
    {
        giveDamaged = false;
    }
}
