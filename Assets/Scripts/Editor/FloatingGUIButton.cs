using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MeshRenderer))]
public class FloatingGUIButton : Editor
{
    private MeshRenderer _target;
    private float buttonSize = 50f;

    private void OnEnable()
    {
        _target = (MeshRenderer)target;
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();

        Vector2 screenPos = Camera.current.WorldToScreenPoint(_target.transform.position);

        screenPos.y = Camera.current.pixelHeight - screenPos.y;

        Vector2 forward = Camera.current.WorldToScreenPoint(_target.transform.position + Vector3.forward);
        Vector2 back = Camera.current.WorldToScreenPoint(_target.transform.position + Vector3.back);
        Vector2 left = Camera.current.WorldToScreenPoint(_target.transform.position + Vector3.left);
        Vector2 right = Camera.current.WorldToScreenPoint(_target.transform.position + Vector3.right);

        forward.y = Camera.current.pixelHeight - forward.y;
        back.y = Camera.current.pixelHeight - back.y;
        left.y = Camera.current.pixelHeight - left.y;
        right.y = Camera.current.pixelHeight - right.y;

        GameObject newObject = null;

        if (GUI.Button(new Rect(screenPos.x + 200, screenPos.y - 25, buttonSize, buttonSize), "+90º"))
        {
            _target.transform.Rotate(0f, 90f, 0, Space.World);
        }

        if (GUI.Button(new Rect(screenPos.x - 250, screenPos.y - 25, buttonSize, buttonSize), "-90º"))
        {
            _target.transform.Rotate(0f, -90f, 0, Space.World);
        }

        if (GUI.Button(new Rect(forward.x - buttonSize / 2, forward.y - buttonSize / 2, buttonSize, buttonSize), "^"))
        {
            newObject = Instantiate(_target.gameObject, _target.transform.position + Vector3.forward, Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            Transform parent = _target.transform.parent;
            if (parent != null)
            {
                newObject.transform.parent = parent;
            }
        }
        if (GUI.Button(new Rect(back.x - buttonSize / 2, back.y - buttonSize / 2, buttonSize, buttonSize), "v"))
        {
            newObject = Instantiate(_target.gameObject, _target.transform.position + Vector3.back, Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            Transform parent = _target.transform.parent;
            if (parent != null)
            {
                newObject.transform.parent = parent;
            }
        }
        if (GUI.Button(new Rect(left.x - buttonSize / 2, left.y - buttonSize / 2, buttonSize, buttonSize), "<"))
        {
            newObject = Instantiate(_target.gameObject, _target.transform.position + Vector3.left, Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            Transform parent = _target.transform.parent;
            if (parent != null)
            {
                newObject.transform.parent = parent;
            }

        }
        if (GUI.Button(new Rect(right.x - buttonSize / 2, right.y - buttonSize / 2, buttonSize, buttonSize), ">"))
        {
            newObject = Instantiate(_target.gameObject, _target.transform.position + Vector3.right, Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(newObject, "Cloned");
            Transform parent = _target.transform.parent;
            if (parent != null)
            {
                newObject.transform.parent = parent;
            }
        }

        if (newObject != null)
        {
            newObject.name = _target.gameObject.name;
            Selection.activeGameObject = newObject;
        }

        Handles.EndGUI();
    }
}
