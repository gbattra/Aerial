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
    public GameTimer gameTimer;
    public AudioSource audioSource;
    public AudioClip gameOverAudioClip;
    public AudioClip readySetAudioClip;
    public AudioClip goAudioClip;
    public AudioClip levelClearAudioClip;
    public AudioClip countdownStartAudioClip;
    public AudioClip finalCountdownAudioClip;
    
    public List<BaseSpawner> obstacleSpawners;
    public List<BaseSpawner> virusSpawners;
    public List<BaseSpawner> ambienceSpawners;
    
    public float secondsAtMax;
    public bool isCountingDown { get; private set; }
    public bool newHighScore;
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
    private float lastCountdownSoundTime;
    private float highScore;
    public bool hasHighScore;
    private bool playedGameOverSound;
    
    public void Awake()
    {
        highScore = PlayerPrefs.GetInt("high-score");
        hasHighScore = highScore > 0;
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
        if (player.score > highScore && hasHighScore)
            newHighScore = true;
        
        if ((countdownSecondsRemaining < 3 ||
             countdownSecondsRemaining < 2 ||
             countdownSecondsRemaining < 1) &&
            isCountingDown)
        {
            if (!(Time.time - lastCountdownSoundTime > 1)) return;
            
            audioSource.PlayOneShot(finalCountdownAudioClip);
            lastCountdownSoundTime = Time.time;
        }
        
        gameOver = !(vehicle.health > 0);
        
        if (gameOver)
        {
            gameTimer.timer.Stop();
            if (newHighScore)
                PlayerPrefs.SetInt("high-score", (int) player.score);
            if (playedGameOverSound) return;
            playedGameOverSound = true;
            audioSource.PlayOneShot(gameOverAudioClip);
            return;
        }
        
        if (!spawnersMaxed) return;
        
        if (spawnersMaxed && !isCountingDown)
        {
            audioSource.PlayOneShot(countdownStartAudioClip);
            isCountingDown = true;
            countdownStartTime = Time.time;
        }
        countdownSecondsRemaining = secondsAtMax - (Time.time - countdownStartTime);
        if (!(countdownSecondsRemaining <= 0)) return;

        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            var virus = obstacle.GetComponent<Virus>();
            if (virus != null)
                player.AddToScore(virus.points);
            Destroy(obstacle);
        }
        audioSource.PlayOneShot(levelClearAudioClip);
        isCountingDown = false;
        levelNumber++;
        fogColorIndex = fogColorIndex < fogColors.Count - 1 ? fogColorIndex + 1 : 0;
        RenderSettings.fogColor = fogColors[fogColorIndex];
        foreach (var spawner in obstacleSpawners)
        {
            startSpawnTimeInterval -= levelSpawnTimeDecay;
            finalSpawnTimeInterval -= levelSpawnTimeDecay;
            obstacleVelocity *= levelVelocityIncrease;
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
