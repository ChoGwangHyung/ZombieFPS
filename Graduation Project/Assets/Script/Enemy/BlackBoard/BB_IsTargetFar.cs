using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/IsTargetFar")]
public class BB_IsTargetFar : GOCondition
{
    [InParam("target")]
    public GameObject target;

    [InParam("farDistance")]
    public float farDistance;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override bool Check()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                
        if (Vector3.Distance(navAgent.transform.position, target.transform.position) > farDistance)
            return true;

        return false;
    }
}
