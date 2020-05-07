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
    public bool a => Input.GetButtonDown("A");
    public bool b => Input.GetButtonDown("B");
    public bool y => Input.GetButtonDown("Y");
    public bool x => Input.GetButtonDown("X");
    
    public bool braking => leftTrigger < 0;
}