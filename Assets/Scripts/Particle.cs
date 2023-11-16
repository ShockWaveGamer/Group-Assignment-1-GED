using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particle : MonoBehaviour
{
    ElementalManagementSystem elementalManagementSystem;
    [HideInInspector] public ElementTypes element;

    Renderer particleRenderer;
    Rigidbody2D body2D;
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        elementalManagementSystem = FindObjectOfType<ElementalManagementSystem>();

        particleRenderer = GetComponent<Renderer>();
        body2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void UpdateParticleElement(ElementTypes type)
    {
        if (!elementalManagementSystem)
        {
            elementalManagementSystem = FindObjectOfType<ElementalManagementSystem>();
        }

        if (elementalManagementSystem.GetElementData(type) == null) return;
        
        Element newElement = elementalManagementSystem.GetElementData(type);

        if (!particleRenderer || !body2D || !boxCollider2D)
        {
            particleRenderer = GetComponent<Renderer>();
            body2D = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        element = type;
        particleRenderer.material.color = newElement.color;
        body2D.gravityScale = newElement.gravity;
        boxCollider2D.sharedMaterial.friction = newElement.friction;
    }

    public void UpdateParticleElement(ElementTypes type, Color col, float grav, float fric)
    {
        if (!particleRenderer || !body2D || !boxCollider2D)
        {
            particleRenderer = GetComponent<Renderer>();
            body2D = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        element = type;
        particleRenderer.material.color = col;
        body2D.gravityScale = grav;
        boxCollider2D.sharedMaterial.friction = fric;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Particle>(out Particle otherParticle))
            elementalManagementSystem.UpdateParticleElements(this, otherParticle);
    }
}
