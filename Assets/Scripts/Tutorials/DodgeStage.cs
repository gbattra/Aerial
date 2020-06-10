using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeStage : Stage
{
    public Vehicle vehicle;
    public void Update()
    {
        if (!isActive)
            return;

        conditionResolved |= ResolveCondition();
    }
    
    protected override bool ResolveCondition()
    {
        return vehicle.dodgeMove.isDodging;
    }
}
