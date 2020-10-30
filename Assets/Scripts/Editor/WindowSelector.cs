using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WindowSelector : EditorWindow
{
    [MenuItem("Window/Prefab Selector")]
    public static void ShowWindow()
    {
        GetWindow<WindowSelector>().Show();
    }

    private void OnGUI()
    {
        var GUIDs = AssetDatabase.FindAssets("t:gameobject");
        foreach (var GUID in GUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(GUID);
            var tex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath<GameObject>(path));

            GUILayout.Label(new GUIContent(tex));
        }

    }
}

