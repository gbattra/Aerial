using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameTimer gameTimer;
    private float timeScale;

    public void Awake()
    {
        timeScale = Time.timeScale;
    }

    public void Update()
    {
        if (Controller.start)
        {
            gameTimer.timer.Stop();
            Time.timeScale = 0;
            menuCanvas.SetActive(true);   
        }
    }

    public void Resume()
    {
        gameTimer.timer.Start();
        Time.timeScale = timeScale;
        menuCanvas.SetActive(false);
    }

    public void Restart()
    {
        gameTimer.timer.Start();
        Time.timeScale = timeScale;
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        gameTimer.timer.Start();
        Time.timeScale = timeScale;
        SceneManager.LoadScene("Menu");
    }
}
