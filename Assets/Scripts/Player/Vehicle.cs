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

    public float maxPitch;
    public float pitchSpeed;
    public float maxRoll;
    public float rollSpeed;
    public float resetRotationSpeed;
    
    public float maxYaw = 5f;

    public Vector3 maxEulers;

    // Start is called before the first frame update
    public void Awake()
    {
        dragHelper.Init(rigidbody.drag, rigidbody.angularDrag);
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        // rigidbody.velocity = ComputeVelocity(rigidbody.velocity);
        rigidbody.velocity = new Vector3(
            rigidbody.velocity.x,
            rigidbody.velocity.y,
            maxSpeed);
        // transform.eulerAngles = CapRotation(transform.eulerAngles);
        forwardSpeed = rigidbody.velocity.magnitude;
        rigidbody.drag = dragHelper.ComputeDrag(forwardSpeed);
        rigidbody.angularDrag = dragHelper.ComputeAngularDrag(forwardSpeed);
    }

    public void HandleInputs(Controller controller)
    {
        // HandleForces(controller);
        HandleRotations(controller);
        // HandlePowerMoves(controller);
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
        if (controller.noInputs)
        {
            var targetQ = Quaternion.Euler(Vector3.zero);
            transform.rotation = Quaternion.Lerp(
                transform.rotation, targetQ, resetRotationSpeed * Time.deltaTime);
        }
        // var roll = ComputeRoll(-controller.leftStickHorizontal);
        // var pitch = ComputePitch(controller.leftStickVertical);
        // transform.Rotate(roll + pitch);
        var roll = ComputeRoll(-controller.leftStickHorizontal);
        var pitch = controller.leftStickVertical * maxPitch;
        transform.Rotate(roll);
        // rigidbody.AddRelativeTorque(Vector3.back * roll);
        // rigidbody.AddRelativeTorque(Vector3.right * pitch);
    }

    private Vector3 ComputeRoll(float leftStickHorizontal)
    {
        Debug.Log(transform.rotation.z);
        // if (transform.eulerAngles.z < -maxRoll || transform.eulerAngles.z > maxRoll)
        //     return Vector3.zero;

        var rollForce = leftStickHorizontal * rollSpeed * Time.deltaTime;
        return Vector3.forward * rollForce;
    }

    private void HandleForces(Controller controller)
    {
        // var thrust = thrustEngine.ComputeThrust(
        //                  Vector3.forward,
        //                  -controller.rightTrigger) * (controller.braking ? 0 : 1);

        var normSpeed = Mathf.InverseLerp(0f, maxSpeed, forwardSpeed);
        var angleOfAttack = ComputeAngleOfAttack();
        var lift = liftEngine.ComputeLift(
                       Vector3.up, 
                       normSpeed,
                       angleOfAttack,
                       -controller.leftStickVertical) * (controller.braking ? 0 : 1);
        
        // var totalForce = thrust + lift;
        rigidbody.AddRelativeForce(lift);
    }
    
    private Vector3 ComputeVelocity(Vector3 velocity)
    {
        if (!(velocity.magnitude > maxSpeed)) return velocity;
        
        var curVel = velocity;
        return curVel.normalized * maxSpeed;
    }

    private float ComputeAngleOfAttack()
    {
        var angleOfAttack = Vector3.Dot(rigidbody.velocity.normalized, transform.forward);
        return Mathf.Clamp(0.0f, angleOfAttack * angleOfAttack, 1f);
    }
}
