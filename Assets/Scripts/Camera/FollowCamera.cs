using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float distancingSpeed;
    public float catchingUpSpeed;
    public float minDistance;
    public float maxDistance;
    public Vector3 positionOffset;
    public Vector3 lookOffset;
    public Player player;
    
    private void FixedUpdate()
    {
    
        var playerToCam = (transform.position - player.transform.position).normalized;
        var wantedPos = player.controller.braking
            ? player.transform.position + playerToCam * maxDistance
            : player.transform.position + -player.transform.forward * maxDistance;
        var tooClose = Vector3.Distance(transform.position, player.transform.position) < minDistance;
        transform.position = Vector3.Slerp(
            transform.position,
            wantedPos + positionOffset,
            tooClose ? distancingSpeed : catchingUpSpeed);
        transform.LookAt(player.transform.position + lookOffset);
    }
    
}
