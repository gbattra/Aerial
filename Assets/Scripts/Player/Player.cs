using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Controller controller;
    public Vehicle vehicle;

    public void Awake()
    {
        vehicle.Init(rigidbody.drag, rigidbody.angularDrag);
    }

    public void FixedUpdate()
    {
        var thrust = vehicle.ComputeThrust(
            transform.forward,
            -controller.rightTrigger);
        rigidbody.AddForce(thrust);
        rigidbody.velocity = vehicle.CapVelocity(
            rigidbody.velocity);
    }
    
}
