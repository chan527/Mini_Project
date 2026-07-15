using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<GameObject> pooledObjects = new Queue<GameObject>();

    private Transform poolParent;

    public Queue<GameObject> PooledObjects => pooledObjects;
    public Transform PoolParent => poolParent;

    public void SetPoolParent(Transform parent)
    {
        poolParent = parent;
    }

    public void EnqueueObject(GameObject go)
    {
        pooledObjects.Enqueue(go);
    }

    public GameObject DequeueObject()
    {
        return pooledObjects.Dequeue();
    }
}
