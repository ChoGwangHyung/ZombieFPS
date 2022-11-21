using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/HaveTarget")]

public class BB_HaveTarget : GOCondition
{
    [InParam("Target")]    
    public GameObject target;

    public override bool Check()
    {
        return target != null;
    }
}