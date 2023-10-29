using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int initialSize;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> m_poolDictionary;

    private void Start()
    {

        m_poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            if (objectPool != null)
            {
                m_poolDictionary.Add(pool.tag, objectPool);
            }
        }

    }

    public GameObject ReturnObject(string tag, Vector2 position, Quaternion rotation)
    {
        if (!m_poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("ayo it broke");
            return null;
        }

        GameObject objectToSpawn = m_poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);

        m_poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}