using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
{
    [SerializeField] private readonly Dictionary<string, GameObject> resourceCache = new Dictionary<string, GameObject>();
    private readonly Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();

    protected override void Awake()
    {
        base.Awake();
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject resource = GetResource(prefabId);
        if(resource == null)
        {
            return null;
        }

        bool isActive = resource.activeSelf;
        if (isActive)
        {
            resource.SetActive(false);
        }

        GameObject instance = GetOrCreateInstance(resource);

        if(instance)
        {
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            if(instance.activeSelf)
            {
                instance.SetActive(false);
            }
        }

        if(isActive)
        {
            resource.SetActive(true);
        }

        return instance;
    }

    public new T Instantiate<T> (T prefab) where T : Object
    {
        if(!prefab)
        {
            return null;
        }

        GameObject prefabToGameObject = prefab as GameObject;
        if(!prefabToGameObject)
        {
            return null;
        }

        GameObject instance = GetOrCreateInstance(prefabToGameObject);
        if(instance && !instance.gameObject.activeSelf)
        {
            instance.gameObject.SetActive(true);
        }

        return instance as T;
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if(!prefab)
        {
            return null;
        }

        GameObject instance = GetOrCreateInstance(prefab);

        if(instance)
        {
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            if(!instance.activeSelf)
            {
                instance.SetActive(true);
            }
        }

        return instance;
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!prefab)
        {
            return null;
        }

        GameObject instance = GetOrCreateInstance(prefab);

        if (instance)
        {
            instance.transform.parent = parent;
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            if (!instance.activeSelf)
            {
                instance.SetActive(true);
            }
        }

        return instance;
    }

    public void Destroy(GameObject gameObject)
    {
        Poolable poolable = gameObject?.GetComponent<Poolable>();
        Pool pool = poolable?.Pool;
        if (pool != null)
        {
            pool.ReturnObjectToPool(gameObject);
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void ClearPools()
    {
        foreach(Pool pool in pools.Values)
        {
            pool.ClearPool();
        }

        pools.Clear();
    }

    private GameObject GetOrCreateInstance(GameObject prefab)
    {
        GameObject instance = null;
        Pool pool = null;

        if(pools.TryGetValue(prefab, out pool))
        {
            instance = pool.RetrieveObject();
        }
        else
        {
            instance = GameObject.Instantiate(prefab);

            Poolable poolableComponent = instance.GetComponent<Poolable>();
            if(poolableComponent)
            {
                pool = new Pool(prefab);
                poolableComponent.Pool = pool;
                pools.Add(prefab, pool);
            }
        }

        return instance;
    }

    private GameObject GetResource(string prefabId)
    {
        GameObject res = null;
        bool cached = resourceCache.TryGetValue(prefabId, out res);
        if(!cached)
        {
            res = (GameObject)Resources.Load(prefabId, typeof(GameObject));
            if(res == null)
            {
                Debug.LogError("DefaultPool failed to load \"" + prefabId + "\" . Make sure it's in the \"Resources\" folder.");
            }
            else
            {
                resourceCache.Add(prefabId, res);
            }
        }

        return res;
    }
}