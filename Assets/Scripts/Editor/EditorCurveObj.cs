using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurveObjToSpline))]
public class EditorCurveObj : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CurveObjToSpline curveObjToSpline = target as CurveObjToSpline;

        if (GUILayout.Button("Calculate"))
            curveObjToSpline.UpdateSpline();
    }
}
