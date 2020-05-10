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
    public float rollTorque;
    public float pitchTorque;
    public float dodgeCooldown;
    
    private bool _isDodging;
    public bool isDodging => _isDodging;
    
    private Vector3 dodgeDirection;


    public void Update()
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
        var dodge = (horizontalDodge + verticalDodge) * dodgePower;
        rigidbody.velocity = new Vector3(dodge.x, dodge.y, rigidbody.velocity.z);
        
        var roll = transform.forward * (-player.controller.leftStickHorizontal * rollTorque);
        var pitch = transform.right * (player.controller.leftStickVertical * pitchTorque);
        var torque = pitch + roll;
        rigidbody.AddRelativeTorque(torque);
    }
}
