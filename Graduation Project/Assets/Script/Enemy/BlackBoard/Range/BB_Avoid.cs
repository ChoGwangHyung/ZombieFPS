using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Actions;

[Action("MyActions / Avoid")]

public class BB_AvoidSetPos : GOAction
{
    [InParam("target")]
    public GameObject target;

    private UnityEngine.AI.NavMeshAgent navAgent;
    private Vector3 direction;
    private float changeDirectionTime = 2.0f;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetDirection();
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        navAgent.transform.LookAt(target.transform);
        navAgent.isStopped = false;
        navAgent.SetDestination(navAgent.transform.position + direction);

        if (changeDirectionTime <= 0.0f)
            return TaskStatus.COMPLETED;
        changeDirectionTime -= Time.deltaTime;
        return TaskStatus.RUNNING;
    }

    private void SetDirection()
    {
        int moveDir = Random.Range(1, 3);
        switch (moveDir)
        {
            case 1:
                direction = navAgent.transform.right * 3.0f;
                break;
            case 2:
                direction = -navAgent.transform.right * 3.0f;
                break;
        }
    }
}
