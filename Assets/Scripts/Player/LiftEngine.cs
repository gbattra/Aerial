using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftEngine : MonoBehaviour
{
    public float power;
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private float _lift { get; set; }
    public float lift => _lift;

    public Vector3 ComputeLift(float leftStickVertical, float boost)
    {
        
        _lift = leftStickVertical * (power + boost) * powerCurve.Evaluate(Time.time);
        return Vector3.up * lift;
    }
}
