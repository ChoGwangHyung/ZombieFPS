using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/IsTargetCloseOrInSight")]
public class BB_IsTargetCloseOrInSight : GOCondition
{
    [InParam("target")]
    public GameObject target;

    [InParam("angle")]
    public float angle;

    [InParam("closeDistance")]
    public float closeDistance;

    [InParam("enemy")]
    public Enemy enemy;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override bool Check()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (Vector3.Distance(navAgent.transform.position, target.transform.position) <= closeDistance || enemy._haveDamagedByBarrel == true || enemy._haveDamagedByBullet == true)
            return true;

        Vector3 leftBoundary = BoundaryAngle(-this.angle * 0.5f, navAgent);
        Vector3 rightBoundary = BoundaryAngle(this.angle * 0.5f, navAgent);

        Vector3 direction = (target.transform.position - navAgent.transform.position).normalized;
        float angle = Vector3.Angle(direction, navAgent.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(navAgent.transform.position + navAgent.transform.up, direction, out hit, closeDistance, target.layer))
            if (angle < this.angle * 0.5f && angle > -this.angle * 0.5f && hit.transform.tag == "Player")
                return true;

        return false;
    }

    private Vector3 BoundaryAngle(float angle, UnityEngine.AI.NavMeshAgent navAgent)
    {
        angle += navAgent.transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
