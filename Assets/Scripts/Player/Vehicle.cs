using System;
using System.Collections;
using System.Collections.Generic;
using QFX.IFX;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Player player;
    public RollEngine rollEngine;
    public ThrustEngine thrustEngine;
    public LiftEngine liftEngine;
    public DodgeMove dodgeMove;
    public PivotMove pivotMove;
    public Boost boost;
    public ShieldAbility shieldAbility;
    public IFX_Minigun minigun;
    public Rigidbody rigidbody;

    public float forwardSpeed;
    public float maxSpeed;
    public float maxPitch;
    public float maxRoll;
    public float maxYaw;
    
    public float resetRotationSpeed;

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        forwardSpeed = rigidbody.velocity.magnitude;
        // rigidbody.drag = dragHelper.ComputeDrag(forwardSpeed);
        // rigidbody.angularDrag = dragHelper.ComputeAngularDrag(forwardSpeed);

        HandleForces();
        HandleRotations();
    }

    private void HandleRotations()
    {
        if (pivotMove.isPivoting)
            return;
        transform.rotation = ComputeRotation();
    }

    private Quaternion ComputeRotation()
    {
        if (Controller.noInputs)
        {
            return Quaternion.Lerp(
                transform.rotation, 
                Quaternion.Euler(Vector3.zero),
                resetRotationSpeed * Time.deltaTime);
        }
        var yaw = maxYaw * Controller.rightStickHorizontal;
        var aimPitch = maxYaw * -Controller.rightStickVertical;
        var roll = (maxRoll + (Controller.b ? boost.rollBuffer : 0f)) * Controller.leftStickHorizontal;
        var pitch = (maxPitch + (Controller.b ? boost.pitchBuffer : 0f)) * -Controller.leftStickVertical;
        var targetEulers = Vector3.up * yaw + Vector3.back * roll + Vector3.right * (pitch + aimPitch);
        var targetQ = Quaternion.Euler(targetEulers);
        return Quaternion.Lerp(
            transform.rotation, targetQ, resetRotationSpeed * Time.deltaTime);
    }

    private void HandleForces()
    {
        var thrust = thrustEngine.ComputeThrust(Controller.b ? boost.boostThrust : 0f);
        var roll = rollEngine.ComputeRoll(
            Controller.leftStickHorizontal,
            Controller.b ? boost.boostRoll : 0f);
        var lift = liftEngine.ComputeLift(
            Controller.leftStickVertical,
            Controller.b ? boost.boostLift : 0f);
        var finalForce = dodgeMove.isDodging ? thrust : thrust + roll + lift;
        rigidbody.AddForce(finalForce);
    }
}
