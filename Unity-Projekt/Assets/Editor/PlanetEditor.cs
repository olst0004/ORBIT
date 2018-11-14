using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Planet _Planet = (Planet)target;
        _Planet.UpdateLineToGround();
    }
}
