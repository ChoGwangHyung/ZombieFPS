using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("MyActions / MeleeAttack")]

public class BB_MeleeAttack : GOAction
{
    [InParam("Weapon")]
    public MeleeWeapon weapon;

    [InParam("target")]
    public GameObject target;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();        
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (weapon == null)
            return TaskStatus.FAILED;

        navAgent.transform.LookAt(target.transform);
        weapon.Attack();

        return TaskStatus.COMPLETED;
    }
}