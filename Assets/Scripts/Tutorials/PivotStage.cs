using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotStage : Stage
{
    public bool rightPivot;
    
    public void Update()
    {
        if (!isActive)
            return;

        conditionResolved |= ResolveCondition();
    }
    
    protected override bool ResolveCondition()
    {
        return rightPivot ? Controller.rightBumper : Controller.leftBumper;
    }
}
