using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceObjectWindow : EditorWindow
{
    [MenuItem("Window/Replace Tool")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceObjectWindow>().Show();
    }

    GameObject gameObject;
    GameObject prefab;

    private void OnGUI()
    {
        gameObject = (GameObject)EditorGUILayout.ObjectField("Game Object To Replace", gameObject, typeof(GameObject), true);
        prefab = (GameObject)EditorGUILayout.ObjectField("New Game Object", prefab, typeof(GameObject), false);

        if (gameObject == null)
        {
            EditorGUILayout.HelpBox("Please select a gameobject in scene", MessageType.Error);
            GUI.enabled = false;
        }

        if (prefab == null)
        {
            EditorGUILayout.HelpBox("Please select a prefab", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Replace"))
        {
            var newObject = Instantiate(prefab);
            newObject.transform.position = gameObject.transform.position;
            newObject.transform.rotation = gameObject.transform.rotation;
            DestroyImmediate(gameObject);
        }
    }
}
