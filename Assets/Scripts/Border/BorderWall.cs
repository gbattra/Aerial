using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWall : MonoBehaviour
{
    public Collider collider;
    
    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
            Physics.IgnoreCollision(other.collider, collider);
    }
}
