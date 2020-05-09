using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustEngine : MonoBehaviour
{
    public float power;
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private float _thrust { get; set; }
    public float thrust => _thrust;

    public Vector3 ComputeThrust(float leftTrigger, float rightTrigger)
    {
        _thrust = power * (leftTrigger + -rightTrigger) * powerCurve.Evaluate(Time.time);
        return Vector3.forward * thrust;
    }
}
