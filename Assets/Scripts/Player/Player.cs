using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardForce = 10f;
    public float verticalForce = 10f;
    public float horizontalForce = 10f;
    public float maxPitch = 10f;
    public float maxRotation = 10f;
    public float maxYaw = 5f;

    public Rigidbody _rigidbody;
    
    private Controller _controller;
    
    public Controller controller => _controller;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = new Controller();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var force = ComputeForce();
        _rigidbody.AddForce(force);
    }

    public Vector3 ComputeForce()
    {
        var lift = transform.up * (verticalForce * _controller.leftStickVertical * Time.deltaTime);
        var turn = transform.right * (horizontalForce * _controller.Yaw * Time.deltaTime);
        return lift + turn;
    }

    public Vector3 ComputeRotation()
    {
        var pitch = transform.right * (maxPitch * _controller.leftStickVertical * Time.deltaTime);
        var roll = transform.forward * (-maxRotation * _controller.Roll * Time.deltaTime);
        var yaw = transform.up * (maxYaw * _controller.Yaw * Time.deltaTime);
        return pitch + roll + yaw;
    }
}
