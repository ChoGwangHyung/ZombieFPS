using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("MyBasic/SetObjectPos")]

public class BB_SetObjectPos : BasePrimitiveAction
{
    [OutParam("outputVector")]
    public Vector3 output;

    [InParam("inputObject")]
    public GameObject input;

    public override void OnStart()
    {
        output = input.transform.position;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
