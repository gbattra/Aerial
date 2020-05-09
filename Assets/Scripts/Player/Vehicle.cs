using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public RollEngine rollEngine;
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
        HandleForces(controller);
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
        transform.rotation = ComputeRotation(
            controller.leftStickHorizontal, -controller.leftStickVertical);
    }

    private Quaternion ComputeRotation(float leftStickHorizontal, float leftStickVertical)
    {
        var roll = maxRoll * leftStickHorizontal;
        var pitch = maxPitch * leftStickVertical;
        var targetEulers = (Vector3.back * roll) + (Vector3.right * pitch);
        var targetQ = Quaternion.Euler(targetEulers);
        return Quaternion.Lerp(
            transform.rotation, targetQ, resetRotationSpeed * Time.deltaTime);
    }

    private void HandleForces(Controller controller)
    {
        var roll = rollEngine.ComputeRoll(controller.leftStickHorizontal);
        var lift = liftEngine.ComputeLift(controller.leftStickVertical);
        
        rigidbody.AddRelativeForce(lift + roll);
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
