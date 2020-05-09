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
        var horizontalDoge = Vector3.left * -horizontal;
        // var horizontalDoge = transform.right * horizontal;
        var verticalDodge = Vector3.up * vertical;
        // var forwardDodge = transform.forward * vertical;
        return (horizontalDoge + verticalDodge) * (dodgePower * Time.deltaTime);
    }

    public Vector3 ComputeTorque(
        float horizontal,
        float vertical)
    {
        var roll = transform.forward * (-horizontal * sideTorque * Time.deltaTime);
        var pitch = transform.right * (vertical * forwardTorque * Time.deltaTime);

        return roll + pitch;
    }
}
