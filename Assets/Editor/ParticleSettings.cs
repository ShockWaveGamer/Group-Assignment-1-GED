using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using Codice.CM.Client.Differences;
using System.IO;
using System;
using System.Runtime.InteropServices;

public class ParticleSettings : EditorWindow
{
    private GameObject prefabToEdit;
    public Color ParticleColour;
    public float Gravity;
    public float Friction;

    public UnityEngine.Object statFile;

    private ElementTypes selectedPreset;

    [MenuItem("Window/Particles/Particle Settings")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ParticleSettings));
    }

    private void OnGUI()
    {
        selectedPreset = (ElementTypes)EditorGUILayout.EnumPopup("Select Preset", selectedPreset);
        if (selectedPreset != ElementTypes.Custom)
        {
            if (GUILayout.Button("Apply Preset"))
            {
                FindObjectOfType<ParticleSpawner>().SetCurrentElement(
                    FindObjectOfType<ElementalManagementSystem>().GetElementData(selectedPreset)
                    );
            }
        }
        else
        {
            ParticleColour = EditorGUILayout.ColorField("Select Color", ParticleColour);
            Gravity = EditorGUILayout.FloatField("Custom Gravity Value", Gravity);
            Friction = EditorGUILayout.FloatField("Custom Friction Value", Friction);

            if (GUILayout.Button("Apply Custom Values"))
                FindObjectOfType<ParticleSpawner>().SetCurrentElement(ElementTypes.Custom, ParticleColour, Gravity, Friction);
        }

        EditorGUILayout.Space();

        statFile = EditorGUILayout.ObjectField("Stat File Path", statFile, typeof(UnityEngine.Object));
        
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Clear Particles"))
            {
                if (statFile != null)
                {
                    string saveFilePath = AssetDatabase.GetAssetPath(statFile);
                    Debug.Log("Saved to: " + saveFilePath);

                    //FindObjectOfType<Logger>().SLNewParticleEntry("Hi");
                }

                ObjectPool objectPool = FindObjectOfType<ObjectPool>();
                foreach (Particle particle in FindObjectsOfType<Particle>())
                {
                    if (objectPool.enabled)
                    {
                        objectPool.RemoveObj(particle.gameObject);
                    }
                    else if (particle.CompareTag("Particle"))
                    {
                        Destroy(particle.gameObject);
                    }
                }
            }
        }
    }
}
