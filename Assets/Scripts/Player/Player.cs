using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity;
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
        _rigidbody.velocity = velocity * Vector3.forward;
    }

}
