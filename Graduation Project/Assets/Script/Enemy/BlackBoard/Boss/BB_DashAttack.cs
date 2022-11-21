using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("BossActions / DashAttack")]

public class BB_DashAttack : GOAction
{
    [InParam("target")]
    public GameObject target;

    [InParam("Boss Enemy")]
    public BossEnemy boss;

    private UnityEngine.AI.NavMeshAgent navAgent;
    private Vector3 goalPos;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (boss._isPatternEnd == true)
        {
            Vector3 dir = (target.transform.position - navAgent.transform.position).normalized;
            goalPos = target.transform.position + dir * 5.0f;
        }
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (boss._isPatternEnd == true)
        {
            boss.DashAttackStart();
            navAgent.SetDestination(goalPos);
        }

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            boss.DashAttackEnd();
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }
}