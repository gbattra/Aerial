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
    public bool b => Input.GetButton("B");
    public bool y => Input.GetButtonDown("Y");
    public bool x => Input.GetButtonDown("X");
    
    public bool braking => leftTrigger < 0f;

    public bool noInputs =>
        leftStickHorizontal == 0f &&
        leftStickVertical == 0f &&
        rightStickHorizontal == 0f &&
        rightStickVertical == 0f &&
        leftTrigger == 0f &&
        rightTrigger == 0f &&
        !leftBumper &&
        !rightBumper &&
        !a &&
        !b &&
        !y &&
        !x;
}