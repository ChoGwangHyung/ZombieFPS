using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("BossActions / AcidSpit")]

public class BB_AcidSpit : GOAction
{
    [InParam("target")]
    public GameObject target;

    [InParam("Boss Enemy")]
    public BossEnemy boss;

    private UnityEngine.AI.NavMeshAgent navAgent;
    private float dist;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (boss._isPatternEnd == true)
        {
            dist = 7.0f;
            boss.AcidAttackStart();
        }
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (dist >= Vector3.Distance(target.transform.position, navAgent.transform.position))
        {
            navAgent.transform.LookAt(target.transform);
            return TaskStatus.RUNNING;
        }
        else
        {
            boss.AcidAttackEnd();
            return TaskStatus.COMPLETED;
        }
    }
}