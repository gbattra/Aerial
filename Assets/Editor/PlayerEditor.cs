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
        EditorGUILayout.BeginFoldoutHeaderGroup(true, "Controller");
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.FloatField("LeftStickHorizontal", Controller.leftStickHorizontal);
        EditorGUILayout.FloatField("LeftStickVertical", Controller.leftStickVertical);
        EditorGUILayout.FloatField("RightStickHorizontal", Controller.rightStickHorizontal);
        EditorGUILayout.FloatField("RightStickVertical", Controller.rightStickVertical);
        EditorGUILayout.FloatField("LeftTrigger", Controller.leftTrigger);
        EditorGUILayout.FloatField("RightTrigger", Controller.rightTrigger);
        EditorGUILayout.Toggle("LeftBumper", Controller.leftBumper);
        EditorGUILayout.Toggle("RightBumper", Controller.rightBumper);
        EditorGUILayout.Toggle("A", Controller.a);
        EditorGUILayout.Toggle("B", Controller.b);
        EditorGUILayout.Toggle("Y", Controller.y);
        EditorGUILayout.Toggle("X", Controller.x);
        EditorGUILayout.Toggle("Braking", Controller.braking);
        EditorGUILayout.Toggle("No Inputs", Controller.noInputs);
        EditorGUILayout.EndFoldoutHeaderGroup();
        Repaint();
    }
}