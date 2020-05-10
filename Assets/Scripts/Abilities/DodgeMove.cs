using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeMove : MonoBehaviour
{
    public Player player;
    public Rigidbody rigidbody;
    
    public float dodgePower;
    public float dodgeTime;
    public float startDodgeTime;

    private bool _isDodging;
    public bool isDodging => _isDodging;
    
    private Vector3 dodgeDirection;
    
    public float forwardTorque;
    public float sideTorque;
    public float dodgeCooldown;

    public void FixedUpdate()
    {
        if (!isDodging && player.controller.a)
        {
            dodgeTime = startDodgeTime;
            _isDodging = true;
        }

        dodgeTime = isDodging ? dodgeTime - Time.deltaTime : 0f;
        if (isDodging && dodgeTime <= 0f)
        {
            _isDodging = false;
            rigidbody.velocity = new Vector3(0, 0, rigidbody.velocity.z);
        }
        
        if (!isDodging) return;
        
        var horizontalDodge = Vector3.left * -player.controller.leftStickHorizontal;
        var verticalDodge = Vector3.up * player.controller.leftStickVertical;
        var dodge = (horizontalDodge + verticalDodge) * (dodgePower * Time.deltaTime);
        
        rigidbody.velocity = new Vector3(dodge.x, dodge.y, rigidbody.velocity.z);
    }

    public Vector3 ComputeTorque(
        float horizontal,
        float vertical)
    {
        var roll = transform.forward * (-horizontal * sideTorque);
        var pitch = transform.right * (vertical * forwardTorque);

        return roll + pitch;
    }
}
