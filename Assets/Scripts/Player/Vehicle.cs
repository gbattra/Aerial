using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Player player;
    public RollEngine rollEngine;
    public ThrustEngine thrustEngine;
    public LiftEngine liftEngine;
    public DragHelper dragHelper;
    public DodgeThruster dodgeThruster;
    public Boost boost;
    public Rigidbody rigidbody;

    public float forwardSpeed;
    public float maxSpeed;
    public float maxPitch;
    public float maxRoll;
    public float resetRotationSpeed;

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
        rigidbody.velocity = ComputeVelocity(rigidbody.velocity);
        // rigidbody.velocity = new Vector3(
        //     rigidbody.velocity.x,
        //     rigidbody.velocity.y,
        //     maxSpeed + (player.controller.b ? boost.boostSpeed : 0f));
        // transform.eulerAngles = CapRotation(transform.eulerAngles);
        forwardSpeed = rigidbody.velocity.magnitude;
        // rigidbody.drag = dragHelper.ComputeDrag(forwardSpeed);
        // rigidbody.angularDrag = dragHelper.ComputeAngularDrag(forwardSpeed);

        HandleForces(player.controller);
        HandleRotations(player.controller);
    }

    private void HandlePowerMoves(Controller controller)
    {
        if (controller.a)
        {
            var dodge = dodgeThruster.ComputeDodge(
                controller.leftStickHorizontal, controller.leftStickVertical);
            var torque = dodgeThruster.ComputeTorque(
                controller.leftStickHorizontal, controller.leftStickVertical);
            rigidbody.AddForce(dodge);
            // rigidbody.AddTorque(torque);
        }

        if (controller.b)
        {
            rigidbody.AddForce(boost.Engage());
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
        var roll = (maxRoll + (player.controller.b ? boost.rollBuffer : 0f)) * leftStickHorizontal;
        var pitch = (maxPitch + (player.controller.b ? boost.pitchBuffer : 0f)) * leftStickVertical;
        var targetEulers = (Vector3.back * roll) + (Vector3.right * pitch);
        var targetQ = Quaternion.Euler(targetEulers);
        return Quaternion.Lerp(
            transform.rotation, targetQ, resetRotationSpeed * Time.deltaTime);
    }

    private void HandleForces(Controller controller)
    {
        var thrust = thrustEngine.ComputeThrust(controller.b ? boost.boostThrust : 0f);
        var roll = rollEngine.ComputeRoll(
            controller.leftStickHorizontal,
            controller.b ? boost.boostRoll : 0f);
        var lift = liftEngine.ComputeLift(
            controller.leftStickVertical,
            controller.b ? boost.boostLift : 0f);
        rigidbody.AddForce(lift + roll + thrust);
    }
    
    private Vector3 ComputeVelocity(Vector3 velocity)
    {
        var z = Mathf.Clamp(
            0f, velocity.z, maxSpeed + (player.controller.b ? boost.boostSpeed : 0f));
        return new Vector3(velocity.x, velocity.y, z);
    }
}
