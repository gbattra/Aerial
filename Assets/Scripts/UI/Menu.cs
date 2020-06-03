using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonSelectAudioClip;

    public TextMeshProUGUI highScoreText;

    public void Awake()
    {
        highScoreText.text = $"HIGH SCORE: {PlayerPrefs.GetInt("high-score")}";
    }

    public void Update()
    {
        if (Controller.a)
            Play();
        if (Controller.b)
            Exit();
    }

    public void Play()
    {
        audioSource.PlayOneShot(buttonSelectAudioClip);
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        audioSource.PlayOneShot(buttonSelectAudioClip);
        Application.Quit();
    }
}
