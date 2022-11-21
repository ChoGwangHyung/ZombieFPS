using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/canMeleeAttack")]

public class BB_CanMeleeAttack : GOCondition
{
    [InParam("Weapon")]
    public MeleeWeapon weapon;

    [InParam("target")]
    public GameObject target;

    public override bool Check()
    {
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().transform.LookAt(target.transform);
        return weapon.CanAttack();
    }
}