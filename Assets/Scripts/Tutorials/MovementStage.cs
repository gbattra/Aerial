using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStage : Stage
{
    public void Update()
    {
        if (!isActive)
            return;

        conditionResolved |= ResolveCondition();
    }
    
    protected override bool ResolveCondition()
    {
        return Controller.leftStickVertical != 0f && Controller.leftStickHorizontal != 0f;
    }
}
