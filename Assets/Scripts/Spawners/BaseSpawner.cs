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
    public float currentObstacleVelocity;

    public Vector3 spawnBounds;

    public float spawnInterval;
    public float spawnDecay;
    public float currentSpawnTime;
    public float bigCountdown;
    public float currentBigTime;

    public Random _rand;

    public void Awake()
    {
        _rand = new Random();
        SpawnObstacle();
    }

    public void FixedUpdate()
    {
        transform.position = new Vector3(
            player.transform.position.x, player.transform.position.y, transform.position.z);
        
        currentSpawnTime += Time.deltaTime;
        currentBigTime += Time.deltaTime;

        if (currentSpawnTime >= spawnInterval)
        {
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
        var obstacle = asteroids[_rand.Next(asteroids.Count)];
        var position = RandomSpawnPosition();
        var clone = Instantiate(
            obstacle,
            position,
            Quaternion.identity);
        clone.velocity = currentObstacleVelocity;
    }

    private Vector3 RandomSpawnPosition()
    {
        var x = _rand.Next((int) -spawnBounds.x, (int) spawnBounds.x);
        var y = _rand.Next((int) -spawnBounds.y, (int) spawnBounds.y);
        
        return new Vector3(
            x, y, transform.position.z);
    }
}
