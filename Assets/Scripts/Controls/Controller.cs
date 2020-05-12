using UnityEngine;

public static class Controller
{
    public static float leftStickHorizontal => Input.GetAxis("LeftStickHorizontal");
    public static float leftStickVertical => Input.GetAxis("LeftStickVertical");
    public static float rightStickHorizontal => Input.GetAxis("RightStickHorizontal");
    public static float rightStickVertical => Input.GetAxis("RightStickVertical");
    public static float leftTrigger => Input.GetAxis("LeftTrigger");
    public static float rightTrigger => Input.GetAxis("RightTrigger");
    
    public static bool leftBumper => Input.GetButton("LeftBumper");
    public static bool rightBumper => Input.GetButton("RightBumper");
    public static bool a => Input.GetButtonDown("A");
    public static bool b => Input.GetButton("B");
    public static bool y => Input.GetButtonDown("Y");
    public static bool x => Input.GetButtonDown("X");
    
    public static bool braking => leftTrigger < 0f;

    public static bool noInputs =>
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