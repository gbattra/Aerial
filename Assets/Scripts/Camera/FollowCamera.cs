using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float distance;
    public Player player;
    
    private void FixedUpdate()
    {
        var wantedPos = player.transform.position + -player.transform.forward * distance;
        transform.position = Vector3.Slerp(
            transform.position,
            wantedPos,
            smoothSpeed);
        transform.LookAt(player.transform.position);
    }
    
}
