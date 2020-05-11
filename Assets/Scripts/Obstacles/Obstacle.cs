using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Obstacle : MonoBehaviour
{
    public float velocity;
    public float velocityFactor;
    public float rotateSpeed;
    public Player player;
    public Rigidbody rigidbody;

    private Random _rand;
    public Vector3 rotation;

    public void Awake()
    {
        _rand = new Random();
        rotation = new Vector3(
            _rand.Next(0, 10),
            _rand.Next(0, 10),
            _rand.Next(0, 10)) * rotateSpeed;
    }

    private const float BUFFER = -500f;


    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = Vector3.back * (velocity * velocityFactor);
        if (transform.position.z < player.transform.position.z + BUFFER)
            Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        transform.Rotate(rotation);
    }
}
  