using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public List<BaseSpawner> spawners;
    public float secondsAtMax;
    
    private int currentLevel;
    private bool spawnersMaxed => spawners.All(spawner => spawner.isMaxed);
    private float countdownStartTime;
    private bool isCountingDown;
    
    public float decayAmount;
    public float decayInterval;
    public float startSpawnTimeInterval;
    public float finalSpawnTimeInterval;
    public float obstacleVelocity;

    public void Awake()
    {
        foreach (var spawner in spawners)
        {
            spawner.player = player;
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                decayAmount,
                decayInterval,
                finalSpawnTimeInterval,
                obstacleVelocity);
        }
    }

    public void Update()
    {
        if (!spawnersMaxed)
            return;
        
        if (spawnersMaxed && !isCountingDown)
        {
            Debug.Log("Counting down");
            isCountingDown = true;
            countdownStartTime = Time.time;
        }

        var secondsLeft = secondsAtMax - (Time.time - countdownStartTime);
        if (!(secondsLeft <= 0)) return;
        isCountingDown = false;
        foreach (var spawner in spawners)
        {
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                decayAmount,
                decayInterval,
                finalSpawnTimeInterval,
                obstacleVelocity);
            Debug.Log("New Level Started");
        }
    }
}
