using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float maxDistance;
    public float boostReduction;
    public Vector3 positionOffset;
    public Vector3 lookOffset;
    
    public Player player;
    public Border border;
    
    private void FixedUpdate()
    {
        var targetPos = new Vector3(
            transform.position.x, transform.position.y, player.transform.position.z + maxDistance);
        var smoothPos = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed + (player.controller.b ? boostReduction : 0f));
        transform.position = smoothPos;
        
        // transform.LookAt(border.playerOutOfBounds ?
        //     player.transform.position + lookOffset : new Vector3(
        //         transform.position.x,
        //         transform.position.y,
        //         transform.position.z + 10));
    }

}
