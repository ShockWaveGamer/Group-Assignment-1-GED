using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ParticleSpawner : MonoBehaviour
{
    ObjectPool objectPool;

    private void Awake()
    {
        if (!(objectPool = FindObjectOfType<ObjectPool>())) objectPool = new GameObject().AddComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ClickPos.z = 0;
            CreateParticle("Particle", ClickPos);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ClickPos.z = 0;
            DeleteParticle(ClickPos);
        }

    }

    private void CreateParticle(string particleType, Vector3 pos)
    {
        // todo implement type switching 
        objectPool.CreateObj(particleType.ToString(), pos, Quaternion.identity);
        // todo add NewSpawnedParticle to pooling etc.
    }

    private void DeleteParticle(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit) objectPool.RemoveObj(hit.collider.gameObject);
    }
}