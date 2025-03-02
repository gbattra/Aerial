﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustEngine : MonoBehaviour
{
    public float power;
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private float _thrust { get; set; }
    public float thrust => _thrust;

    public Vector3 ComputeThrust(
        Vector3 forward,
        float powerFactor)
    {
        _thrust = power * powerFactor * powerCurve.Evaluate(Time.time);
        return thrust * forward;
    }
}
