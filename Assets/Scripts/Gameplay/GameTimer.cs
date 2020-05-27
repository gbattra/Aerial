using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public Stopwatch timer => _timer;
    private Stopwatch _timer = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        _timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
