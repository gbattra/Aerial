using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeedSmoothFactor;
    public float maxSpeed;
    public float forwardForce;
    public float moveSpeed;
    public float dodgeSpeed;
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

    public void FixedUpdate()
    {
        HandleControls();
        if (_rigidbody.velocity.magnitude > maxSpeed)
        {
            var curVel = _rigidbody.velocity;
            var targetVel = curVel.normalized * maxSpeed;
            _rigidbody.velocity = Vector3.Lerp(
                curVel, targetVel, Time.deltaTime * maxSpeedSmoothFactor);
        }
    }

    public void HandleControls()
    {
        var x = _controller.leftStickHorizontal * (Time.deltaTime * moveSpeed);
        var y = _controller.leftStickVertical * (Time.deltaTime * moveSpeed);
        var horiz = transform.right * (moveSpeed * x);
        var vert = transform.up * (moveSpeed * y);
        var accel = -_controller.rightTrigger * (Time.deltaTime * forwardForce);
        var forward = transform.forward * accel;
        _rigidbody.AddForce(horiz + vert + forward);
        
        if (_controller.a)
            _rigidbody.AddForce(transform.right * (x * dodgeSpeed)
                                + transform.up * (y * dodgeSpeed));
    }
}
