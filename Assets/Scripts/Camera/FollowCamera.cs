using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float maxDistance;
    public Vector3 positionOffset;
    public Vector3 lookOffset;
    public Player player;
    
    private void FixedUpdate()
    {
        var targetPos = player.transform.position + positionOffset;
        var smoothPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = new Vector3(
            smoothPos.x, smoothPos.y,
            smoothPos.z > maxDistance ? player.transform.position.z + maxDistance : smoothPos.y);
        
        // transform.LookAt(player.transform.position + lookOffset);
    }
    
}
