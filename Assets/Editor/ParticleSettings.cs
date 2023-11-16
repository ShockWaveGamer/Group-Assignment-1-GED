using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class ParticleSettings : EditorWindow
{
    private GameObject prefabToEdit;
    public Color ParticleColour;
    public float Gravity;
    public float Friction;

    private ElementTypes selectedPreset;

    [MenuItem("Window/Particles/Particle Settings")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ParticleSettings));
    }

    private void OnGUI()
    {
        prefabToEdit = EditorGUILayout.ObjectField("Prefab to Edit", prefabToEdit, typeof(GameObject), false) as GameObject;

        EditorGUILayout.Space();

        selectedPreset = (ElementTypes)EditorGUILayout.EnumPopup("Select Preset", selectedPreset);
        if (selectedPreset != ElementTypes.Custom)
        {
            if (GUILayout.Button("Apply Preset"))
            {
                Element newElement;

                switch (selectedPreset)
                {
                    case ElementTypes.Sand:
                        newElement = FindObjectOfType<ElementalManagementSystem>().GetElementData(ElementTypes.Sand);
                        FindObjectOfType<ParticleSpawner>().SetCurrentElement(newElement);
                        break;
                    case ElementTypes.Water:
                        newElement = FindObjectOfType<ElementalManagementSystem>().GetElementData(ElementTypes.Water);
                        FindObjectOfType<ParticleSpawner>().SetCurrentElement(newElement);
                        break;
                    case ElementTypes.Gas:
                        newElement = FindObjectOfType<ElementalManagementSystem>().GetElementData(ElementTypes.Gas);
                        FindObjectOfType<ParticleSpawner>().SetCurrentElement(newElement);
                        break;
                    default:
                        break;
                }
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
    }
}
