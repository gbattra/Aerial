using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float velocity;
    public Rigidbody rigidbody;

    private const float BUFFER = 200f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = Vector3.back * velocity;
        if (transform.position.z < -BUFFER)
            Destroy(gameObject);
    }
}
