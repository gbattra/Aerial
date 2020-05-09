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
    public Border border;
    
    private void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x, transform.position.y, player.transform.position.z + maxDistance);
        // var targetPos = player.transform.position + positionOffset;
        // var smoothPos = Vector3.Lerp(
        //     transform.position,
        //     targetPos,
        //     border.playerOutOfBounds ? smoothSpeed / 2 : smoothSpeed);
        // transform.position = new Vector3(
        //     smoothPos.x, smoothPos.y,
        //     smoothPos.z > maxDistance ? player.transform.position.z + maxDistance : smoothPos.y);

        // transform.LookAt(border.playerOutOfBounds ?
        //     player.transform.position + lookOffset : new Vector3(
        //         transform.position.x,
        //         transform.position.y,
        //         transform.position.z + 10));
    }

}
