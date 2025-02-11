using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : BaseMonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    protected bool dontDestroyOnLoad = false;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject singleton = new(typeof(T).Name);
                    instance = singleton.AddComponent<T>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (dontDestroyOnLoad)
            {
                Transform root = transform.root;
                if (root != transform)
                {
                    DontDestroyOnLoad(root);
                }
                else
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }
        else
        {
            Debug.Log($"datdb - Destroy");
            Destroy(gameObject);
        }
    }
}
