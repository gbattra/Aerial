using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BaseSpawner : MonoBehaviour
{
    public float respawnFrequency;
    public Player player;
    public List<Obstacle> obstacles;
    public float distanceFromPlayer;

    public int obstacleScaleMin;
    public int obstacleScaleMax;

    public Vector3 spawnBounds;

    public float spawnDistanceInterval;
    public float lastSpawnLocation;
    public float spawnDecay;
    public int spawnCount;
    public float bigCountdown;
    public float currentBigTime;
    public int randomSeed;
    public float obstacleVelocity;

    public bool shouldSpawn => transform.position.z - lastSpawnLocation > spawnDistanceInterval;

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
        
        currentBigTime += Time.deltaTime;

        if (shouldSpawn)
        {
            for (var i = 0; i < spawnCount; i++)
                SpawnObstacle();
            lastSpawnLocation = transform.position.z;
        }

        if (currentBigTime >= bigCountdown)
        {
            spawnDistanceInterval -= spawnDecay;
            currentBigTime = 0;
        }
    }

    private void SpawnObstacle()
    {
        var obstacle = obstacles[rand.Next(obstacles.Count)];
        var position = RandomSpawnPosition();
        var clone = Instantiate(
            obstacle,
            position,
            Quaternion.identity);
        clone.transform.localScale *= rand.Next(obstacleScaleMin, obstacleScaleMax);
        clone.player = player;
        clone.velocity = obstacleVelocity;
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
