using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public ThrustEngine thrustEngine;
    public LiftEngine liftEngine;
    public DragHelper dragHelper;
    public DodgeThruster dodgeThruster;
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

    // Start is called before the first frame update
    public void Awake()
    {
        dragHelper.Init(rigidbody.drag, rigidbody.angularDrag);
    }

    public void FixedUpdate()
    {
        rigidbody.velocity = ComputeVelocity(rigidbody.velocity);
        // transform.eulerAngles = CapRotation(transform.eulerAngles);
        forwardSpeed = rigidbody.velocity.magnitude;
        rigidbody.drag = dragHelper.ComputeDrag(forwardSpeed);
        rigidbody.angularDrag = dragHelper.ComputeAngularDrag(forwardSpeed);
    }

    public void HandleInputs(Controller controller)
    {
        HandleForces(controller);
        HandleRotations(controller);
        HandlePowerMoves(controller);
    }

    private void HandlePowerMoves(Controller controller)
    {
        if (controller.a)
        {
            var dodge = dodgeThruster.ComputeDodge(
                controller.leftStickHorizontal, controller.leftStickVertical, forwardSpeed);
            var torque = dodgeThruster.ComputeTorque(
                controller.leftStickHorizontal, controller.leftStickVertical, forwardSpeed);
            // rigidbody.AddForce(dodge);
            rigidbody.AddTorque(torque);
        }
    }

    private void HandleRotations(Controller controller)
    {
        // var roll = ComputeRoll(-controller.leftStickHorizontal);
        // var pitch = ComputePitch(controller.leftStickVertical);
        // transform.Rotate(roll + pitch);
        var roll = controller.leftStickHorizontal * rollPower; // * rollCurve.Evaluate(Time.time);
        var pitch = controller.leftStickVertical * pitchPower; // * pitchCurve.Evaluate(Time.time);
        rigidbody.AddRelativeTorque(Vector3.back * roll);
        rigidbody.AddRelativeTorque(Vector3.right * pitch);
    }

    private void HandleForces(Controller controller)
    {
        var thrust = thrustEngine.ComputeThrust(
                         Vector3.forward,
                         -controller.rightTrigger) * (controller.braking ? 0 : 1);

        var normSpeed = Mathf.InverseLerp(0f, maxSpeed, forwardSpeed);
        var angleOfAttack = ComputeAngleOfAttack();
        var lift = liftEngine.ComputeLift(
                       Vector3.up, 
                       normSpeed,
                       angleOfAttack,
                       -controller.leftStickVertical) * (controller.braking ? 0 : 1);
        
        var totalForce = thrust + lift;
        rigidbody.AddRelativeForce(totalForce);
    }
    
    private Vector3 ComputeVelocity(Vector3 velocity)
    {
        if (!(velocity.magnitude > maxSpeed)) return velocity;
        
        var curVel = velocity;
        return curVel.normalized * maxSpeed;
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

    private float ComputeAngleOfAttack()
    {
        var angleOfAttack = Vector3.Dot(rigidbody.velocity.normalized, transform.forward);
        return Mathf.Clamp(0.0f, angleOfAttack * angleOfAttack, 1f);
    }
}
