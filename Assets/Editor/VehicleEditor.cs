using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Vehicle))]
public class VehicleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var vehicle = (Vehicle) target;
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.FloatField("Forward Speed", vehicle.forwardSpeed);
        EditorGUILayout.FloatField("Velocity", vehicle.rigidbody.velocity.magnitude);
        Repaint();
    }
}