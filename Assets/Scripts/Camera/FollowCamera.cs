using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float distance;
    public Player player;

    private Vector3 smoothVelocity;
    
    private void FixedUpdate()
    {
        var wantedPos = player.transform.position + -player.transform.forward * distance;
        Debug.DrawLine(player.transform.position, wantedPos, Color.green);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            wantedPos,
            ref smoothVelocity,
            smoothSpeed);
        transform.LookAt(player.transform.position);
    }
    
}
