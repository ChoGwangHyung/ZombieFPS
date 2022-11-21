using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Actions;

[Action("MyBasic/SetObject")]

public class BB_SetObject : BasePrimitiveAction
{
    [OutParam("outputObject")]
    public GameObject outputObject;

    [InParam("inputObject")]
    public GameObject inputObject;

    public override void OnStart()
    {
        outputObject = inputObject;        
    }

    public override TaskStatus OnUpdate()
    {     
        return TaskStatus.COMPLETED;
    }
}