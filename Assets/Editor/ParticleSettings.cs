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

    [SerializeField] Material Sand;
    [SerializeField] Material Water;
    [SerializeField] Material Gas;

    public enum ParticlePresets
    {
        Sand,
        Water,
        Gas
    }

    private ParticlePresets selectedPreset;

    [MenuItem("Window/Particles/Particle Settings")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ParticleSettings));
    }

    private void OnGUI()
    {
        prefabToEdit = EditorGUILayout.ObjectField("Prefab to Edit", prefabToEdit, typeof(GameObject), false) as GameObject;
        selectedPreset = (ParticlePresets)EditorGUILayout.EnumPopup("Select Preset", selectedPreset);
        Gravity = EditorGUILayout.FloatField("Custom Gravity Value", Gravity);
        Friction = EditorGUILayout.FloatField("Custom Friction Value", Friction);
        ParticleColour = EditorGUILayout.ColorField("Select Color", ParticleColour);


        if (GUILayout.Button("Apply Custom Values"))
        {
            Material CustomMaterial = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit"));
            CustomMaterial.color = ParticleColour;
            prefabToEdit.GetComponent<Renderer>().sharedMaterial = CustomMaterial;
            prefabToEdit.GetComponent<Rigidbody2D>().gravityScale = Gravity;
            prefabToEdit.GetComponent<BoxCollider2D>().sharedMaterial.friction = Friction;
            prefabToEdit.tag = "Custom";
        }


            if (GUILayout.Button("Apply Preset"))
        {
            switch (selectedPreset)
            {
                case ParticlePresets.Sand:
                    prefabToEdit.GetComponent<Renderer>().sharedMaterial = Sand;
                    prefabToEdit.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    prefabToEdit.GetComponent<BoxCollider2D>().sharedMaterial.friction = 0.7f;
                    prefabToEdit.tag = "Sand";
                    break;
                case ParticlePresets.Water:
                    prefabToEdit.GetComponent<Renderer>().sharedMaterial = Water;
                    prefabToEdit.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    prefabToEdit.GetComponent<BoxCollider2D>().sharedMaterial.friction = 0.1f;
                    prefabToEdit.tag = "Water";
                    break;
                case ParticlePresets.Gas:
                    prefabToEdit.GetComponent<Renderer>().sharedMaterial = Gas;
                    prefabToEdit.GetComponent<Rigidbody2D>().gravityScale = -1f;
                    prefabToEdit.GetComponent<BoxCollider2D>().sharedMaterial.friction = 0.01f;
                    prefabToEdit.tag = "Gas";
                    break;
                default:
                    break;
            }
        }
    }
}
