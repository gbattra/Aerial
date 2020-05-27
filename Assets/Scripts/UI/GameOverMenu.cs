using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Player player;
    public GameTimer gameTimer;
    public LevelManager levelManager;
    public GameObject gameOverMenuCanvas;

    private bool isOpen;

    // Update is called once per frame
    public void Update()
    {
        if (levelManager.gameOver && !isOpen)
        {
            isOpen = true;
            gameOverMenuCanvas.SetActive(true);   
        }
        
        if (isOpen && Controller.a)
            Restart();
        if (isOpen && Controller.b)
            Exit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
