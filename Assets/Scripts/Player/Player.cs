using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float score;

    public void AddToScore(float points)
    {
        score += points;
    }
}
