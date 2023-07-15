using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//https://www.youtube.com/watch?v=texonivDsy0
// FOLDER OF HELPER 
public class ObjectPool : Singleton<ObjectPool>
{
    // List of the objects to be pooled
    public List<GameObject> m_PrefabsForPool;

    // List of the pooled objects
    private List<GameObject> m_PooledObjects = new List<GameObject>();

    public GameObject GetObjectFromPool(string a_ObjectName)
    {
        // try get a pooled instance
        var instance = m_PooledObjects.FirstOrDefault(obj => obj.name == a_ObjectName);

        // If we have a pooled instance already
        if (instance != null)
        {
            m_PooledObjects.Remove(instance);
            instance.SetActive(true);
            return instance;
        }

        // if we don't have a pooled instance
        var prefab = m_PrefabsForPool.FirstOrDefault(obj => obj.name == a_ObjectName);
        
        if (prefab != null)
        {
            // create a new instance
            var newInstance = Instantiate(prefab, transform.position, Quaternion.identity, transform);

            newInstance.name = a_ObjectName;

            return newInstance;
        }

        Debug.LogWarning("Object pool doesnt have a prefab for the object with name " + a_ObjectName);
        return null;
    }

    public void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
        m_PooledObjects.Add(obj);
    }
}