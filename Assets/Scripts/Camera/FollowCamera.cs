using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool lookAt = true;
    public float cameraSpeed;
    public Vector3 lookOffset = new Vector3(0, 1, 0);
    public Vector3 offsetPositionWorld;
    public Vector3 offsetPositionSelf;
    public Space offsetPositionSpace =>
        player.controller.accelerating ? Space.Self : Space.World;
    public Player player;

    // private bool lookAt => player.controller.rightTrigger > 0;
    
    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (player == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.transform.TransformPoint(offsetPositionSelf);
        }
        else
        {
            transform.position = player.transform.position + offsetPositionWorld;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(player.transform.position + lookOffset);
        } 
        else
        {
            transform.rotation = player.transform.rotation;
        }
    }
    
}
