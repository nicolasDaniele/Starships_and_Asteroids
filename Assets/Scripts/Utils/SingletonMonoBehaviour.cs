using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance => instance;
    public virtual bool ShouldDestroyOnLoad => true;

    private static T instance;

    protected virtual void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        if(!ShouldDestroyOnLoad)
        {
            if(transform.parent != null)
            {
                transform.SetParent(null);
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void DestroyInstance()
    {
        if(instance == null)
        {
            return;
        }

        Destroy(instance.gameObject);
    }
}
