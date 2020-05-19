using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Obstacle
{
    public void OnParticleCollision(GameObject other)
    {
        player.score += points;
        Destroy(gameObject);
    }
}
