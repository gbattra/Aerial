﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Obstacle : MonoBehaviour
{
    public float velocity;
    public float rotateSpeed;
    public Player player;
    public Rigidbody rigidbody;

    private Random _rand;
    public Vector3 rotation;

    private const float BUFFER = -25f;
    
    public void Awake()
    {
        _rand = new Random();
        rotation = new Vector3(
            _rand.Next(0, 10),
            _rand.Next(0, 10),
            _rand.Next(0, 10)) * rotateSpeed;
    }
    
    public void FixedUpdate()
    {
        rigidbody.velocity = Vector3.back * velocity;
        if (transform.position.z < player.transform.position.z + BUFFER)
            Destroy(gameObject);
        transform.Rotate(rotation);
    }
}
  