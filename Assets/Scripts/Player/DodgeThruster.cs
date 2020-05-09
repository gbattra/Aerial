﻿using System.Collections;
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
        float vertical,
        float forwardSpeed)
    {
        var horizontalDoge = Vector3.left * -horizontal;
        // var horizontalDoge = transform.right * horizontal;
        var forwardDodge = Vector3.up * vertical;
        // var forwardDodge = transform.forward * vertical;
        return (horizontalDoge + forwardDodge) * (dodgePower * 1 * Time.deltaTime);
    }

    public Vector3 ComputeTorque(
        float horizontal,
        float vertical,
        float forwardSpeed)
    {
        var roll = transform.forward * (-horizontal * sideTorque * 1 * Time.deltaTime);
        var pitch = transform.right * (vertical * forwardTorque * 1 * Time.deltaTime);

        return roll + pitch;
    }
}
