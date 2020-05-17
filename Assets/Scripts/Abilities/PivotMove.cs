using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PivotMove : MonoBehaviour
{
    public float thrustPower;
    public float pivotTime;
    public Rigidbody rigidbody;

    public bool isPivoting => _isPivoting;
    private bool _isPivoting;
    private int pivotDirection;
    private float pivotStartTime;

    private const int NONE = 0;
    private const int LEFT = 1;
    private const int RIGHT = 2;

    public void Update()
    {
        var leftBumper = Controller.leftBumper;
        var rightBumper = Controller.rightBumper;
        if (!isPivoting && (leftBumper || rightBumper))
        {
            _isPivoting = true;
            pivotStartTime = Time.time;
            pivotDirection = leftBumper ? LEFT : RIGHT;
        }
        
        _isPivoting &= Time.time - pivotStartTime < pivotTime;
        if (isPivoting) return;
        
        pivotDirection = NONE;
    }

    public void FixedUpdate()
    {
        if (!isPivoting)
            return;
        
        rigidbody.AddTorque(pivotDirection == LEFT
                            ? transform.forward * (thrustPower * Time.deltaTime)
                            : -transform.forward * (thrustPower * Time.deltaTime));
    }
}
