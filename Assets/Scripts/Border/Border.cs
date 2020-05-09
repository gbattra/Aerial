using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    public float offset;
    
    public Player player;

    public bool tooFarLeft =>
        player.transform.position.x < left.transform.position.x;
    public bool tooFarRight =>
        player.transform.position.x > right.transform.position.x;
    public bool tooFarUp =>
        player.transform.position.y > top.transform.position.y;
    public bool tooFarDown =>
        player.transform.position.y < bottom.transform.position.y;

    public bool playerOutOfBounds => tooFarLeft || tooFarRight || tooFarUp || tooFarDown;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.transform.position.z + offset);
    }
}
