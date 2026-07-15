using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField] List<GameObject> objList = new List<GameObject>();

    Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

    private int poolSize;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        poolSize = 10;

        foreach(GameObject obj in objList)
        {
            pools[obj.name] = new ObjectPool();

            GameObject poolParent = new GameObject($"{obj.name}_pool");
            poolParent.transform.SetParent(this.transform);
            pools[obj.name].SetPoolParent(poolParent.transform);

            for(int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj, pools[obj.name].PoolParent);
                go.name = obj.name;
                go.SetActive(false);
                pools[obj.name].EnqueueObject(go);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (!pools.ContainsKey(name))
        {
            return null;
        }

        if (pools[name].PooledObjects.Count > 0)
        {
            GameObject go = pools[name].DequeueObject();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = Instantiate(objList.Find(obj => obj.name == name), pools[name].PoolParent);
            go.name = name;
            return go;
        }
    }

    public void ReturnObject(string name, GameObject go)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        pools[name].EnqueueObject(go);
    }
}
