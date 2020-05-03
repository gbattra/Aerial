using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public ThrustEngine thrustEngine;

    public float forwardSpeed;
    public float maxSpeed;
    public float maxSpeedSmoothFactor;
    public float moveSpeed;
    public float dodgeSpeed;
    
    public float maxPitch = 10f;
    public float maxRotation = 10f;
    public float maxYaw = 5f;
    
    private float startDrag;
    private float startAngularDrag;
    
    // Start is called before the first frame update
    public void Init(
        float drag,
        float angularDrag)
    {
        startDrag = drag;
        startAngularDrag = angularDrag;
    }

    public void ComputeForwardSpeed(Vector3 velocity)
    {
        var localVelocity = transform.InverseTransformDirection(velocity);
        forwardSpeed = localVelocity.z;
    }

    public Vector3 ComputeThrust(
        Vector3 forward,
        float powerFactor)
    {
        return thrustEngine.ComputeThrust(forward, powerFactor);
    }
    
    public Vector3 CapVelocity(Vector3 velocity)
    {
        if (!(velocity.magnitude > maxSpeed)) return velocity;
        
        var curVel = velocity;
        var targetVel = curVel.normalized * maxSpeed;
        return Vector3.Lerp(
            curVel, targetVel, Time.deltaTime * maxSpeedSmoothFactor);
    }
}
