using UnityEngine;

public class Controller : MonoBehaviour
{
    public float leftStickHorizontal => Input.GetAxis("LeftStickHorizontal");
    public float leftStickVertical => Input.GetAxis("LeftStickVertical");
    public float rightStickHorizontal => Input.GetAxis("RightStickHorizontal");
    public float rightStickVertical => Input.GetAxis("RightStickVertical");
    public float leftTrigger => Input.GetAxis("LeftTrigger");
    public float rightTrigger => Input.GetAxis("RightTrigger");
    
    public bool leftBumper => Input.GetButton("LeftBumper");
    public bool rightBumper => Input.GetButton("RightBumper");
    public bool a => Input.GetButton("A");
    public bool b => Input.GetButton("B");
    public bool y => Input.GetButton("Y");
    public bool x => Input.GetButton("X");

    public float Roll => leftTrigger < 0 ? leftStickHorizontal : 0;
    public float Yaw => leftTrigger < 0 ? 0 : leftStickHorizontal;
    public bool accelerating => rightTrigger < 0;
}