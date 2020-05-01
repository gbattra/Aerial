using UnityEngine;

public class Controller
{
    public float LeftStickHorizontal => Input.GetAxis("LeftStickHorizontal");
    public float LeftStickVertical => Input.GetAxis("LeftStickVertical");
    public float RightStickHorizontal => Input.GetAxis("RightStickHorizontal");
    public float RightStickVertical => Input.GetAxis("RightStickVertical");
    public float LeftTrigger => Input.GetAxis("LeftTrigger");
    public float RightTrigger => Input.GetAxis("RightTrigger");
    //
    public bool LeftBumper => Input.GetButton("LeftBumper");
    public bool RightBumper => Input.GetButton("RightBumper");
    public bool A => Input.GetButton("A");
    public bool B => Input.GetButton("B");
    public bool Y => Input.GetButton("Y");
    public bool X => Input.GetButton("X");
}