using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/canFire")]

public class BB_CanFire : GOCondition
{
    [InParam("WeaponController")]
    public EnemyWeaponController weapon;

    public override bool Check()
    {
        return weapon.CanFire();
    }
}