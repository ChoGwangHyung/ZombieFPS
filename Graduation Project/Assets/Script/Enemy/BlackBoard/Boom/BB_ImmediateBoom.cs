using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions / ImmediateBoom")]

public class BB_ImmediateBoom : BasePrimitiveAction
{
    [InParam("Boomer Enemy")]
    public BoomerEnemy boomerEnemy;

    public override TaskStatus OnUpdate()
    {
        boomerEnemy.ImmediateBoom();
        return TaskStatus.COMPLETED;
    }
}