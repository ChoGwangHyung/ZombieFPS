using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/IsTargetPosClose")]

public class BB_IsTargetPosClose : GOCondition
{
    [InParam("targetPos")]
    public Vector3 targetPos;

    [InParam("closeDistance")]
    public float closeDistance;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override bool Check()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        return (navAgent.transform.position - targetPos).sqrMagnitude < closeDistance * closeDistance;
    }
}