﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float maxDistance;
    public float boostReduction;
    public float boostFOV;
    public float normalFOV;
    
    public Vector3 positionOffset;
    public Vector3 moveableRange;
    
    public AnimationCurve fovCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    
    public Player player;
    public Border border;
    public Camera camera;

    private void FixedUpdate()
    {
        var x = TargetX();
        var y = TargetY();
        var targetPos = new Vector3(x, y, player.transform.position.z + maxDistance);
        var smoothPos = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed + (Controller.b ? boostReduction : 0f));
        transform.position = smoothPos;
        var fov = Controller.b ? boostFOV : normalFOV;
        camera.fieldOfView = fov * fovCurve.Evaluate(Time.time);
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
