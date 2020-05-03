using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeedSmoothFactor;
    public float maxSpeed;
    public float moveSpeed;
    public float dodgeSpeed;
    
    public float maxPitch = 10f;
    public float maxRotation = 10f;
    public float maxYaw = 5f;

    public Rigidbody rigidbody;
    public ThrustEngine thrustEngine;
    public Controller controller;

    // Update is called once per frame
    void Update()
    {
    }

    public void FixedUpdate()
    {
        var thrust = thrustEngine.ComputeThrust(
            transform.forward,
            -controller.rightTrigger);
        rigidbody.AddForce(thrust);
        rigidbody.velocity = ComputeVelocity();
    }

    private Vector3 ComputeVelocity()
    {
        if (!(rigidbody.velocity.magnitude > maxSpeed)) return rigidbody.velocity;
        
        var curVel = rigidbody.velocity;
        var targetVel = curVel.normalized * maxSpeed;
        return Vector3.Lerp(
            curVel, targetVel, Time.deltaTime * maxSpeedSmoothFactor);
    }

    public void HandleControls()
    {
        var x = controller.leftStickHorizontal * (Time.deltaTime * moveSpeed);
        var y = controller.leftStickVertical * (Time.deltaTime * moveSpeed);
        var horiz = transform.right * (moveSpeed * x);
        var vert = transform.up * (moveSpeed * y);
        rigidbody.AddForce(horiz + vert);
        
        if (controller.a)
            rigidbody.AddForce(transform.right * (x * dodgeSpeed)
                               + transform.up * (y * dodgeSpeed));
    }
}
