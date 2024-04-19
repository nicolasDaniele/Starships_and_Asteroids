using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private GameObject resource = null;
    private Stack<GameObject> availableObjects = new Stack<GameObject>();
    private HashSet<GameObject> notAvailableObjects = new HashSet<GameObject>();

    public Pool(GameObject inResource)
    {
        resource = inResource;
    }

    public GameObject RetrieveObject()
    {
        if (resource == null)
        {
            return null;
        }

        GameObject objectToRetrieve = null;
        if (availableObjects.Count > 0)
        {
            while(availableObjects.Count > 0 && objectToRetrieve == null)
            {
                objectToRetrieve = availableObjects.Pop();
            }
            if(objectToRetrieve == null)
            {
                objectToRetrieve = CreatePoolObject();
            }
        }
        else
        {
            objectToRetrieve = CreatePoolObject();
        }

        notAvailableObjects.Add(objectToRetrieve);
        return objectToRetrieve;
    }

    public void ReturnObjectToPool(GameObject objectToReturn)
    {
        if(objectToReturn == null)
        {
            return;
        }

        notAvailableObjects.Remove(objectToReturn);
        availableObjects.Push(objectToReturn);
    }

    public void ClearPool()
    {
        List<GameObject> objectsToDestroy = new List<GameObject>();

        foreach(GameObject availableObject in availableObjects)
        {
            if(availableObject == null)
            {
                continue;
            }
            objectsToDestroy.Add(availableObject);
        }
        foreach (GameObject notAvailableObject in notAvailableObjects)
        {
            if (notAvailableObject == null)
            {
                continue;
            }
            objectsToDestroy.Add(notAvailableObject);
        }

        availableObjects.Clear();
        notAvailableObjects.Clear();

        for(int i = objectsToDestroy.Count - 1; i >= 0; i--)
        {
            GameObject instance = objectsToDestroy[i];
            objectsToDestroy.RemoveAt(i);
            if (instance)
            {
                GameObject.Destroy(instance);
            }
        }
    }

    private GameObject CreatePoolObject()
    {
        GameObject newGameObject = GameObject.Instantiate(resource) as GameObject;

        Poolable poolableComponent = newGameObject?.GetComponent<Poolable>();
        if(poolableComponent != null)
        {
            poolableComponent.Pool = this;
        }

        return newGameObject;
    }
}
