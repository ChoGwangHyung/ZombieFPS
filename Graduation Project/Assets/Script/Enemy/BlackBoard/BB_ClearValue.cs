using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Actions;

[Action("MyBasic/ClearValue")]

public class BB_ClearValue : BasePrimitiveAction
{
    [InParam("value")]
    public GameObject input;

    public override void OnStart()
    {
        input = null;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}