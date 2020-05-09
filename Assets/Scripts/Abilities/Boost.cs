using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostSpeed;
    public float boostThrust;
    public float boostRoll;
    public float boostLift;
    public float rollBuffer;
    public float pitchBuffer;
    
    public float boostEnergy;

    public Vector3 Engage()
    {
        var boostPower = boostEnergy > 0 ? boostThrust * Time.deltaTime : 0f;
        return transform.forward * boostPower;
    }
}
