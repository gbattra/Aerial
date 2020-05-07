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
    
        var wantedPos = player.transform.position + positionOffset;
        var tooClose = Vector3.Distance(transform.position, player.transform.position) < minDistance;
        transform.position = Vector3.Slerp(
            transform.position,
            wantedPos + positionOffset,
            tooClose ? distancingSpeed : catchingUpSpeed);
        transform.LookAt(player.transform.position + lookOffset);
    }
    
}
