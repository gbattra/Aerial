using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public Player player;
    public GameTimer gameTimer;
    public LevelManager levelManager;
    public GameObject gameOverMenuCanvas;

    private bool isOpen;

    // Update is called once per frame
    void Update()
    {
        if (levelManager.gameOver && !isOpen)
        {
            isOpen = true;
            gameTimer.timer.Stop();
            Time.timeScale = 0;
            gameOverMenuCanvas.SetActive(true);   
        }
    }
}
