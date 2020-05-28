﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public Vehicle vehicle;
    public GameTimer gameTimer;
    public List<BaseSpawner> obstacleSpawners;
    public List<BaseSpawner> virusSpawners;
    public List<BaseSpawner> ambienceSpawners;
    
    public float secondsAtMax;
    public bool isCountingDown { get; private set; }
    public float countdownSecondsRemaining { get; private set; }
    public float decayAmount;
    public float decayInterval;
    public float startSpawnTimeInterval;
    public float finalSpawnTimeInterval;
    public float obstacleVelocity;
    public float levelSpawnTimeDecay;
    public float levelVelocityIncrease;
    public float virusSpawnPad;
    public float secondsIncrease;
    public int fogColorIndex;
    public List<Color> fogColors;
    public float percentProgress => obstacleSpawners.Average(spawner => spawner.percentProgress);
    public int levelNumber { get; private set; }

    public bool gameOver { get; private set; }
    
    private int currentLevel;
    private bool spawnersMaxed => obstacleSpawners.Any() &&
                                  obstacleSpawners.All(spawner => spawner.isMaxed);
    private float countdownStartTime;
    
    public void Awake()
    {
        levelNumber = 1;
        foreach (var spawner in obstacleSpawners)
        {
            spawner.player = player;
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                finalSpawnTimeInterval,
                decayAmount,
                decayInterval,
                obstacleVelocity);
        }
        
        levelNumber = 1;
        foreach (var spawner in virusSpawners)
        {
            spawner.player = player;
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                finalSpawnTimeInterval + virusSpawnPad,
                decayAmount,
                decayInterval,
                obstacleVelocity);
        }

        foreach (var spawner in ambienceSpawners)
        {
            spawner.player = player;
        }
    }

    public void Start()
    {
        RenderSettings.fogColor = fogColors[fogColorIndex];
    }

    public void Update()
    {
        gameOver = !(vehicle.health > 0);
        
        if (gameOver)
        {
            gameTimer.timer.Stop();
            return;
        }
        
        if (!spawnersMaxed) return;
        
        if (spawnersMaxed && !isCountingDown)
        {
            isCountingDown = true;
            countdownStartTime = Time.time;
        }
        countdownSecondsRemaining = secondsAtMax - (Time.time - countdownStartTime);
        if (!(countdownSecondsRemaining <= 0)) return;

        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obstacle);
        }
        
        isCountingDown = false;
        levelNumber++;
        fogColorIndex = fogColorIndex < fogColors.Count - 1 ? fogColorIndex + 1 : 0;
        RenderSettings.fogColor = fogColors[fogColorIndex];
        foreach (var spawner in obstacleSpawners)
        {
            startSpawnTimeInterval -= levelSpawnTimeDecay;
            finalSpawnTimeInterval -= levelSpawnTimeDecay;
            obstacleVelocity += levelVelocityIncrease;
            secondsAtMax += secondsIncrease;
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                finalSpawnTimeInterval,
                decayAmount,
                decayInterval,
                obstacleVelocity);
        }
        
        foreach (var spawner in virusSpawners)
        {
            startSpawnTimeInterval -= levelSpawnTimeDecay;
            finalSpawnTimeInterval -= levelSpawnTimeDecay;
            obstacleVelocity += levelVelocityIncrease;
            secondsAtMax += 5;
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                finalSpawnTimeInterval + virusSpawnPad,
                decayAmount,
                decayInterval,
                obstacleVelocity);
        }
    }
}
