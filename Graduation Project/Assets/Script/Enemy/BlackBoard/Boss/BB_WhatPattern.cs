using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/WhatPattern")]

public class BB_WhatPattern : GOCondition
{
    [InParam("Boss Enemy")]
    public BossEnemy boss;

    [InParam("Goal Pattern")]
    public BossAttackPattern goalPattern;

    public override bool Check()
    {
        return boss.curPattern == goalPattern;
    }
}