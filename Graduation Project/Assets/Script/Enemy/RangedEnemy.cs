using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    override protected void AnimatorSetting()
    {
        base.AnimatorSetting();
        ani.SetBool("HaveDamaged", haveDamagedByBarrel || haveDamagedByBullet);
    }

    override protected void Die()
    {
        enemyAi.enabled = false;
        hpBar.gameObject.SetActive(false);

        foreach (Rigidbody rb in rigidBodys)
            rb.isKinematic = false;
        ani.enabled = false;

        Invoke("DieToVanish", 5.0f);
    }
}
