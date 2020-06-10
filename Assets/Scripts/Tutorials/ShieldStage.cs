using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStage : Stage
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
        return vehicle.shieldAbility.isShielding;
    }
}
