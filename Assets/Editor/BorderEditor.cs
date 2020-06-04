
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
        Repaint();
    }
}
