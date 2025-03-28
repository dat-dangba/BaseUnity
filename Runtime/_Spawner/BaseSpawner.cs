using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner<I, T> : BaseMonoBehaviour where T : Component where I : BaseMonoBehaviour
{
    [SerializeField] protected bool dontDestroyOnLoad = false;
    [SerializeField] private int spawnedCount = 0;
    [SerializeField] protected List<T> poolObjs = new();

    protected int SpawnedCount
    {
        get => spawnedCount;
        private set => spawnedCount = value;
    }

    #region Singleton

    private static I instance;

    public static I Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<I>();
                if (instance == null)
                {
                    GameObject singleton = new(typeof(I).Name);
                    instance = singleton.AddComponent<I>();
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
            instance = this as I;
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
            Destroy(gameObject);
        }
    }

    #endregion

    public virtual T Spawn(Vector3 spawnPos, bool show = false)
    {
        return Spawn(spawnPos, Quaternion.identity, show);
    }

    public virtual T Spawn(Vector3 spawnPos, Quaternion rotation, bool show = false)
    {
        T newPrefab = GetObjectFromPool(show);
        newPrefab.transform.SetPositionAndRotation(spawnPos, rotation);
        spawnedCount++;

        return newPrefab.GetComponent<T>();
    }

    protected virtual T GetObjectFromPool(bool show)
    {
        if (poolObjs.Count > 0)
        {
            if (!poolObjs[0].gameObject.activeInHierarchy)
            {
                T t = poolObjs[0];
                t.gameObject.SetActive(show);
                poolObjs.Remove(poolObjs[0]);
                return t;
            }
        }

        T newPrefab = Instantiate(GetPrefab(), transform);
        newPrefab.gameObject.SetActive(show);
        newPrefab.name = GetPrefab().name;
        return newPrefab;
    }

    public virtual void Despawn(T obj)
    {
        if (poolObjs.Contains(obj)) return;

        poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        spawnedCount--;
    }

    public virtual void DespawnAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeInHierarchy)
            {
                Despawn(child.GetComponent<T>());
            }
        }
    }

    protected abstract T GetPrefab();
}