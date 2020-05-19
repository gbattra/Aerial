using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BaseSpawner : MonoBehaviour
{
    public Player player;
    public List<Obstacle> obstacles;
    public float distanceFromPlayer;
    public float obstacleScaleMin;
    public float obstacleScaleMax;
    public Vector3 spawnBounds;
    public int spawnCount;
    public int randomSeed;
    
    // difficulty settings
    public float decayAmount;
    public float decayInterval;
    public float startSpawnTimeInterval;
    public float finalSpawnTimeInterval;
    public float currentSpawnTimeInterval;
    public float obstacleVelocity;
    public bool isMaxed => currentSpawnTimeInterval <= finalSpawnTimeInterval;

    public float percentProgress =>
        (currentSpawnTimeInterval - startSpawnTimeInterval) / (finalSpawnTimeInterval - startSpawnTimeInterval);
    
    private bool shouldSpawn => Time.time - lastSpawnTime > currentSpawnTimeInterval;
    private float lastSpawnTime;
    private float currentTimeInterval;
    
    private Random rand;

    public void Awake()
    {
        currentSpawnTimeInterval = startSpawnTimeInterval;
        rand = new Random(randomSeed);
        SpawnObstacle();
    }

    public void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.transform.position.z + distanceFromPlayer);
        
        currentTimeInterval += Time.deltaTime;

        if (shouldSpawn)
        {
            for (var i = 0; i < spawnCount; i++)
                SpawnObstacle();
            lastSpawnTime = Time.time;
        }

        if (currentTimeInterval >= decayInterval && !isMaxed)
        {
            currentSpawnTimeInterval -= decayAmount;
            currentTimeInterval = 0;
        }
    }

    public void ResetSpawner(
        float _startSpawnTimeInterval,
        float _decayAmount,
        float _decayInterval,
        float _finalSpawnTimeInterval,
        float _obstacleVelocity)
    {
        decayAmount = _decayAmount;
        decayInterval = _decayInterval;
        finalSpawnTimeInterval = _finalSpawnTimeInterval;
        obstacleVelocity = _obstacleVelocity;
        startSpawnTimeInterval = _startSpawnTimeInterval;
        currentSpawnTimeInterval = startSpawnTimeInterval;
    }

    private void SpawnObstacle()
    {
        var obstacle = obstacles[rand.Next(obstacles.Count)];
        var position = RandomSpawnPosition();
        var clone = Instantiate(
            obstacle,
            position,
            Quaternion.identity);
        clone.transform.localScale *= (float) rand.NextDouble() *
                                      (obstacleScaleMax - obstacleScaleMin) + obstacleScaleMin;
        clone.player = player;
        clone.velocity = obstacleVelocity;
    }

    private Vector3 RandomSpawnPosition()
    {
        var x = rand.Next((int) -spawnBounds.x, (int) spawnBounds.x);
        var y = rand.Next((int) -spawnBounds.y, (int) spawnBounds.y);
        var z = rand.Next((int) -spawnBounds.z, (int) spawnBounds.z);
        
        return new Vector3(
            transform.position.x + x,
            transform.position.y + y,
            transform.position.z + z);
    }
}
