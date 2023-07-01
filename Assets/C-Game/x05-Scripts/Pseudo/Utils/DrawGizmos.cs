using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StickToSurface))]
public class DrawGizmos : Editor
{
    #if UNITY_EDITOR
    void OnSceneGUI()
    {
        StickToSurface stickToSurface = (StickToSurface)target;
        Handles.color = Color.yellow;

        Handles.DrawWireArc(stickToSurface.centerPoint, Vector3.forward, Vector3.right, 360, stickToSurface.radius);
    }
    #endif
}