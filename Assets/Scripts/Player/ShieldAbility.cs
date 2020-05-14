using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    public Shield shield;
    public float shieldTime;
    public bool isShielding => _isShielding;

    private float shieldStartTime;
    private bool _isShielding;
    private GameObject clone;

    public void Update()
    {
        if (!_isShielding && Controller.y)
        {
            shield.gameObject.SetActive(true);
            _isShielding = true;
            shieldStartTime = Time.time;
        }

        _isShielding &= Time.time - shieldStartTime < shieldTime;

        if (_isShielding)
            return;
        
        shield.gameObject.SetActive(false);
        shieldStartTime = 0;
    }
}