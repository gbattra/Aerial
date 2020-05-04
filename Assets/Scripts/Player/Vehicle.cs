using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public ThrustEngine thrustEngine;
    public LiftEngine liftEngine;
    public Rigidbody rigidbody;

    public float forwardSpeed;
    public float maxSpeed;
    public float moveSpeed;
    public float dodgeSpeed;
    
    public float pitchPower = 10f;
    public float pitchSmoothFactor;
    public AnimationCurve pitchCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public float rollPower = 10f;
    public float rotationSmoothFactor;
    public AnimationCurve rollCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    
    public float maxYaw = 5f;

    public Vector3 maxEulers;

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

    public void FixedUpdate()
    {
        rigidbody.velocity = CapVelocity(rigidbody.velocity);
        // transform.eulerAngles = CapRotation(transform.eulerAngles);
        forwardSpeed = ComputeForwardSpeed(rigidbody.velocity);
    }

    public void HandleInputs(Controller controller)
    {
        var thrust = thrustEngine.ComputeThrust(
            transform.forward,
            -controller.rightTrigger) * (controller.braking ? 0 : 1);

        var normSpeed = Mathf.InverseLerp(0f, maxSpeed, forwardSpeed);
        var lift = liftEngine.ComputeLift(
            transform.up,
            normSpeed,
            -controller.leftStickVertical) * (controller.braking ? 0 : 1);
        
        var totalForce = thrust + lift;
        rigidbody.AddForce(totalForce);

        var roll = ComputeRoll(-controller.leftStickHorizontal);
        var pitch = ComputePitch(controller.leftStickVertical);
        transform.Rotate(roll + pitch, Space.World);
    }

    private float ComputeForwardSpeed(Vector3 velocity)
    {
        var localVelocity = transform.InverseTransformDirection(velocity);
        var maxVelocity = Mathf.Max(0f, localVelocity.z);
        return Mathf.Clamp(0f, maxVelocity, maxSpeed);
    }
    
    private Vector3 CapVelocity(Vector3 velocity)
    {
        if (!(velocity.magnitude > maxSpeed)) return velocity;
        
        var curVel = velocity;
        return curVel.normalized * maxSpeed;
    }

    private Vector3 CapRotation(Vector3 rotation)
    {
        var x = Mathf.Clamp(-maxEulers.x, rotation.x, maxEulers.x);
        var y = Mathf.Clamp(-maxEulers.y, rotation.y, maxEulers.y);
        var z = Mathf.Clamp(-maxEulers.z, rotation.z, maxEulers.z);
        return new Vector3(x, y, z);
    }

    private Vector3 ComputeRoll(float rollFactor)
    {
        var roll = rollFactor * rollPower * rollCurve.Evaluate(Time.time);
        return transform.forward * roll;
    }

    private Vector3 ComputePitch(float pitchFactor)
    {
        var pitch = pitchFactor * pitchPower * pitchCurve.Evaluate(Time.time);
        return transform.right * pitch;
    }
}
