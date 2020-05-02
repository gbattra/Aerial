using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var player = (Player) target;
        EditorGUILayout.LabelField("Controller Inputs");
        EditorGUILayout.FloatField("LeftStickHorizontal", player.controller.leftStickHorizontal);
        EditorGUILayout.FloatField("LeftStickVertical", player.controller.leftStickVertical);
        EditorGUILayout.FloatField("RightStickHorizontal", player.controller.rightStickHorizontal);
        EditorGUILayout.FloatField("RightStickVertical", player.controller.rightStickVertical);
        EditorGUILayout.FloatField("LeftTrigger", player.controller.leftTrigger);
        EditorGUILayout.FloatField("RightTrigger", player.controller.rightTrigger);
        EditorGUILayout.Toggle("LeftBumper", player.controller.leftBumper);
        EditorGUILayout.Toggle("RightBumper", player.controller.rightBumper);
        EditorGUILayout.Toggle("A", player.controller.a);
        EditorGUILayout.Toggle("B", player.controller.b);
        EditorGUILayout.Toggle("Y", player.controller.y);
        EditorGUILayout.Toggle("X", player.controller.x);
        
        EditorGUILayout.LabelField("Transform");
        EditorGUILayout.Vector3Field("Rotation", player.ComputeRotation());
        EditorGUILayout.Vector3Field("Force", player.ComputeForce());
        EditorGUILayout.FloatField("Velocity", player._rigidbody.velocity.magnitude);
        Repaint();
    }
}