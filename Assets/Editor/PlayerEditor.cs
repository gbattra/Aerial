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
        EditorGUILayout.LabelField("Vehicle");
        EditorGUILayout.FloatField("Velocity", player.rigidbody.velocity.magnitude);
        Repaint();
    }
}