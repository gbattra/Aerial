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

    public float spawnTimeInterval;
    public float lastSpawnTime;
    public float spawnDecay;
    public int spawnCount;
    public float bigCountdown;
    public float currentBigTime;
    public int randomSeed;
    public float obstacleVelocity;
    public float roundEndInterval;
    public float velocityGain;
    public bool shouldSpawn => Time.time > 10f && Time.time - lastSpawnTime > spawnTimeInterval;

    public Random rand;

    private float initialSpawnTimeInterval;

    public void Awake()
    {
        initialSpawnTimeInterval = spawnTimeInterval;
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
            lastSpawnTime = Time.time;
        }

        if (currentBigTime >= bigCountdown)
        {
            spawnTimeInterval -= spawnDecay;
            currentBigTime = 0;
            obstacleVelocity *= 1 + velocityGain;
        }

        if (spawnTimeInterval < roundEndInterval)
            spawnTimeInterval = initialSpawnTimeInterval;
    }

    private void SpawnObstacle()
    {
        Debug.Log("Spawning");
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
        var z = rand.Next((int) -spawnBounds.z, (int) spawnBounds.z);
        
        return new Vector3(
            transform.position.x + x,
            transform.position.y + y,
            transform.position.z + z);
    }
}
