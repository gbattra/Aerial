using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeThruster : MonoBehaviour
{
    public float dodgePower;
    public float forwardTorque;
    public float sideTorque;
    public float dodgeCooldown;

    public Vector3 ComputeDodge(
        float horizontal,
        float vertical)
    {
        var horizontalDoge = transform.right * horizontal;
        var forwardDodge = transform.forward * vertical;
        return (horizontalDoge + forwardDodge) * dodgePower;
    }

    public Vector3 ComputeTorque(
        float horizontal,
        float vertical)
    {
        var roll = transform.forward * (-horizontal * sideTorque);
        var pitch = transform.right * (vertical * forwardTorque);

        return roll + pitch;
    }
}
