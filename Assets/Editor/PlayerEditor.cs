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
        EditorGUILayout.FloatField("LeftStickHorizontal", player.Controller.LeftStickHorizontal);
        EditorGUILayout.FloatField("LeftStickVertical", player.Controller.LeftStickVertical);
        EditorGUILayout.FloatField("RightStickHorizontal", player.Controller.RightStickHorizontal);
        EditorGUILayout.FloatField("RightStickVertical", player.Controller.RightStickVertical);
        EditorGUILayout.FloatField("LeftTrigger", player.Controller.LeftTrigger);
        EditorGUILayout.FloatField("RightTrigger", player.Controller.RightTrigger);
        EditorGUILayout.Toggle("LeftBumper", player.Controller.LeftBumper);
        EditorGUILayout.Toggle("RightBumper", player.Controller.RightBumper);
        EditorGUILayout.Toggle("A", player.Controller.A);
        EditorGUILayout.Toggle("B", player.Controller.B);
        EditorGUILayout.Toggle("Y", player.Controller.Y);
        EditorGUILayout.Toggle("X", player.Controller.X);
        Repaint();
    }
}