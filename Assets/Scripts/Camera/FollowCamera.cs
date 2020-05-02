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
    public Space offsetPositionSpace = Space.Self;
    public Player player;

    // private bool lookAt => player.controller.rightTrigger > 0;
    
    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        transform.position = player.transform.TransformPoint(offsetPositionSelf);

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
