using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public ThrustEngine thrustEngine;
    public Rigidbody rigidbody;

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
    
    private const float MPS_TO_MPH = 2.23604f;
    
    // Start is called before the first frame update
    public void Init(
        float drag,
        float angularDrag)
    {
        startDrag = drag;
        startAngularDrag = angularDrag;
    }

    public void HandleInputs(Controller controller)
    {
        var thrust = thrustEngine.ComputeThrust(
            transform.forward,
            -controller.rightTrigger);
        rigidbody.AddForce(thrust);
        rigidbody.velocity = CapVelocity(
            rigidbody.velocity);
        
        ComputeForwardSpeed(rigidbody.velocity);
    }

    private void ComputeForwardSpeed(Vector3 velocity)
    {
        var localVelocity = transform.InverseTransformDirection(velocity);
        forwardSpeed = localVelocity.z;
        Debug.DrawRay(transform.position, transform.position + localVelocity, Color.cyan);
    }
    
    private Vector3 CapVelocity(Vector3 velocity)
    {
        if (!(velocity.magnitude > maxSpeed)) return velocity;
        
        var curVel = velocity;
        var targetVel = curVel.normalized * maxSpeed;
        return Vector3.Lerp(
            curVel, targetVel, Time.deltaTime * maxSpeedSmoothFactor);
    }
}
