using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private Enemy enemyMain = null;
    public Enemy _enemyMain { get { return enemyMain; } }

    void Awake()
    {        
        enemyMain.AddRigidBody(GetComponent<Rigidbody>());
    }

    public void HitByBullet(float damage, Vector3 point)
    {
        enemyMain.ReceiveDamage(damage, point);
    }

    public void HitByBarrel(float damage)
    {
        if (enemyMain._haveDamagedByBarrel == true) return;
        enemyMain.ReceiveDamage(damage);        
    }
}
