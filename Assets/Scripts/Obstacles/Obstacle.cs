using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Obstacle : MonoBehaviour
{
    public float velocity;
    public float rotateSpeed;
    public float damageAmount;
    public float points;
    
    public Player player;
    public Rigidbody rigidbody;
    public Vector3 rotation;
    public Vector3 rotationDir;
    public ParticleSystem hitEffect;
    public AudioSource audioSource;
    public AudioClip ambientSound;
    public AudioClip onDestroySound;
    
    private Random _rand;

    private bool hasEntered;
    private const float BUFFER = -25f;
    
    public void Awake()
    {
        _rand = new Random();
        rotation = new Vector3(
            _rand.Next(0, 10) * rotationDir.x,
            _rand.Next(0, 10) * rotationDir.y,
            _rand.Next(0, 10) * rotationDir.z) * rotateSpeed;
    }

    public void Start()
    {
        audioSource.clip = ambientSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void FixedUpdate()
    {
        rigidbody.velocity = Vector3.back * velocity;
        if (transform.position.z < player.transform.position.z + BUFFER)
            Destroy(gameObject);
        transform.Rotate(rotation);
    }
    

    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") || hasEntered) return;
        
        hasEntered = true;
        other.gameObject.GetComponent<Vehicle>().HandleImpact(damageAmount);
    }
    
}
  