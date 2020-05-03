using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BaseSpawner : MonoBehaviour
{
    public float respawnFrequency;
    public Player player;
    public List<Asteroid> asteroids;
    public float distanceFromPlayer;

    public Vector3 spawnBounds;

    public float spawnInterval;
    public float spawnDecay;
    public int spawnCount;
    public float currentSpawnTime;
    public float bigCountdown;
    public float currentBigTime;
    public int randomSeed;
    
    public bool shouldSpawn => player.rigidbody.velocity.magnitude > 0
                            && currentSpawnTime >= 
                               spawnInterval * (player.maxSpeed / player.rigidbody.velocity.magnitude);

    public Random rand;

    public void Awake()
    {
        rand = new Random(randomSeed);
        SpawnObstacle();
    }

    public void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.transform.position.z + distanceFromPlayer);
        
        currentSpawnTime += Time.deltaTime;
        currentBigTime += Time.deltaTime;

        if (shouldSpawn)
        {
            for (var i = 0; i < spawnCount; i++)
                SpawnObstacle();
            currentSpawnTime = 0;
        }

        if (currentBigTime >= bigCountdown)
        {
            spawnInterval -= spawnDecay;
            currentBigTime = 0;
        }
    }

    private void SpawnObstacle()
    {
        var obstacle = asteroids[rand.Next(asteroids.Count)];
        var position = RandomSpawnPosition();
        var clone = Instantiate(
            obstacle,
            position,
            Quaternion.identity);
        clone.player = player;
    }

    private Vector3 RandomSpawnPosition()
    {
        var x = rand.Next((int) -spawnBounds.x, (int) spawnBounds.x);
        var y = rand.Next((int) -spawnBounds.y, (int) spawnBounds.y);
        
        return new Vector3(
            transform.position.x + x,
            transform.position.y + y,
            transform.position.z);
    }
}
