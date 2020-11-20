using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConstructionToolWindow : EditorWindow
{
    private GameObject selectedObject = null;
    private GameObject activeGameObject = null;
    private bool _refresh = false;


    [MenuItem("Window/Construction Tool")]
    public static void ShowWindow()
    {
        var win = GetWindow<ConstructionToolWindow>();
        win.titleContent= new GUIContent("Construction Tool");
        win.Show();
    }

    Vector2 _scroll;

    private void Update()
    {
        activeGameObject = Selection.activeGameObject;
    }

    private void OnGUI()
    {
        Undo.RecordObject(this, "Undo");

        if (selectedObject == null)
        {
            EditorGUILayout.HelpBox("Please select an item", MessageType.Error);
            GUI.enabled = false;
        }

        if (selectedObject == null) GUI.enabled = false;
        if (GUILayout.Button("Spawn"))
        {
            var newObject = Instantiate(selectedObject);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            if (activeGameObject != null)
            {
                newObject.transform.position = activeGameObject.transform.position;
                newObject.transform.rotation = activeGameObject.transform.rotation;
                Transform parent = activeGameObject.transform.parent;
                if (parent != null)
                {
                    newObject.transform.parent = parent;
                }
                Selection.activeGameObject = newObject;
            }
        }

        if (activeGameObject == null) GUI.enabled = false;
        if (GUILayout.Button("Clone"))
        {
            var newObject = Instantiate(activeGameObject);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            newObject.transform.position = activeGameObject.transform.position;
            newObject.transform.rotation = activeGameObject.transform.rotation;
            Transform parent = activeGameObject.transform.parent;
            if (parent != null)
            {
                newObject.transform.parent = parent;
            }
            Selection.activeGameObject = newObject;
        }

        if (selectedObject == null) GUI.enabled = false;
        if (GUILayout.Button("Replace"))
        {
            var newObject = Instantiate(selectedObject);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            newObject.transform.position = activeGameObject.transform.position;
            newObject.transform.rotation = activeGameObject.transform.rotation;
            Transform parent = activeGameObject.transform.parent;
            if (parent != null)
            {            
                newObject.transform.parent = parent;
            }
            Selection.activeGameObject = newObject;
            DestroyImmediate(activeGameObject);
        }

        GUI.enabled = true;

        if (GUILayout.Button("Refresh"))
        {
            _refresh = !_refresh;
        }

        if (_refresh)
            AssetsRefresh();
    }

    void AssetsRefresh()
    {
        string[] assetPath = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets" });

        for (int i = 0; i < assetPath.Length; i++)
        {
            assetPath[i] = AssetDatabase.GUIDToAssetPath(assetPath[i]);
        }

        GameObject[] assets = new GameObject[assetPath.Length];
        for (int i = 0; i < assetPath.Length; i++)
        {
            assets[i] = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath[i]);
        }

        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        EditorGUILayout.BeginHorizontal();
        foreach (GameObject gameObject in assets)
        {
            GUI.color = (gameObject == selectedObject) ? Color.green : Color.white;

            GUIContent content = new GUIContent(AssetPreview.GetAssetPreview(gameObject));
            if (GUILayout.Button(content))
            {
                selectedObject = gameObject;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
    }
}
