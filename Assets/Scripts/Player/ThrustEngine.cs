using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustEngine : MonoBehaviour
{
    public float power;
    public float smoothFactor;

    private float _thrust { get; set; }
    public float thrust => _thrust;

    public Vector3 ComputeThrust(
        Vector3 forward,
        float powerFactor)
    {
        Debug.Log(powerFactor);
        _thrust = powerFactor * (Time.deltaTime * power);
        return thrust * forward;
    }
}
