using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float velocity;
    public float velocityFactor;
    public Player player;
    public Rigidbody rigidbody;

    private const float BUFFER = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = Vector3.back * (velocity * velocityFactor);
        if (transform.position.z < player.transform.position.z + BUFFER)
            Destroy(gameObject);
    }
}
