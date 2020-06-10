using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Stage : MonoBehaviour
{
    public bool isActive;
    public bool conditionResolved;

    protected virtual bool ResolveCondition()
    {
        throw new NotImplementedException();
    }
}
