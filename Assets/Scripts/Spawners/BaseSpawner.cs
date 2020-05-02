using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public float respawnFrequency;
    public float spawnDistanceZ = 1300f;
    public bool shouldSpawn;
    public Vector3 screenBounds;
    public Player player;
    public List<Asteroid> asteroids;
    
    
}
