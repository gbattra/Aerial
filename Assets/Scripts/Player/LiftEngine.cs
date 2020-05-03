﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftEngine : MonoBehaviour
{
    public float power;
    public float smoothFactor;
    
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private float _lift { get; set; }
    public float lift => _lift;

    public Vector3 ComputeLift(
        Vector3 up,
        float forwardSpeed,
        float powerFactor)
    {
        _lift = powerFactor * power * powerCurve.Evaluate(Time.time);
        return lift * up;
    }
}
