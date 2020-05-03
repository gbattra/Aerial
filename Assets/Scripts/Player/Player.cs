using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Controller controller;
    public Vehicle vehicle;

    public void FixedUpdate()
    {
        transform.position = vehicle.transform.position;
        vehicle.HandleInputs(controller);
    }
}
