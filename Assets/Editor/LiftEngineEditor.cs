using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LiftEngine))]
public class LiftEngineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var liftEngine = (LiftEngine) target;
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.FloatField("Lift", liftEngine.lift);
        Repaint();
    }
}