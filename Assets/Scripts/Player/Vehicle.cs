﻿using System;
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
    public HealthUpAbility healthUpAbility;
    public Minigun minigun;
    public Rigidbody rigidbody;

    public float forwardSpeed;
    public float maxSpeed;
    public float maxPitch;
    public float maxRoll;
    public float maxYaw;
    
    public float health => _health;
    private float _health;
    
    public float resetRotationSpeed;
    public float rotationSpeed;


    public void Start()
    {
        _health = 1f;
    }

    public void FixedUpdate()
    {
        forwardSpeed = rigidbody.velocity.magnitude;
        // rigidbody.drag = dragHelper.ComputeDrag(forwardSpeed);
        // rigidbody.angularDrag = dragHelper.ComputeAngularDrag(forwardSpeed);

        HandleForces();
        HandleRotations();
    }

    public void HandleImpact(float damage)
    {
        if (shieldAbility.isShielding)
            return;
        _health = Mathf.Clamp(health - damage, 0, 1);
    }

    public void IncreaseHealth(float amount)
    {
        _health = Mathf.Clamp(health + amount, 0, 1);
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
        var roll = (maxRoll + (boost.isBoosting ? boost.rollBuffer : 0f)) * Controller.leftStickHorizontal;
        var pitch = (maxPitch + (boost.isBoosting ? boost.pitchBuffer : 0f)) * -Controller.leftStickVertical;
        var targetEulers = Vector3.up * yaw + Vector3.back * roll + Vector3.right * (pitch + aimPitch);
        var targetQ = Quaternion.Euler(targetEulers);
        return Quaternion.Lerp(
            transform.rotation, targetQ, rotationSpeed * Time.deltaTime);
    }

    private void HandleForces()
    {
        var thrust = thrustEngine.ComputeThrust(boost.isBoosting ? boost.boostThrust : 0f);
        var roll = rollEngine.ComputeRoll(
            Controller.leftStickHorizontal,
            boost.isBoosting ? boost.boostRoll : 0f);
        var lift = liftEngine.ComputeLift(
            Controller.leftStickVertical,
            boost.isBoosting ? boost.boostLift : 0f);
        var finalForce = dodgeMove.isDodging ? thrust : thrust + roll + lift;
        rigidbody.AddForce(finalForce);
    }
}
