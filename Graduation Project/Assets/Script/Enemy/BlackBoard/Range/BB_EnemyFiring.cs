using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions / Firing")]

public class BB_EnemyFiring : BasePrimitiveAction
{
    [InParam("WeaponController")]
    public EnemyWeaponController weapon;

    [InParam("Animator")]
    public Animator ani;

    public override TaskStatus OnUpdate()
    {
        if (weapon == null || ani == null)
            return TaskStatus.FAILED;

        ani.SetBool("IsFired", true);
        weapon.Fire();
        ani.SetBool("IsFired", false);

        return TaskStatus.COMPLETED;
    }
}
