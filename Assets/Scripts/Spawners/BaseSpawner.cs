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

    public Random _rand;

    public void Awake()
    {
        _rand = new Random(randomSeed);
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

        if (currentSpawnTime >= spawnInterval)
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
        var obstacle = asteroids[_rand.Next(asteroids.Count)];
        var position = RandomSpawnPosition();
        var clone = Instantiate(
            obstacle,
            position,
            Quaternion.identity);
        clone.player = player;
    }

    private Vector3 RandomSpawnPosition()
    {
        var x = _rand.Next((int) -spawnBounds.x, (int) spawnBounds.x);
        var y = _rand.Next((int) -spawnBounds.y, (int) spawnBounds.y);
        
        return new Vector3(
            transform.position.x + x,
            transform.position.y + y,
            transform.position.z);
    }
}
