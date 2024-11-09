using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance { get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
             
            }
            return instance;
        } }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
     
    }
}
