using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BoardCleanup : MonoBehaviour
{
    public static HashSet<GameObject> DestroyOnUpdate = new HashSet<GameObject>();

    static BoardCleanup()
    {
        EditorApplication.update += DestroyMarkedObjects;
    }

    // Update is called once per frame
    public static void DestroyMarkedObjects()
    {
        if (EditorApplication.isPlaying)
        {
            return;
        }

        foreach (GameObject go in DestroyOnUpdate)
        {
            DestroyImmediate(go);
        }
        DestroyOnUpdate.Clear();
    }
}
