using System;
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
    public List<BaseSpawner> obstacleSpawners;
    public List<BaseSpawner> virusSpawners;
    public List<BaseSpawner> ambienceSpawners;
    
    public float secondsAtMax;
    
    private int currentLevel;
    private bool spawnersMaxed => obstacleSpawners.All(spawner => spawner.isMaxed);
    private float countdownStartTime;
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
    public bool clearedLevel;

    public int fogColorIndex;
    public List<Color> fogColors;

    public float percentProgress => obstacleSpawners.Average(spawner => spawner.percentProgress);
    public int levelNumber { get; private set; }

    public Stopwatch timer => _timer;
    private Stopwatch _timer = new Stopwatch();

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
    }

    public void Start()
    {
        _timer.Start();
        RenderSettings.fogColor = fogColors[fogColorIndex];
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
        countdownSecondsRemaining = secondsAtMax - (Time.time - countdownStartTime);
        if (!(countdownSecondsRemaining <= 0)) return;
        Debug.Log("New Level Started");

        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obstacle);
        }
        
        // vehicle.healthUpAbility.AddHealthUp(1);
        // vehicle.shieldAbility.AddShield(1);
        isCountingDown = false;
        levelNumber++;
        fogColorIndex = fogColorIndex < fogColors.Count - 1 ? fogColorIndex + 1 : 0;
        RenderSettings.fogColor = fogColors[fogColorIndex];
        foreach (var spawner in obstacleSpawners)
        {
            startSpawnTimeInterval -= levelSpawnTimeDecay;
            finalSpawnTimeInterval -= levelSpawnTimeDecay;
            obstacleVelocity += levelVelocityIncrease;
            secondsAtMax += 5;
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
