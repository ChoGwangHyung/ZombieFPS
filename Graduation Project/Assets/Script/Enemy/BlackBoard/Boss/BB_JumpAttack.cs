using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

[Action("BossActions / JumpAttack")]

public class BB_JumpAttack : GOAction
{
    [InParam("target")]
    public GameObject target;

    [InParam("Boss Enemy")]
    public BossEnemy boss;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        navAgent.enabled = false;
        if (boss._isPatternEnd == true)
        {
            boss.JumpAttackStart(target.transform.position, Time.time);
        }
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        Vector3 center = (boss.transform.position + boss._goalPos) * 0.5f;
        center -= new Vector3(0, 1, 0);
        Vector3 start = boss.transform.position - center;
        Vector3 end = boss._goalPos - center;
        boss.transform.position = Vector3.Slerp(start, end, (Time.time - boss._startJumpTime) / boss._jumpTime);
        boss.transform.position += center;
        if (boss.transform.position.y > boss._goalPos.y + 4.0f)
            boss.transform.position = new Vector3(boss.transform.position.x, boss._goalPos.y + 4.0f, boss.transform.position.z);

        if (Vector3.Distance(boss.transform.position, boss._goalPos) <= 0.1f)
        {
            boss.transform.position = boss._goalPos;
            boss.JumpAttackEnd();
            navAgent.enabled = true;
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }
}