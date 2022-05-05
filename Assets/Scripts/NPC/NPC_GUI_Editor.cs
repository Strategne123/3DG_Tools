using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(NPC_Animation))]
public class NPC_GUI_Editor : Editor
{
    public override void OnInspectorGUI() //переименование элементов массива
    {
        EditorGUI.indentLevel = 0;
        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();

        SerializedProperty sceneNames = this.serializedObject.FindProperty("animation_mas");
        EditorGUILayout.PropertyField(sceneNames.FindPropertyRelative("Array.size"));
        for (int i = 0; i < sceneNames.arraySize; i++)
        {
            EditorGUILayout.PropertyField(sceneNames.GetArrayElementAtIndex(i), new GUIContent(((Animations)i).ToString()));
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}

