using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStage : Stage
{
    public void Update()
    {
        if (!isActive)
            return;

        conditionResolved |= ResolveCondition();
    }
    
    protected override bool ResolveCondition()
    {
        return Controller.rightStickHorizontal != 0f && Controller.rightStickVertical != 0f;
    }
}
