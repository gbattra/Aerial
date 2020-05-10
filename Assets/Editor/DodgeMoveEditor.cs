using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DodgeMove))]
public class DodgeMoveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var dodgeMove = (DodgeMove) target;
        EditorGUILayout.LabelField("Editor");
        EditorGUILayout.Toggle("Is Dodging", dodgeMove.isDodging);
        Repaint();
    }
}
