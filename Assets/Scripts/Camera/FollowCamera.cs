using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float maxDistance;
    public float boostReduction;
    public Vector3 positionOffset;
    public Vector3 moveableRange;
    
    public Player player;
    public Border border;
    
    private void FixedUpdate()
    {
        var x = TargetX();
        var y = TargetY();
        var targetPos = new Vector3(x, y, player.transform.position.z + maxDistance);
        var smoothPos = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed + (player.controller.b ? boostReduction : 0f));
        transform.position = smoothPos;
    }

    public float TargetX()
    {
        var playerX = player.transform.position.x;
        var targetX = playerX;
        if (playerX > moveableRange.x)
            targetX = moveableRange.x;
        if (playerX < -moveableRange.x)
            targetX = -moveableRange.x;

        return targetX;
    }
    
    public float TargetY()
    {
        var playerY = player.transform.position.y;
        var targetY = playerY;
        if (playerY > moveableRange.y)
            targetY = moveableRange.y;
        if (playerY < -moveableRange.y)
            targetY = -moveableRange.y;

        return targetY;
    }
}
