using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    public Shield shield;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float shieldTime;
    
    public bool isShielding => _isShielding;
    private bool _isShielding;
    
    private GameObject clone;
    private float shieldStartTime;

    public int shieldCount => _shieldCount;
    private int _shieldCount;

    public void Start()
    {
        _shieldCount = 1;
    }

    public void Update()
    {
        if (!_isShielding && Controller.y && shieldCount > 0)
        {
            _shieldCount = Mathf.Clamp(_shieldCount - 1, 0, shieldCount);
            shield.gameObject.SetActive(true);
            _isShielding = true;
            shieldStartTime = Time.time;
            audioSource.PlayOneShot(audioClip);
        }

        _isShielding &= Time.time - shieldStartTime < shieldTime;

        if (_isShielding)
            return;
        
        shield.gameObject.SetActive(false);
        shieldStartTime = 0;
    }

    public void AddShield(int count)
    {
        _shieldCount = Mathf.Clamp(_shieldCount + count, 0, shieldCount + count);
    }
}