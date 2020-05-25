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
        if (!isDodging && Controller.a)
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
        
        var horizontalDodge = Vector3.left * -Controller.leftStickHorizontal;
        var verticalDodge = Vector3.up * Controller.leftStickVertical;
        var dodge = (horizontalDodge + verticalDodge) * dodgePower;
        rigidbody.velocity = new Vector3(dodge.x, dodge.y, rigidbody.velocity.z);
        
        var roll = transform.forward * (-Controller.leftStickHorizontal * rollTorque);
        var pitch = transform.right * (Controller.leftStickVertical * pitchTorque);
        var torque = pitch + roll;
        rigidbody.AddRelativeTorque(torque);
    }
}
