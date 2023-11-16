using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ParticleSpawner : MonoBehaviour
{
    ObjectPool objectPool;
    [SerializeField] public GameObject prefab;

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
        GameObject particle = objectPool.enabled ?
            objectPool.CreateObj(particleType.ToString(), pos, Quaternion.identity) :
            Instantiate(prefab, pos, Quaternion.identity);

        particle.GetComponent<Renderer>().sharedMaterial = prefab.GetComponent<Renderer>().sharedMaterial;
        particle.GetComponent<Rigidbody2D>().gravityScale = prefab.GetComponent<Rigidbody2D>().gravityScale;
        particle.GetComponent<BoxCollider2D>().sharedMaterial.friction = prefab.GetComponent<BoxCollider2D>().sharedMaterial.friction;
        // todo add NewSpawnedParticle to pooling etc.
    }

    private void DeleteParticle(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (!hit) return;

        if (objectPool.enabled)
        {
            objectPool.RemoveObj(hit.collider.gameObject);
        }
        else if (hit.collider.CompareTag("Particle"))
        {
            Destroy(hit.collider.gameObject);
        }

/*        objectPool.enabled ? (objectPool.RemoveObj(hit.collider.gameObject)) : (Destroy(hit.collider.gameObject));
*/    }
}