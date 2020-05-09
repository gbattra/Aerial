using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollEngine : MonoBehaviour
{
    public float power;
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private float _roll { get; set; }
    public float roll => _roll;

    public Vector3 ComputeRoll(float powerFactor, float boost)
    {
        _roll = (power + boost) * powerFactor * powerCurve.Evaluate(Time.time);
        return -Vector3.left * roll;
    }
}
