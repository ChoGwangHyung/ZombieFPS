using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("BossActions / Rest")]

public class BB_Rest : GOAction
{
    [InParam("target")]
    public GameObject target;

    [InParam("Boss Enemy")]
    public BossEnemy boss;

    private UnityEngine.AI.NavMeshAgent navAgent;
    private static float time;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (boss._isPatternEnd == true)
        {
            boss.PatternStart();
            time = 2.0f;
        }

        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        time -= Time.deltaTime;
        if (time > 0.0f)
        {
            navAgent.transform.LookAt(target.transform);
            return TaskStatus.RUNNING;
        }
        else
        {
            boss.PatternEnd();
            return TaskStatus.COMPLETED;
        }
    }
}