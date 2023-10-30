using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private GameObject objectPoolContainer;
    [SerializeField] public List<Pool> pools;

    [HideInInspector] public Dictionary<string, Queue<GameObject>> poolDictuibary;

    private void Start()
    {
        poolDictuibary = new Dictionary<string, Queue<GameObject>>();
        
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            if (objectPool != null)
            {
                poolDictuibary.Add(pool.tag, objectPool);
            }
        }
    }

    public GameObject CreateObj(string tag, Vector2 position, Quaternion rotation)
    {
        // check to see if a pool exists for the object in question
        if (!poolDictuibary.ContainsKey(tag))
        {
            Debug.LogError($"Object Pool does not contain tag of: {tag}!");
            return null;
        }

        GameObject obj;

        // check if there is an available object in the object pool
        if (!poolDictuibary[tag].TryDequeue(out obj))

            // if no availabe object; find prefab for new object and instantiate
            foreach (Pool pool in pools)
                if (pool.tag == tag)
                {
                    obj = Instantiate(pool.prefab);
                    break;
                }

        // set object to active and set it's position and rotation
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);

        if (objectPoolContainer) obj.transform.parent = objectPoolContainer.transform;

        return obj;
    }

    public void RemoveObj(GameObject obj)
    {
        // check to see if a pool exists for the object in question
        if (!poolDictuibary.ContainsKey(obj.tag)) return;

        // que the object in the object pool and disable the object
        poolDictuibary[obj.tag].Enqueue(obj);
        obj.SetActive(false);
    }
    
    [System.Serializable] public class Pool
    {
        [TagSelector] public string tag;
        [SerializeField] public GameObject prefab;
    }
}