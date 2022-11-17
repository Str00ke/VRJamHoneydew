using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MouseEditor))]
public class MouseEditorEditor : Editor
{
    SerializedProperty refresh;


    void OnEnable()
    {
        refresh = serializedObject.FindProperty("refresh");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();

        if (GUILayout.Button("Refresh"))
            refresh.boolValue = true;

        serializedObject.ApplyModifiedProperties();
    }
}
