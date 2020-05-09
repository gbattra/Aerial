
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Border))]
public class BorderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var border = (Border) target;
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.Toggle("Too Far Left", border.tooFarLeft);
        EditorGUILayout.Toggle("Too Far Right", border.tooFarRight);
        EditorGUILayout.Toggle("Too Far Up", border.tooFarUp);
        EditorGUILayout.Toggle("Too Far Down", border.tooFarDown);
        EditorGUILayout.Toggle("Out Of Bounds", border.playerOutOfBounds);
        Repaint();
    }
}
