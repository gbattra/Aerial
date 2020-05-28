using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameTimer gameTimer;
    
    private bool isOpen;
    private float timeScale;

    public void Awake()
    {
        timeScale = Time.timeScale;
    }

    public void Update()
    {
        if (Controller.start && !isOpen)
        {
            isOpen = true;
            gameTimer.timer.Stop();
            Time.timeScale = 0;
            menuCanvas.SetActive(true);   
        }

        if (!isOpen) return;
        
        if (Controller.a)
            Resume();
        if (Controller.b)
            Restart();
        if (Controller.x)
            Exit();
    }

    public void Resume()
    {
        isOpen = false;
        gameTimer.timer.Start();
        Time.timeScale = timeScale;
        menuCanvas.SetActive(false);
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
