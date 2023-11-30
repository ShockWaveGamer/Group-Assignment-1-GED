using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEditor.Rendering.FilterWindow;

public class ElementalManagementSystem : Singleton<ElementalManagementSystem>
{
    [SerializeField] public Element[] elements;
    [SerializeField] public ElementalCombination[] elementalCombinations;

    private Dictionary<ElementTypes, Element> elementsDictionary = new Dictionary<ElementTypes, Element>();
    private Dictionary<ElementalCombo, ElementTypes> elementalCombinationsDictionary = new Dictionary<ElementalCombo, ElementTypes>();

    AchievementManager achievementManager;

    private void Awake()
    {
        elementsDictionary.Clear();
        foreach (Element element in elements) 
        {
            elementsDictionary.Add(element.type, element);
        }

        elementalCombinationsDictionary.Clear();
        foreach (ElementalCombination elementalCombination in elementalCombinations)
        {
            ElementalCombo newKey = new ElementalCombo(elementalCombination.elementA, elementalCombination.elementB);

            if (elementalCombinationsDictionary.ContainsKey(newKey)) 
                Debug.LogError($"() already Exists at {elementalCombinationsDictionary.Count + 1}!");

            elementalCombinationsDictionary.Add(newKey, elementalCombination.elementResult);
        }

        achievementManager = GetComponent<AchievementManager>();
    }

    public Element GetElementData(ElementTypes type) 
    {
        if (Application.isEditor)
        {
            elementsDictionary.Clear();
            foreach (Element element in elements)
            {
                elementsDictionary.Add(element.type, element);
            }
        }

        return elementsDictionary[type];
    }

    public bool CheckElementalCombination(ElementTypes a, ElementTypes b)
    {
        if (Application.isEditor)
        {
            if (elementsDictionary.Count! > 0)
            {
                elementsDictionary.Clear();
                foreach (Element element in elements)
                {
                    elementsDictionary.Add(element.type, element);
                }
            }

            elementalCombinationsDictionary.Clear();
            foreach (ElementalCombination elementalCombination in elementalCombinations)
            {
                ElementalCombo elementalCombo = new ElementalCombo(elementalCombination.elementA, elementalCombination.elementB);
                if (!elementalCombinationsDictionary.ContainsKey(elementalCombo))
                    elementalCombinationsDictionary.Add(elementalCombo, elementalCombination.elementResult);
            }
        }

        return elementalCombinationsDictionary.ContainsKey(new ElementalCombo(a, b));
    }

    public ElementTypes GetElementalCombinationResult(ElementTypes a, ElementTypes b)
    {
        if (!CheckElementalCombination(a, b)) return a;
        return elementalCombinationsDictionary[new ElementalCombo(a, b)];
    }

    public void UpdateParticleElements(Particle a, Particle b)
    {
        if (a.element == ElementTypes.Custom || b.element == ElementTypes.Custom) return;

        ElementTypes aType = a.element;
        ElementTypes bType = b.element;

        a.UpdateParticleElement(GetElementalCombinationResult(aType, bType));
        b.UpdateParticleElement(GetElementalCombinationResult(bType, aType));

        achievementManager.AddParticle(aType.ToString());
        achievementManager.AddParticle(bType.ToString());
    }
}

[Serializable] public class Element
{
    public ElementTypes type;
    [Space]
    public Color color;
    public float gravity, friction;
}

internal struct ElementalCombo
{
    public ElementTypes elementA;
    public ElementTypes elementB;

    public ElementalCombo(ElementTypes elementA, ElementTypes elementB)
    {
        this.elementA = elementA;
        this.elementB = elementB;
    }
}

[Serializable] public class ElementalCombination
{
    public ElementTypes elementResult;
    [Space]
    public ElementTypes elementA;
    public ElementTypes elementB;
}

public enum ElementTypes
{
    Custom,
    Ash,
    Crystal,
    Earth,
    Ember,
    Energy,
    Glass,
    Lava,
    Sand,
    Smoke,
    Steam,
    Stone,
    Water,
    Wood,
}
