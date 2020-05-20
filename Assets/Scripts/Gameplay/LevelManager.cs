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

    public bool clearedLevel;

    public int fogColorIndex;
    public List<Color> fogColors;

    public float percentProgress => spawners.Average(spawner => spawner.percentProgress);
    public int levelNumber { get; private set; }

    public Stopwatch timer => _timer;
    private Stopwatch _timer = new Stopwatch();

    public void Awake()
    {
        levelNumber = 1;
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
        var secondsLeft = secondsAtMax - (Time.time - countdownStartTime);
        if (!(secondsLeft <= 0)) return;
        Debug.Log("New Level Started");

        isCountingDown = false;
        levelNumber++;
        fogColorIndex = fogColorIndex < fogColors.Count - 1 ? fogColorIndex + 1 : 0;
        RenderSettings.fogColor = fogColors[fogColorIndex];
        foreach (var spawner in spawners)
        {
            spawner.ResetSpawner(
                startSpawnTimeInterval,
                decayAmount,
                decayInterval,
                finalSpawnTimeInterval,
                obstacleVelocity);
        }
    }
}
