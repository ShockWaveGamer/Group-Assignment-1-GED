using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGeoMaker : EditorWindow
{
    [MenuItem("Window/Particles/Level Geometry Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelGeoMaker));
    }

    Vector2 geoPos = new Vector2();
    
    int geoNumOfVert;
    Vector2[] geoVertexPos = new Vector2[9];

    Material lineMaterial;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Position: ");
        geoPos = EditorGUILayout.Vector2Field("", geoPos);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Number of Vertecies: ");
        geoNumOfVert = EditorGUILayout.IntSlider(geoNumOfVert, 3, geoVertexPos.Length);
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < geoNumOfVert; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label($"     Vertex {i + 1} Pos: ");
            geoVertexPos[i] = EditorGUILayout.Vector2Field("", geoVertexPos[i]);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label($"Line Material: ");
        lineMaterial = (Material)EditorGUILayout.ObjectField(lineMaterial, typeof(Material));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Polygon"))
        {
            CreatePolygon();
        }
        
        if (GUILayout.Button("Clear Polygons"))
        {
            ClearPolygons();
        }
    }

    private void ClearPolygons()
    {
        GameObject[] polygons = GameObject.FindGameObjectsWithTag("Level Geometry");
        foreach (GameObject polygon in polygons) { DestroyImmediate(polygon); }
    }

    private void CreatePolygon()
    {
        GameObject newPolygon = new GameObject();
        PolygonCollider2D collider = newPolygon.AddComponent<PolygonCollider2D>();
        LineRenderer lineRenderer = newPolygon.AddComponent<LineRenderer>();

        Vector2[] vertecies = new Vector2[geoNumOfVert];

        lineRenderer.positionCount = geoNumOfVert;

        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.material = lineMaterial;

        for (int i = 0;i < geoNumOfVert;i++) 
        {
            vertecies[i] = geoVertexPos[i];
            lineRenderer.SetPosition(i, geoVertexPos[i]);
        }

        collider.points = vertecies;
        newPolygon.transform.position = geoPos;
        newPolygon.tag = "Level Geometry";
        newPolygon.name = "New Polygon";
    }
}
