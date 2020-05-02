using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity;
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
        _rigidbody.velocity = velocity * Vector3.forward;
    }

    public void HandleControls()
    {
        var x = _controller.leftStickHorizontal * (Time.deltaTime * moveSpeed);
        var y = _controller.leftStickVertical * (Time.deltaTime * moveSpeed);
        var horiz = transform.right * (moveSpeed * x);
        var vert = transform.up * (moveSpeed * y);
        _rigidbody.AddForce(horiz + vert);
        
        if (_controller.a)
            _rigidbody.AddForce(transform.right * (x * dodgeSpeed)
                                + transform.up * (y * dodgeSpeed));
    }
}
