using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class Gridscript : MonoBehaviour
{
    public bool gridLock = true;

    void Update()
    {
        if (Application.isPlaying) return;
        if (gridLock)
        {
            foreach (Transform child in transform)
            {
                Vector3 childPosition = child.localPosition;
                childPosition.x = (int)childPosition.x;
                childPosition.y = (int)childPosition.y;
                childPosition.z = (int)childPosition.z;
                child.localPosition = childPosition;

                //No sirve pero lo dejo igual como archive
                /*Quaternion toRotation = Quaternion.LookRotation(child.localPosition);
                Vector3 childRotation = toRotation.eulerAngles;
                childRotation.x = Mathf.Round(childRotation.x / 45.0f) * 45.0f;
                childRotation.y = Mathf.Round(childRotation.y / 45.0f) * 45.0f;
                childRotation.z = Mathf.Round(childRotation.z / 45.0f) * 45.0f;
                child.transform.eulerAngles = childRotation;*/
                
            }
        }
    }
}
