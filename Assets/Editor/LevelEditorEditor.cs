using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorEditor : Editor
{
    private SerializedProperty list;

    private SerializedProperty _selected;

    private SerializedProperty _levelName;

    private int indexSelect = 0;

    private static bool isEnable;

    void OnEnable()
    {
        list = serializedObject.FindProperty("enemiesList");

        _levelName = serializedObject.FindProperty("levelName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        if (isEnable)
        {
            if (GUILayout.Button("Disable"))
            {
                isEnable = false;
                FindObjectOfType<LevelEditor>().IO(isEnable);
            }
        }
        else
        {
            if (GUILayout.Button("Enable"))
            {
                isEnable = true;
                FindObjectOfType<LevelEditor>().IO(isEnable);
            }
            else
            {
                serializedObject.ApplyModifiedProperties();
                return;
            }
        }
        EditorGUILayout.Space(30);


        base.OnInspectorGUI();
       
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (i == indexSelect) GUI.color = Color.green;
            else GUI.color = new Color(0.6f, 0.6f, 0.6f, 1);
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            if (GUILayout.Button("Select"))
            {
                indexSelect = i;
                FindObjectOfType<LevelEditor>().ChangeEnemyToPlace(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(10);

        GUI.color = Color.red;
        if (GUILayout.Button("Restart"))
            FindObjectOfType<LevelEditor>().Restart();

        EditorGUILayout.Space(10);


        GUI.color = Color.blue;
        if (GUILayout.Button("Clear"))
            FindObjectOfType<LevelEditor>().Clear();

        EditorGUILayout.Space(30);

       
        if (_levelName.stringValue == "")
        {
            GUILayout.Button("Need a name to save!");
        }
        else
        {
            GUI.color = Color.green;
            if (GUILayout.Button("Save"))
                FindObjectOfType<LevelEditor>().SaveLevel();
        }
        

        serializedObject.ApplyModifiedProperties();
    }
}

