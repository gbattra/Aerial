﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Player player;
    public GameTimer gameTimer;
    public LevelManager levelManager;
    public GameObject gameOverMenuCanvas;
    public Text timeText;
    public Text scoreText;

    private bool isOpen;
    private float timeScale;

    public void Awake()
    {
        timeScale = Time.timeScale;
    }

    // Update is called once per frame
    public void Update()
    {
        if (levelManager.gameOver && !isOpen)
        {
            isOpen = true;
            timeText.text = $"{gameTimer.timer.Elapsed:m\\:ss}";
            scoreText.text = $"{player.score}";
            Time.timeScale = 0;
            gameOverMenuCanvas.SetActive(true);   
        }
        
        if (isOpen && Controller.a)
            Restart();
        if (isOpen && Controller.x)
            Exit();
    }

    public void Restart()
    {
        Time.timeScale = timeScale;
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Time.timeScale = timeScale;
        SceneManager.LoadScene("Menu");
    }
}
