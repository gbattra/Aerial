using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ThrustEngine))]
public class ThrustEngineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var thrustEngine = (ThrustEngine) target;
        EditorGUILayout.LabelField("Forces");
        EditorGUILayout.FloatField("Thrust", thrustEngine.thrust);
        Repaint();
    }
}