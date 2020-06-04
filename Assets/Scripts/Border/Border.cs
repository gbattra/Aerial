using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Player player;
    public Vector2 bounds;


    void FixedUpdate()
    {
        var playerPos = player.transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -bounds.x, bounds.x);
        playerPos.y = Mathf.Clamp(playerPos.y, -bounds.y, bounds.y);
        player.transform.position = playerPos;
    }
}
