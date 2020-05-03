using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Controller))]
public class ControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var controller = (Controller) target;
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.FloatField("LeftStickHorizontal", controller.leftStickHorizontal);
        EditorGUILayout.FloatField("LeftStickVertical", controller.leftStickVertical);
        EditorGUILayout.FloatField("RightStickHorizontal", controller.rightStickHorizontal);
        EditorGUILayout.FloatField("RightStickVertical", controller.rightStickVertical);
        EditorGUILayout.FloatField("LeftTrigger", controller.leftTrigger);
        EditorGUILayout.FloatField("RightTrigger", controller.rightTrigger);
        EditorGUILayout.Toggle("LeftBumper", controller.leftBumper);
        EditorGUILayout.Toggle("RightBumper", controller.rightBumper);
        EditorGUILayout.Toggle("A", controller.a);
        EditorGUILayout.Toggle("B", controller.b);
        EditorGUILayout.Toggle("Y", controller.y);
        EditorGUILayout.Toggle("X", controller.x);
        Repaint();
    }
}