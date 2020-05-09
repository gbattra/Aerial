using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RollEngine))]
public class RollEngineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var rollEngine = (RollEngine) target;
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.FloatField("Roll", rollEngine.roll);
        Repaint();
    }
}